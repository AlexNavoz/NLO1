using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;

public class MainScript : MonoBehaviour, IUnityAdsListener
{
    public bool peace = false;
    //plate variables
    public float P_enginePower;
    public float P_maxFuel;
    public float P_gunPower;
    public float P_rayLiftPower;
    public float P_forceShieldStrength;

    //WS variables
    public float WS_enginePower;
    public float WS_maxFuel;
    public float WS_gunPower;
    public float WS_rayLiftPower;
    public float WS_forceShieldStrength;

    //Knippel variables
    public float K_enginePower;
    public float K_maxFuel;
    public float K_gunPower;
    public float K_rayLiftPower;
    public float K_forceShieldStrength;

    //plate short time variables
    public float P_fuelLevel;
    public float P_forceShieldLevel;
    public int P_cowCount;

    //starting game
    Vector3 startPos;
    Transform startPosTransform;
    public GameObject plate;
    public GameObject warship;
    public GameObject knippel;
    public int ShipIndex;
    public int levelIndex = 0;
    public int mainMenuPanelIndex = 0;
    playerMoving player;
    ForceShieldScript fs;
    public bool shieldIsActive;
    public int[] campLevels = null;

    //money variables
    string lname;
    public int collection;
    public int allMoney;

    //GPG_CloudSaveSystem cloudsaves;

    public static float forceBatchingMultiplier = 0;

    //Quest variables
    public int questObjectCount = 0;
    public int questObjectIndex = 0;
    public int questObjectId = 0;
    public int questIdSeed = 0;
    public GameObject questButton;
    public GameObject[] quests;
    public Text questPanelProgress;
    public Text questPanelTime;

    //Campaign Quest var
    public int campaignQisetObjIndex = 0;
    public int campaignQuestObjCount = 0;

#if UNITY_IOS
    string gameId = "4059550";
#else
    string gameId = "4059551";
#endif
    string mySurfacingId = "rewardedVideo";
    bool testMode = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //cloudsaves = new GPG_CloudSaveSystem();
        //cloudsaves.SaveToCloud();
        LoadPlatePrefs();
        LoadWSPrefs();
        LoadKPrefs();
        LoadQuestPrefs();

        ShipIndex = PlayerPrefs.GetInt("ShipIndex",0);
        allMoney = PlayerPrefs.GetInt("allMoney", 1500);

        forceBatchingMultiplier = (0.02f / Time.fixedDeltaTime);

        //Analytics.initializeOnStartup = false;
        //Analytics.enabled = false;

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }
    public void StartOnPosition()
    {
        startPosTransform = GameObject.FindGameObjectWithTag("StartPosition").GetComponent<Transform>();
        startPos = new Vector3(startPosTransform.position.x, startPosTransform.position.y, startPosTransform.position.z);

        //change after garage menu
        if (ShipIndex == 0)
        {
            Instantiate(plate, startPos, Quaternion.identity);
        }
        if (ShipIndex == 1)
        {
            Instantiate(warship, startPos, Quaternion.identity);
        }
        if (ShipIndex == 2)
        {
            Instantiate(knippel, startPos, Quaternion.identity);
        }
    }

    public void SetMoney(int money)
    {
        allMoney += money;
        PlayerPrefs.SetInt("allMoney", allMoney);
    }
    // quest

    public void LoadQuestPrefs()
    {
        questObjectId = PlayerPrefs.GetInt("questObjectId", 0);
        questObjectCount = PlayerPrefs.GetInt("questObjectCount", 0);
        questIdSeed = PlayerPrefs.GetInt("questIdSeed", 0);
    }

    public void SaveQuestPrefs()
    {
        PlayerPrefs.SetInt("questObjectId", questObjectId);
        PlayerPrefs.SetInt("questObjectCount", questObjectCount);
        PlayerPrefs.SetInt("questIdSeed", questIdSeed);
    }

    //__________________________________________________________________________PLATE______________________________________________________________________
    public void LoadPlatePrefs()
    {
        P_enginePower = PlayerPrefs.GetFloat("P_enginePower", 120.0f);
        P_maxFuel = PlayerPrefs.GetFloat("P_maxFuel", 30.0f);
        P_rayLiftPower = PlayerPrefs.GetFloat("P_rayLiftPower", 30.0f);
        P_gunPower = PlayerPrefs.GetFloat("P_gunPower", 0.1f);
        P_forceShieldStrength = PlayerPrefs.GetFloat("P_forceShieldStrength", 20.0f);
    }

    //__________________________________________________________________________________WARSHIP__________________________________________________________


    public void LoadWSPrefs()
    {
        WS_enginePower = PlayerPrefs.GetFloat("WS_enginePower", 80.0f);
        WS_maxFuel = PlayerPrefs.GetFloat("WS_maxFuel", 40.0f);
        WS_gunPower = PlayerPrefs.GetFloat("WS_gunPower", 0.2f);
        WS_rayLiftPower = PlayerPrefs.GetFloat("WS_rayLiftPower", 30.0f);
        WS_forceShieldStrength = PlayerPrefs.GetFloat("WS_forceShieldStrength", 40.0f);
    }
    //__________________________________________________________________________________KNIPPEL__________________________________________________________


    public void LoadKPrefs()
    {
        K_enginePower = PlayerPrefs.GetFloat("K_enginePower", 80.0f);
        K_maxFuel = PlayerPrefs.GetFloat("K_maxFuel", 40.0f);
        K_gunPower = PlayerPrefs.GetFloat("K_gunPower", 0.2f);
        K_rayLiftPower = PlayerPrefs.GetFloat("K_rayLiftPower", 30.0f);
        K_forceShieldStrength = PlayerPrefs.GetFloat("K_forceShieldStrength", 40.0f);
    }

    public void CampLevels()
    {
        CampaignMenuScript campMenu = GameObject.FindGameObjectWithTag("CampMenu").GetComponent<CampaignMenuScript>();
        campLevels = new int[campMenu.campStages.Length];
        for (int i = 0; i < campMenu.campStages.Length; i++)
        {
            campLevels[i] = PlayerPrefs.GetInt("campStage" + i, 0);
        }
    }

    //________________________________________________________BuyMoney___________________________________________

    AdsListener adslistener;
    public void checkIfAdsReady()
    {
        if (Advertisement.IsReady(mySurfacingId))
        {
            EnableAdButtons();
        }
        else
        {
            DisableAdButtons();
        }
    }
    void DisableAdButtons()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("AdButton");
        if (buttons == null)
            return;
        foreach (GameObject buttongo in buttons) {
            Button button =  buttongo.GetComponent<Button>();
            if(button!=null)
                button.interactable = false;
        }
    }
    void EnableAdButtons()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("AdButton");
        if (buttons == null)
            return;
        foreach (GameObject buttongo in buttons)
        {
            Button button = buttongo.GetComponent<Button>();
            if (button != null)
                button.interactable = true;
        }
    }
    public bool ShowRewardedVideo(AdsListener p_adslistener)
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(mySurfacingId))
        {
            Advertisement.Show(mySurfacingId);
            adslistener = p_adslistener;
            return true;
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
        return false;
    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        if (adslistener == null)
            return;
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            adslistener.AdsShowed();
            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped)
        {
            adslistener.AdsSkipped();
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
            adslistener.AdsFailed();
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == mySurfacingId)
        {
            EnableAdButtons();
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }


    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    //__________________________________________________________________________________Quest_________________________________________
    
}
