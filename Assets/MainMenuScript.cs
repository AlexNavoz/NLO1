using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour, AdsListener
{
    MainScript mainScript;
    GameObject mainCameraObj;
    bool garageIsOpen;
    int adsDestination;

    //Energy
    public GameObject energyPanel;
    public Text energyText;
    public Text energyTimeText;
    public Text enetgyPriceText;

    //DailyQuest
    QuestScript questScript;
    public Button dailyQuestButton;

    //Money
    public Text milkText;
    public Text brainsText;
    int currentMilk;
    int currentBrains;

    public AudioSource tunButton;
    public AudioSource clickButton;                       //Sounds
    public AudioSource menuOpenSound;

    //Settings
    public GameObject settingsPanel;
    public Text onOffText;
    public AudioMixerGroup mixer;
    public Slider effectsSlider;
    public Slider musicSlider;
    //float musicVolume;
    float effectsVolume;

    //Campaign
    public GameObject campaignPanel;

    //ShipRent
    public GameObject rentPanel;
    public Button rentByMoneyButton;
    int rentIndex = 0;

    public Button nextShipButton;
    public Button previousShipButton;
    public GameObject EGaragePanel;
    public GameObject plateGaragePanel;
    public GameObject WSGaragePanel;
    public GameObject knippelGaragePanel;
    public Transform[] transformPoints;
    public GameObject buyMoneyPanel;
    public GameObject chooseGameStagePanel;

    int[] Eprices = new int[] { 2, 4, 8, 15, 20, 30, 40, 50, 60, 70 };
    int[] prices = new int[] { 2, 4, 8, 15, 20, 30, 40, 50, 60, 70 };
    int[] WSprices = new int[] { 3, 6, 10, 20, 30, 50, 60, 70, 80, 100 };
    int[] Kprices = new int[] { 5, 10, 20, 40, 60, 80, 100, 150, 200, 300 };

    //Buy ship
    public GameObject buyWSPanel;
    public Button buyWSButton;
    public GameObject buyWSButtonObj;
    public GameObject buyKPanel;
    public Button buyKButton;
    public GameObject buyKButtonObj;


    //EasyShip variables_______________________________________________________________________________________________

    //engine
    int E_EngineLevel;
    int E_enginePrice;
    public Text E_engineText;
    public Slider E_engineSlider;
    public Button E_engineButton;
    float[] E_enginepowers = new float[] { 160.0f, 170.0f, 180.0f, 190.0f, 200.0f, 210.0f, 220.0f, 240.0f, 270.0f, 300.0f };

    //Ray
    int E_RayLevel;
    int E_rayPrice;
    public Text E_rayText;
    public Slider E_raySlider;
    public Button E_rayButton;
    float[] E_raypowers = new float[] { 7.0f, 8.5f, 9.0f, 10.5f, 12.0f, 12.5f, 13.0f, 13.5f, 14.0f, 15.0f };
    float[] E_gunPowers = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1 };

    //Tank
    int E_TankLevel;
    int E_tankPrice;
    public Text E_tankText;
    public Slider E_tankSlider;
    public Button E_tankButton;
    float[] E_tankpowers = new float[] { 40.0f, 45.0f, 50.0f, 55.0f, 60.0f, 65.0f, 70.0f, 75.0f, 80.0f, 90.0f };

    //Shield
    int E_ShieldLevel;
    int E_shieldPrice;
    public Text E_shieldText;
    public Slider E_shieldSlider;
    public Button E_shieldButton;
    float[] E_shieldpowers = new float[] { 20.0f, 25.0f, 30.0f, 40.0f, 50.0f, 60.0f, 70.0f, 80.0f, 100.0f, 130.0f };

    //plate variables_______________________________________________________________________________________________

    //engine
    int P_EngineLevel;
    int P_enginePrice;
    public Text P_engineText;
    public Slider P_engineSlider;
    public Button P_engineButton;
    float[] P_enginepowers = new float[] { 160.0f, 170.0f, 180.0f, 190.0f, 200.0f, 210.0f, 220.0f, 240.0f, 270.0f, 300.0f };

    //Ray
    int P_RayLevel;
    int P_rayPrice;
    public Text P_rayText;
    public Slider P_raySlider;
    public Button P_rayButton;
    float[] P_raypowers = new float[] { 10.0f, 11.0f, 12.0f, 13.0f, 14.0f, 15.0f, 16.0f, 17.0f, 18.0f, 20.0f };
    float[] P_gunPowers = new float[] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1 };

    //Tank
    int P_TankLevel;
    int P_tankPrice;
    public Text P_tankText;
    public Slider P_tankSlider;
    public Button P_tankButton;
    float[] P_tankpowers = new float[] { 40.0f, 45.0f, 50.0f, 60.0f, 70.0f, 80.0f, 90.0f, 100.0f, 110.0f, 130.0f };

    //Shield
    int P_ShieldLevel;
    int P_shieldPrice;
    public Text P_shieldText;
    public Slider P_shieldSlider;
    public Button P_shieldButton;
    float[] P_shieldpowers = new float[] { 20.0f, 30.0f, 50.0f, 70.0f, 100.0f, 130.0f, 160.0f, 190.0f, 240.0f, 300.0f };



    //WarShip variables_________________________________________________________________________________________________________

    //engine
    int WS_EngineLevel;
    int WS_enginePrice;
    public Text WS_engineText;
    public Slider WS_engineSlider;
    public Button WS_engineButton;
    float[] WS_enginepowers = new float[] { 100.0f, 110.0f, 115.0f, 120.0f, 125.0f, 130.0f, 140.0f, 150.0f, 170.0f, 200.0f };

    //Ray
    int WS_RayLevel;
    int WS_rayPrice;
    public Text WS_rayText;
    public Slider WS_raySlider;
    public Button WS_rayButton;
    float[] WS_raypowers = new float[] { 10.0f, 12.0f, 14.0f, 16.0f, 18.0f, 20.0f, 22.0f, 24.0f, 26.0f, 30.0f };
    float[] WS_gunPowers = new float[] { 0.2f, 0.3f, 0.4f, 0.6f, 0.8f, 1.0f, 1.25f, 1.5f, 1.7f, 2.0f };

    //Tank
    int WS_TankLevel;
    int WS_tankPrice;
    public Text WS_tankText;
    public Slider WS_tankSlider;
    public Button WS_tankButton;
    float[] WS_tankpowers = new float[] { 40.0f, 50.0f, 60.0f, 80.0f, 100.0f, 130.0f, 150.0f, 160.0f, 180.0f, 200.0f };

    //Shield
    int WS_ShieldLevel;
    int WS_shieldPrice;
    public Text WS_shieldText;
    public Slider WS_shieldSlider;
    public Button WS_shieldButton;
    float[] WS_shieldpowers = new float[] { 40.0f, 60.0f, 80.0f, 100.0f, 130.0f, 160.0f, 200.0f, 240.0f, 300.0f, 400.0f };

    //Knippel variables_________________________________________________________________________________________________________

    //engine
    int K_EngineLevel;
    int K_enginePrice;
    public Text K_engineText;
    public Slider K_engineSlider;
    public Button K_engineButton;
    float[] K_enginepowers = new float[] { 100.0f, 110.0f, 115.0f, 120.0f, 125.0f, 130.0f, 140.0f, 150.0f, 170.0f, 200.0f };

    //Ray
    int K_RayLevel;
    int K_rayPrice;
    public Text K_rayText;
    public Slider K_raySlider;
    public Button K_rayButton;
    float[] K_raypowers = new float[] { 10.0f, 12.0f, 14.0f, 16.0f, 18.0f, 21.0f, 24.0f, 27.0f, 30.0f, 35.0f };
    float[] K_gunPowers = new float[] { 0.2f, 0.3f, 0.4f, 0.6f, 0.8f, 1.0f, 1.25f, 1.5f, 1.7f, 2.0f };

    //Tank
    int K_TankLevel;
    int K_tankPrice;
    public Text K_tankText;
    public Slider K_tankSlider;
    public Button K_tankButton;
    float[] K_tankpowers = new float[] { 50.0f, 60.0f, 80.0f, 100.0f, 120.0f, 150.0f, 170.0f, 200.0f, 250.0f, 300.0f };

    //Shield
    int K_ShieldLevel;
    int K_shieldPrice;
    public Text K_shieldText;
    public Slider K_shieldSlider;
    public Button K_shieldButton;
    float[] K_shieldpowers = new float[] { 50.0f, 80.0f, 120.0f, 150.0f, 170.0f, 200.0f, 250.0f, 300.0f, 400.0f, 500.0f };

    int shipIndex = 0;
    private void Start()
    {
        Time.timeScale = 1;
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        questScript = GameObject.FindGameObjectWithTag("Quest").GetComponent<QuestScript>();
        mainScript.questButton.SetActive(false);
        rentPanel.SetActive(false);
        MusicVolumeOnStart();
        EffectsVolumeOnStart();

        #region

        //EasyShip_______________________________________________________________________________________________________________________________

        //engine
        E_engineSlider.value = PlayerPrefs.GetFloat("E_enginePower", 160.0f);
        E_EngineLevel = PlayerPrefs.GetInt("E_EngineLevel", 0);
        E_enginePrice = Eprices[E_EngineLevel];
        E_engineText.text = E_enginePrice.ToString();

        //Ray
        E_raySlider.value = PlayerPrefs.GetFloat("E_rayLiftPower", 7.0f);
        E_RayLevel = PlayerPrefs.GetInt("E_RayLevel", 0);
        E_rayPrice = Eprices[E_RayLevel];
        E_rayText.text = E_rayPrice.ToString();

        //Tank
        E_tankSlider.value = PlayerPrefs.GetFloat("E_maxFuel", 40.0f);
        E_TankLevel = PlayerPrefs.GetInt("E_TankLevel", 0);
        E_tankPrice = Eprices[E_TankLevel];
        E_tankText.text = E_tankPrice.ToString();

        //Shield
        E_shieldSlider.value = PlayerPrefs.GetFloat("E_forceShieldStrength", 20.0f);
        E_ShieldLevel = PlayerPrefs.GetInt("E_ShieldLevel", 0);
        E_shieldPrice = Eprices[E_ShieldLevel];
        E_shieldText.text = E_shieldPrice.ToString();

        //plate_______________________________________________________________________________________________________________________________

        //engine
        P_engineSlider.value = PlayerPrefs.GetFloat("P_enginePower", 160.0f);
        P_EngineLevel = PlayerPrefs.GetInt("P_EngineLevel", 0);
        P_enginePrice = prices[P_EngineLevel];
        P_engineText.text = P_enginePrice.ToString();

        //Ray
        P_raySlider.value = PlayerPrefs.GetFloat("P_rayLiftPower", 10.0f);
        P_RayLevel = PlayerPrefs.GetInt("P_RayLevel", 0);
        P_rayPrice = prices[P_RayLevel];
        P_rayText.text = P_rayPrice.ToString();

        //Tank
        P_tankSlider.value = PlayerPrefs.GetFloat("P_maxFuel", 30.0f);
        P_TankLevel = PlayerPrefs.GetInt("P_TankLevel", 0);
        P_tankPrice = prices[P_TankLevel];
        P_tankText.text = P_tankPrice.ToString();

        //Shield
        P_shieldSlider.value = PlayerPrefs.GetFloat("P_forceShieldStrength", 20.0f);
        P_ShieldLevel = PlayerPrefs.GetInt("P_ShieldLevel", 0);
        P_shieldPrice = prices[P_ShieldLevel];
        P_shieldText.text = P_shieldPrice.ToString();

        //WarShip_____________________________________________________________________________________________________________________

        //engine
        WS_engineSlider.value = PlayerPrefs.GetFloat("WS_enginePower", 80.0f);
        WS_EngineLevel = PlayerPrefs.GetInt("WS_EngineLevel", 0);
        WS_enginePrice = WSprices[WS_EngineLevel];
        WS_engineText.text = WS_enginePrice.ToString();

        //Ray
        WS_raySlider.value = PlayerPrefs.GetFloat("WS_rayLiftPower", 10.0f);
        WS_RayLevel = PlayerPrefs.GetInt("WS_RayLevel", 0);
        WS_rayPrice = WSprices[WS_RayLevel];
        WS_rayText.text = WS_rayPrice.ToString();

        //Tank
        WS_tankSlider.value = PlayerPrefs.GetFloat("WS_maxFuel", 30.0f);
        WS_TankLevel = PlayerPrefs.GetInt("WS_TankLevel", 0);
        WS_tankPrice = WSprices[WS_TankLevel];
        WS_tankText.text = WS_tankPrice.ToString();

        //Shield
        WS_shieldSlider.value = PlayerPrefs.GetFloat("WS_forceShieldStrength", 20.0f);
        WS_ShieldLevel = PlayerPrefs.GetInt("WS_ShieldLevel", 0);
        WS_shieldPrice = WSprices[WS_ShieldLevel];
        WS_shieldText.text = WS_shieldPrice.ToString();

        //Knippel_____________________________________________________________________________________________________________________

        //engine
        K_engineSlider.value = PlayerPrefs.GetFloat("K_enginePower", 80.0f);
        K_EngineLevel = PlayerPrefs.GetInt("K_EngineLevel", 0);
        K_enginePrice = Kprices[K_EngineLevel];
        K_engineText.text = K_enginePrice.ToString();

        //Ray
        K_raySlider.value = PlayerPrefs.GetFloat("K_rayLiftPower", 10.0f);
        K_RayLevel = PlayerPrefs.GetInt("K_RayLevel", 0);
        K_rayPrice = Kprices[K_RayLevel];
        K_rayText.text = K_rayPrice.ToString();

        //Tank
        K_tankSlider.value = PlayerPrefs.GetFloat("K_maxFuel", 50.0f);
        K_TankLevel = PlayerPrefs.GetInt("K_TankLevel", 0);
        K_tankPrice = Kprices[K_TankLevel];
        K_tankText.text = K_tankPrice.ToString();

        //Shield
        K_shieldSlider.value = PlayerPrefs.GetFloat("K_forceShieldStrength", 50.0f);
        K_ShieldLevel = PlayerPrefs.GetInt("K_ShieldLevel", 0);
        K_shieldPrice = Kprices[K_ShieldLevel];
        K_shieldText.text = K_shieldPrice.ToString();
#endregion

        //_______________________________________________________________________SWITCH SHIP___________________
        shipIndex = PlayerPrefs.GetInt("ShipIndex", -1);
        switch (shipIndex)
        {
            case -1:
                nextShipButton.interactable = true;
                previousShipButton.interactable = false;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(true);
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                break;
            case 0:
                nextShipButton.interactable = true;
                previousShipButton.interactable = true;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(true);
                    EGaragePanel.SetActive(false);
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                break;
            case 1:
                nextShipButton.interactable = true;
                previousShipButton.interactable = true;
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(true);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(false);
                    if (PlayerPrefs.GetInt("WSBuy", 0) == 0)
                    {
                        buyWSPanel.SetActive(true);
                    }
                    else
                    {
                        buyWSPanel.SetActive(false);
                    }
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                if (PlayerPrefs.GetInt("WSBuy", 0) == 0)
                {
                    buyWSButtonObj.SetActive(true);
                }
                else
                {
                    buyWSButtonObj.SetActive(false);
                }
                break;
            case 2:
                nextShipButton.interactable = false;
                previousShipButton.interactable = true;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(true);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(false);
                    if (PlayerPrefs.GetInt("KBuy", 0) == 0)
                    {
                        buyKPanel.SetActive(true);
                    }
                    else
                    {
                        buyKPanel.SetActive(false);
                    }
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                if (PlayerPrefs.GetInt("KBuy", 0) == 0)
                {
                    buyKButtonObj.SetActive(true);
                }
                else
                {
                    buyKButtonObj.SetActive(false);
                }
                break;
        }

        switch (mainScript.mainMenuPanelIndex)
        {
            case 1:
                GameButtonClick();
                break;
            case 2:
                CampaignButtonClick();
                break;
        }
        currentMilk = mainScript.allMilk;
        milkText.text = currentMilk.ToString();
        currentBrains = mainScript.allBrains;
        brainsText.text = currentBrains.ToString();

    }

    float changingPercent = 0;
    float changingPercentB = 0;
    private void Update()
    {
        mainCameraObj.transform.position = Vector3.Lerp(mainCameraObj.transform.position, transformPoints[shipIndex+1].position, 0.05f);

        energyText.text = mainScript.GetCurrentEnergy().ToString() + "/" + mainScript.maximumEnergy.ToString();
        {
            int timetofill = (int)mainScript.GetTimeToFillEnergy();
            energyTimeText.text = (timetofill / (60*60)).ToString("00") + ":" + ((timetofill % (60*60)) / 60).ToString("00") + ":" + (timetofill % 60).ToString("00");
        }

        //MilkButton
        bool doupdate = false; // Не обновляем значение лишний раз чтобы не грузить
        if (currentMilk != mainScript.allMilk && changingPercent < 1.0f)
        {
            changingPercent += Time.deltaTime / (1.0f); // Подгоночный коэффицент, количество секунд для прокрутки денег
            doupdate = true;
        }
        else {
            if (changingPercent != 0)
                doupdate = true;
            currentMilk = mainScript.allMilk;
            changingPercent = 0;
        }
        if(doupdate)
            milkText.text = ((int)Mathf.Lerp((float)currentMilk,(float)mainScript.allMilk,changingPercent)).ToString();

        //BrainsButton
        bool doupdateB = false;
        if (currentBrains != mainScript.allBrains && changingPercentB < 1.0f)
        {
            changingPercentB += Time.deltaTime / (1.0f); // Подгоночный коэффицент, количество секунд для прокрутки денег
            doupdateB = true;
        }
        else {
            if (changingPercentB != 0)
                doupdateB = true;
            currentBrains = mainScript.allBrains;
            changingPercentB = 0;
        }
        if(doupdateB)
            brainsText.text = ((int)Mathf.Lerp((float)currentBrains, (float)mainScript.allBrains, changingPercentB)).ToString();

        //Buy ships
        if (mainScript.allMilk < 100)
        {
            buyWSButton.interactable = false;
        }
        else buyWSButton.interactable = true;


        //_______________________Knippel Buy

        if (mainScript.allMilk < 1000)
        {
            buyKButton.interactable = false;
        }
        else buyKButton.interactable = true;

        if (mainScript.allMilk < 10)
        {
            rentByMoneyButton.interactable = false;
        }
        else rentByMoneyButton.interactable = true;

        //___________________________________________dailyQuest_______
        if (questScript.questCompleted)
        {
            dailyQuestButton.interactable = false;
        }
        else
        {
            dailyQuestButton.interactable = true;
        }

        #region
        //_____________________________________________________________________EASYSHIP_________________________________________
        //engine
        if (E_enginePrice > mainScript.allBrains)
        {
            E_engineButton.interactable = false;
        }
        else E_engineButton.interactable = true;

        if (E_EngineLevel == 9)
        {
            E_engineButton.gameObject.SetActive(false);
        }



        //ray
        if (E_rayPrice > mainScript.allBrains)
        {
            E_rayButton.interactable = false;
        }
        else E_rayButton.interactable = true;

        if (PlayerPrefs.GetInt("E_RayLevel", 0) == 9)
        {
            E_rayButton.gameObject.SetActive(false);
        }

        //tank
        if (E_tankPrice > mainScript.allBrains)
        {
            E_tankButton.interactable = false;
        }
        else E_tankButton.interactable = true;
        if (E_TankLevel == 9)
        {
            E_tankButton.gameObject.SetActive(false);
        }

        //shield
        if (E_shieldPrice > mainScript.allBrains)
        {
            E_shieldButton.interactable = false;
        }
        else E_shieldButton.interactable = true;
        if (E_ShieldLevel == 9)
        {
            E_shieldButton.gameObject.SetActive(false);
        }

        //_____________________________________________________________________PLATE_________________________________________
        //engine
        if (P_enginePrice > mainScript.allBrains)
        {
            P_engineButton.interactable = false;
        }
        else P_engineButton.interactable = true;

        if (P_EngineLevel == 9)
        {
            P_engineButton.gameObject.SetActive(false);
        }



        //ray
        if (P_rayPrice > mainScript.allBrains)
        {
            P_rayButton.interactable = false;
        }
        else P_rayButton.interactable = true;

        if (PlayerPrefs.GetInt("P_RayLevel", 0) == 9)
        {
            P_rayButton.gameObject.SetActive(false);
        }

        //tank
        if (P_tankPrice > mainScript.allBrains)
        {
            P_tankButton.interactable = false;
        }
        else P_tankButton.interactable = true;
        if (P_TankLevel == 9)
        {
            P_tankButton.gameObject.SetActive(false);
        }

        //shield
        if (P_shieldPrice > mainScript.allBrains)
        {
            P_shieldButton.interactable = false;
        }
        else P_shieldButton.interactable = true;
        if (P_ShieldLevel == 9)
        {
            P_shieldButton.gameObject.SetActive(false);
        }

        //___________________________________________________________________________WARSHIP____________________________________________________

        //engine
        if (WS_enginePrice > mainScript.allBrains)
        {
            WS_engineButton.interactable = false;
        }
        else WS_engineButton.interactable = true;
        if (WS_EngineLevel == 9)
        {
            WS_engineButton.gameObject.SetActive(false);
        }


        //ray
        if (WS_rayPrice > mainScript.allBrains)
        {
            WS_rayButton.interactable = false;
        }
        else WS_rayButton.interactable = true;
        if (PlayerPrefs.GetInt("WS_RayLevel", 0) == 9)
        {
            WS_rayButton.gameObject.SetActive(false);
        }

        //tank
        if (WS_tankPrice > mainScript.allBrains)
        {
            WS_tankButton.interactable = false;
        }
        else WS_tankButton.interactable = true;
        if (PlayerPrefs.GetInt("WS_TankLevel", 0) == 9)
        {
            WS_tankButton.gameObject.SetActive(false);
        }

        //shield
        if (WS_shieldPrice > mainScript.allBrains)
        {
            WS_shieldButton.interactable = false;
        }
        else WS_shieldButton.interactable = true;
        if (PlayerPrefs.GetInt("WS_ShieldLevel", 0) == 9)
        {
            WS_shieldButton.gameObject.SetActive(false);
        }
        //___________________________________________________________________________Knippel____________________________________________________

        //engine
        if (K_enginePrice > mainScript.allBrains)
        {
            K_engineButton.interactable = false;
        }
        else K_engineButton.interactable = true;
        if (K_EngineLevel == 9)
        {
            K_engineButton.gameObject.SetActive(false);
        }


        //ray
        if (K_rayPrice > mainScript.allBrains)
        {
            K_rayButton.interactable = false;
        }
        else K_rayButton.interactable = true;
        if (PlayerPrefs.GetInt("K_RayLevel", 0) == 9)
        {
            K_rayButton.gameObject.SetActive(false);
        }

        //tank
        if (K_tankPrice > mainScript.allBrains)
        {
            K_tankButton.interactable = false;
        }
        else K_tankButton.interactable = true;
        if (PlayerPrefs.GetInt("K_TankLevel", 0) == 9)
        {
            K_tankButton.gameObject.SetActive(false);
        }

        //shield
        if (K_shieldPrice > mainScript.allBrains)
        {
            K_shieldButton.interactable = false;
        }
        else K_shieldButton.interactable = true;
        if (PlayerPrefs.GetInt("K_ShieldLevel", 0) == 9)
        {
            K_shieldButton.gameObject.SetActive(false);
        }
        #endregion
    }

    public void DailyQuestPanelOpen()
    {
        questScript.QuestPanelActivation();
    }

    //___________________________________________________TUNNING!!!_________________________________________________________________________________
    #region

    //___________________________________________________________EASYSHIP_UPGRADE_____________________________________________________
    public void E_UpgradeEngine()
    {
        tunButton.Play();
        E_enginePrice = Eprices[E_EngineLevel];
        E_EngineLevel++;
        PlayerPrefs.SetInt("E_EngineLevel", E_EngineLevel);
        PlayerPrefs.SetFloat("E_enginePower", E_enginepowers[E_EngineLevel]);
        mainScript.SetBrains(-E_enginePrice);
        E_engineSlider.value = PlayerPrefs.GetFloat("E_enginePower", 160.0f);
        E_enginePrice = Eprices[E_EngineLevel];
        E_engineText.text = E_enginePrice.ToString();

        if (PlayerPrefs.GetInt("E_EngineLevel", 0) == 9)
        {
            E_engineButton.gameObject.SetActive(false);
        }

    }
    public void E_UpgradeRay()
    {
        tunButton.Play();
        E_rayPrice = Eprices[E_RayLevel];
        E_RayLevel++;
        PlayerPrefs.SetInt("E_RayLevel", E_RayLevel);
        PlayerPrefs.SetFloat("E_rayLiftPower", E_raypowers[E_RayLevel]);
        PlayerPrefs.SetFloat("E_gunPower", E_gunPowers[E_RayLevel]);
        mainScript.SetBrains(-E_rayPrice);
        E_raySlider.value = PlayerPrefs.GetFloat("E_rayLiftPower", 10.0f);
        E_rayPrice = Eprices[E_RayLevel];
        E_rayText.text = E_rayPrice.ToString();

        if (PlayerPrefs.GetInt("E_RayLevel", 0) == 9)
        {
            E_rayButton.gameObject.SetActive(false);
        }

    }

    public void E_UpgradeTank()
    {
        tunButton.Play();
        E_tankPrice = Eprices[E_TankLevel];
        E_TankLevel++;
        PlayerPrefs.SetInt("E_TankLevel", E_TankLevel);
        PlayerPrefs.SetFloat("E_maxFuel", E_tankpowers[E_TankLevel]);
        mainScript.SetBrains(-E_tankPrice);
        E_tankSlider.value = PlayerPrefs.GetFloat("E_maxFuel", 40.0f);
        E_tankPrice = Eprices[E_TankLevel];
        E_tankText.text = E_tankPrice.ToString();

        if (PlayerPrefs.GetInt("E_TankLevel", 0) == 9)
        {
            E_tankButton.gameObject.SetActive(false);
        }

    }

    public void E_UpgradeShield()
    {
        tunButton.Play();
        E_shieldPrice = Eprices[E_ShieldLevel];
        E_ShieldLevel++;
        PlayerPrefs.SetInt("E_ShieldLevel", E_ShieldLevel);
        PlayerPrefs.SetFloat("E_forceShieldStrength", E_shieldpowers[E_ShieldLevel]);
        mainScript.SetBrains(-E_shieldPrice);
        E_shieldSlider.value = PlayerPrefs.GetFloat("E_forceShieldStrength", 20.0f);
        E_shieldPrice = Eprices[E_ShieldLevel];
        E_shieldText.text = E_shieldPrice.ToString();

        if (PlayerPrefs.GetInt("E_ShieldLevel", 0) == 9)
        {
            E_shieldButton.gameObject.SetActive(false);
        }

    }
    //___________________________________________________________PLATE_UPGRADE_____________________________________________________
    public void P_UpgradeEngine()
    {
        tunButton.Play();
        P_enginePrice = prices[P_EngineLevel];
        P_EngineLevel++;
        PlayerPrefs.SetInt("P_EngineLevel", P_EngineLevel);
        PlayerPrefs.SetFloat("P_enginePower", P_enginepowers[P_EngineLevel]);
        mainScript.SetBrains(-P_enginePrice);
        P_engineSlider.value = PlayerPrefs.GetFloat("P_enginePower", 160.0f);
        P_enginePrice = prices[P_EngineLevel];
        P_engineText.text = P_enginePrice.ToString();

        if (PlayerPrefs.GetInt("P_EngineLevel", 0) == 9)
        {
            P_engineButton.gameObject.SetActive(false);
        }

    }
    public void P_UpgradeRay()
    {
        tunButton.Play();
        P_rayPrice = prices[P_RayLevel];
        P_RayLevel++;
        PlayerPrefs.SetInt("P_RayLevel", P_RayLevel);
        PlayerPrefs.SetFloat("P_rayLiftPower", P_raypowers[P_RayLevel]);
        PlayerPrefs.SetFloat("P_gunPower", P_gunPowers[P_RayLevel]);
        mainScript.SetBrains(-P_rayPrice);
        P_raySlider.value = PlayerPrefs.GetFloat("P_rayLiftPower", 10.0f);
        P_rayPrice = prices[P_RayLevel];
        P_rayText.text = P_rayPrice.ToString();

        if (PlayerPrefs.GetInt("P_RayLevel", 0) == 9)
        {
            P_rayButton.gameObject.SetActive(false);
        }

    }

    public void P_UpgradeTank()
    {
        tunButton.Play();
        P_tankPrice = prices[P_TankLevel];
        P_TankLevel++;
        PlayerPrefs.SetInt("P_TankLevel", P_TankLevel);
        PlayerPrefs.SetFloat("P_maxFuel", P_tankpowers[P_TankLevel]);
        mainScript.SetBrains(-P_tankPrice);
        P_tankSlider.value = PlayerPrefs.GetFloat("P_maxFuel", 40.0f);
        P_tankPrice = prices[P_TankLevel];
        P_tankText.text = P_tankPrice.ToString();

        if (PlayerPrefs.GetInt("P_TankLevel", 0) == 9)
        {
            P_tankButton.gameObject.SetActive(false);
        }

    }

    public void P_UpgradeShield()
    {
        tunButton.Play();
        P_shieldPrice = prices[P_ShieldLevel];
        P_ShieldLevel++;
        PlayerPrefs.SetInt("P_ShieldLevel", P_ShieldLevel);
        PlayerPrefs.SetFloat("P_forceShieldStrength", P_shieldpowers[P_ShieldLevel]);
        mainScript.SetBrains(-P_shieldPrice);
        P_shieldSlider.value = PlayerPrefs.GetFloat("P_forceShieldStrength", 20.0f);
        P_shieldPrice = prices[P_ShieldLevel];
        P_shieldText.text = P_shieldPrice.ToString();

        if (PlayerPrefs.GetInt("P_ShieldLevel", 0) == 9)
        {
            P_shieldButton.gameObject.SetActive(false);
        }

    }

    //_______________________________________________________________________WARSHIP_UPGRADE________________________________________________

    public void WS_UpgradeEngine()
    {
        tunButton.Play();
        WS_enginePrice = WSprices[WS_EngineLevel];
        WS_EngineLevel++;
        PlayerPrefs.SetInt("WS_EngineLevel", WS_EngineLevel);
        PlayerPrefs.SetFloat("WS_enginePower", WS_enginepowers[WS_EngineLevel]);
        mainScript.SetBrains(-WS_enginePrice);
        WS_engineSlider.value = PlayerPrefs.GetFloat("WS_enginePower", 100.0f);
        WS_enginePrice = WSprices[WS_EngineLevel];
        WS_engineText.text = WS_enginePrice.ToString();

        if (PlayerPrefs.GetInt("WS_EngineLevel", 0) == 9)
        {
            WS_engineButton.gameObject.SetActive(false);
        }

    }
    public void WS_UpgradeRay()
    {
        tunButton.Play();
        WS_rayPrice = WSprices[WS_RayLevel];
        WS_RayLevel++;
        PlayerPrefs.SetInt("WS_RayLevel", WS_RayLevel);
        PlayerPrefs.SetFloat("WS_rayLiftPower", WS_raypowers[WS_RayLevel]);
        PlayerPrefs.SetFloat("WS_gunPower", WS_gunPowers[WS_RayLevel]);
        mainScript.SetBrains(-WS_rayPrice);
        WS_raySlider.value = PlayerPrefs.GetFloat("WS_rayLiftPower", 10.0f);
        WS_rayPrice = WSprices[WS_RayLevel];
        WS_rayText.text = WS_rayPrice.ToString();

        if (PlayerPrefs.GetInt("WS_RayLevel", 0) == 9)
        {
            WS_rayButton.gameObject.SetActive(false);
        }

    }

    public void WS_UpgradeTank()
    {
        tunButton.Play();
        WS_tankPrice = WSprices[WS_TankLevel];
        WS_TankLevel++;
        PlayerPrefs.SetInt("WS_TankLevel", WS_TankLevel);
        PlayerPrefs.SetFloat("WS_maxFuel", WS_tankpowers[WS_TankLevel]);
        mainScript.SetBrains(-WS_tankPrice);
        WS_tankSlider.value = PlayerPrefs.GetFloat("WS_maxFuel", 100.0f);
        WS_tankPrice = WSprices[WS_TankLevel];
        WS_tankText.text = WS_tankPrice.ToString();

        if (PlayerPrefs.GetInt("WS_TankLevel", 0) == 9)
        {
            WS_tankButton.gameObject.SetActive(false);
        }

    }

    public void WS_UpgradeShield()
    {
        tunButton.Play();
        WS_shieldPrice = WSprices[WS_ShieldLevel];
        WS_ShieldLevel++;
        PlayerPrefs.SetInt("WS_ShieldLevel", WS_ShieldLevel);
        PlayerPrefs.SetFloat("WS_forceShieldStrength", WS_shieldpowers[WS_ShieldLevel]);
        mainScript.SetBrains(-WS_shieldPrice);
        WS_shieldSlider.value = PlayerPrefs.GetFloat("WS_forceShieldStrength", 20.0f);
        WS_shieldPrice = WSprices[WS_ShieldLevel];
        WS_shieldText.text = WS_shieldPrice.ToString();

        if (PlayerPrefs.GetInt("WS_ShieldLevel", 0) == 9)
        {
            WS_shieldButton.gameObject.SetActive(false);
        }

    }

    //_______________________________________________________________________KNIPPEL_UPGRADE________________________________________________

    public void K_UpgradeEngine()
    {
        tunButton.Play();
        K_enginePrice = Kprices[K_EngineLevel];
        K_EngineLevel++;
        PlayerPrefs.SetInt("K_EngineLevel", K_EngineLevel);
        PlayerPrefs.SetFloat("K_enginePower", K_enginepowers[K_EngineLevel]);
        mainScript.SetBrains(-K_enginePrice);
        K_engineSlider.value = PlayerPrefs.GetFloat("K_enginePower", 100.0f);
        K_enginePrice = Kprices[K_EngineLevel];
        K_engineText.text = K_enginePrice.ToString();

        if (PlayerPrefs.GetInt("K_EngineLevel", 0) == 9)
        {
            K_engineButton.gameObject.SetActive(false);
        }

    }
    public void K_UpgradeRay()
    {
        tunButton.Play();
        K_rayPrice = Kprices[K_RayLevel];
        K_RayLevel++;
        PlayerPrefs.SetInt("K_RayLevel", K_RayLevel);
        PlayerPrefs.SetFloat("K_rayLiftPower", K_raypowers[K_RayLevel]);
        PlayerPrefs.SetFloat("K_gunPower", K_gunPowers[K_RayLevel]);
        mainScript.SetBrains(-K_rayPrice);
        K_raySlider.value = PlayerPrefs.GetFloat("K_rayLiftPower", 10.0f);
        K_rayPrice = Kprices[K_RayLevel];
        K_rayText.text = K_rayPrice.ToString();

        if (PlayerPrefs.GetInt("K_RayLevel", 0) == 9)
        {
            K_rayButton.gameObject.SetActive(false);
        }

    }

    public void K_UpgradeTank()
    {
        tunButton.Play();
        K_tankPrice = Kprices[K_TankLevel];
        K_TankLevel++;
        PlayerPrefs.SetInt("K_TankLevel", K_TankLevel);
        PlayerPrefs.SetFloat("K_maxFuel", K_tankpowers[K_TankLevel]);
        mainScript.SetBrains(-K_tankPrice);
        K_tankSlider.value = PlayerPrefs.GetFloat("K_maxFuel", 50.0f);
        K_tankPrice = Kprices[K_TankLevel];
        K_tankText.text = K_tankPrice.ToString();

        if (PlayerPrefs.GetInt("K_TankLevel", 0) == 9)
        {
            K_tankButton.gameObject.SetActive(false);
        }

    }

    public void K_UpgradeShield()
    {
        tunButton.Play();
        K_shieldPrice = Kprices[K_ShieldLevel];
        K_ShieldLevel++;
        PlayerPrefs.SetInt("K_ShieldLevel", K_ShieldLevel);
        PlayerPrefs.SetFloat("K_forceShieldStrength", K_shieldpowers[K_ShieldLevel]);
        mainScript.SetBrains(-K_shieldPrice);
        K_shieldSlider.value = PlayerPrefs.GetFloat("K_forceShieldStrength", 50.0f);
        K_shieldPrice = Kprices[K_ShieldLevel];
        K_shieldText.text = K_shieldPrice.ToString();

        if (PlayerPrefs.GetInt("K_ShieldLevel", 0) == 9)
        {
            K_shieldButton.gameObject.SetActive(false);
        }

    }

    #endregion
    //___________________________________________________________________BUY_SHIP!!!_________________________________________________________
    #region
    public void BuyWS()
    {
        mainScript.SetMilk(-100);
        buyWSPanel.SetActive(false);
        buyWSButtonObj.SetActive(false);
        PlayerPrefs.SetInt("WSBuy", 1);
    }
    public void BuyK()
    {
        mainScript.SetMilk(-1000);
        buyKPanel.SetActive(false);
        buyKButtonObj.SetActive(false);
        PlayerPrefs.SetInt("KBuy", 1);
    }
    #endregion

    //_______________________________BUY_MONEY
    #region



    public void OpenBuyMoneyPanel()
    {
        buyMoneyPanel.SetActive(true);
    }
    public void CloseBuyMoneyPanel()
    {
        buyMoneyPanel.SetActive(false);
    }

    public void BuyMilk(int milkCount)
    {
        mainScript.SetMilk(milkCount);
    }
    public void BuyBrains(int brainsCount)
    {
        mainScript.SetBrains(brainsCount);
    }
    public void MaxUpgradePlate()
    {
        P_EngineLevel = P_RayLevel = P_TankLevel = P_ShieldLevel = 9;
        PlayerPrefs.SetInt("P_EngineLevel", P_EngineLevel);
        PlayerPrefs.SetFloat("P_enginePower", P_enginepowers[P_EngineLevel]);
        P_engineSlider.value = PlayerPrefs.GetFloat("P_enginePower", 160.0f);

        PlayerPrefs.SetInt("P_RayLevel", P_RayLevel);
        PlayerPrefs.SetFloat("P_rayLiftPower", P_raypowers[P_RayLevel]);
        PlayerPrefs.SetFloat("P_gunPower", P_gunPowers[P_RayLevel]);
        P_raySlider.value = PlayerPrefs.GetFloat("P_rayLiftPower", 10.0f);

        PlayerPrefs.SetInt("P_TankLevel", P_TankLevel);
        PlayerPrefs.SetFloat("P_maxFuel", P_tankpowers[P_TankLevel]);
        P_tankSlider.value = PlayerPrefs.GetFloat("P_maxFuel", 40.0f);

        PlayerPrefs.SetInt("P_ShieldLevel", P_ShieldLevel);
        PlayerPrefs.SetFloat("P_forceShieldStrength", P_shieldpowers[P_ShieldLevel]);
        P_shieldSlider.value = PlayerPrefs.GetFloat("P_forceShieldStrength", 20.0f);
    }
    public void MaxUpgradeWS()
    {
        WS_EngineLevel = WS_RayLevel = WS_TankLevel = WS_ShieldLevel = 9;

        PlayerPrefs.SetInt("WS_EngineLevel", WS_EngineLevel);
        PlayerPrefs.SetFloat("WS_enginePower", WS_enginepowers[WS_EngineLevel]);
        WS_engineSlider.value = PlayerPrefs.GetFloat("WS_enginePower", 160.0f);

        PlayerPrefs.SetInt("WS_RayLevel", WS_RayLevel);
        PlayerPrefs.SetFloat("WS_rayLiftPower", WS_raypowers[WS_RayLevel]);
        PlayerPrefs.SetFloat("WS_gunPower", WS_gunPowers[WS_RayLevel]);
        WS_raySlider.value = PlayerPrefs.GetFloat("WS_rayLiftPower", 10.0f);

        PlayerPrefs.SetInt("WS_TankLevel", WS_TankLevel);
        PlayerPrefs.SetFloat("WS_maxFuel", WS_tankpowers[WS_TankLevel]);
        WS_tankSlider.value = PlayerPrefs.GetFloat("WS_maxFuel", 40.0f);

        PlayerPrefs.SetInt("WS_ShieldLevel", WS_ShieldLevel);
        PlayerPrefs.SetFloat("WS_forceShieldStrength", WS_shieldpowers[WS_ShieldLevel]);
        WS_shieldSlider.value = PlayerPrefs.GetFloat("WS_forceShieldStrength", 20.0f);

        buyWSPanel.SetActive(false);
        buyWSButtonObj.SetActive(false);
        PlayerPrefs.SetInt("WSBuy", 1);
    }

    public void MaxUpgradeK()
    {
        K_EngineLevel = K_RayLevel = K_TankLevel = K_ShieldLevel = 9;

        PlayerPrefs.SetInt("K_EngineLevel", K_EngineLevel);
        PlayerPrefs.SetFloat("K_enginePower", K_enginepowers[K_EngineLevel]);
        K_engineSlider.value = PlayerPrefs.GetFloat("K_enginePower", 160.0f);

        PlayerPrefs.SetInt("K_RayLevel", K_RayLevel);
        PlayerPrefs.SetFloat("K_rayLiftPower", K_raypowers[K_RayLevel]);
        PlayerPrefs.SetFloat("K_gunPower", K_gunPowers[K_RayLevel]);
        K_raySlider.value = PlayerPrefs.GetFloat("K_rayLiftPower", 10.0f);

        PlayerPrefs.SetInt("K_TankLevel", K_TankLevel);
        PlayerPrefs.SetFloat("K_maxFuel", K_tankpowers[K_TankLevel]);
        K_tankSlider.value = PlayerPrefs.GetFloat("K_maxFuel", 40.0f);

        PlayerPrefs.SetInt("K_ShieldLevel", K_ShieldLevel);
        PlayerPrefs.SetFloat("K_forceShieldStrength", K_shieldpowers[K_ShieldLevel]);
        K_shieldSlider.value = PlayerPrefs.GetFloat("K_forceShieldStrength", 20.0f);

        buyKPanel.SetActive(false);
        buyKButtonObj.SetActive(false);
        PlayerPrefs.SetInt("KBuy", 1);
    }
    #endregion

    public void CampaignButtonClick()
    {
        mainScript.mainMenuPanelIndex = 2;
        if (shipIndex == 1 && PlayerPrefs.GetInt("WSBuy", 0) == 0 && rentIndex == 0)
        {
            rentPanel.SetActive(true);
            campaignPanel.SetActive(true);
        }
        if (shipIndex == 2 && PlayerPrefs.GetInt("KBuy", 0) == 0 && rentIndex == 0)
        {
            rentPanel.SetActive(true);
            campaignPanel.SetActive(true);
        }
        else
        {
            campaignPanel.SetActive(true);
        }
        mainScript.checkIfAdsReady();
    }
    

    public void GameButtonClick()
    {
        mainScript.mainMenuPanelIndex = 1;
        if(shipIndex == 1 && PlayerPrefs.GetInt("WSBuy", 0) == 0 && rentIndex == 0 )
        {
            rentPanel.SetActive(true);
            chooseGameStagePanel.SetActive(true);
        }
        if (shipIndex == 2 && PlayerPrefs.GetInt("KBuy", 0) == 0 && rentIndex == 0)
        {
            rentPanel.SetActive(true);
            chooseGameStagePanel.SetActive(true);
        }
        else
        {
            chooseGameStagePanel.SetActive(true);
        }
        mainScript.checkIfAdsReady();
    }
    //__________________________RENT!____________________
    #region
    public void RentExit()
    {
        mainScript.mainMenuPanelIndex = 0;
        clickButton.Play();
        rentPanel.SetActive(false);
        chooseGameStagePanel.SetActive(false);
        campaignPanel.SetActive(false);
    }

    public void RentByMoney()
    {
        clickButton.Play();
        rentIndex = 1;
        rentPanel.SetActive(false);
        mainScript.SetMilk(-10);
    }
    public void RentByAds()
    {
        adsDestination = 1;
        clickButton.Play();
        if (mainScript.ShowRewardedVideo(this))
        {
            Debug.Log("Showing ad");
        }
        else
        {
            Debug.Log("Showing ad failed");
        }
    }
    public void AdsShowed()
    {
        Debug.Log("AdsSkipped");
        if (adsDestination == 1)
        {
            
            rentIndex = 1;
            rentPanel.SetActive(false);
        }
        else if(adsDestination == 2)
        {
            mainScript.RestoreEnergy();
        }
    }
    public void AdsFailed()
    {
        Debug.Log("AdsFailed");
    }
    public void AdsSkipped()
    {
        Debug.Log("AdsSkipped");
    }
#endregion
    public void CloseTheGame()
    {
        PlayerPrefs.DeleteAll();                                                                    //TESTS!!!!!!!!!!!
        Application.Quit();
    }

    public void NextShipButton()
    {
        clickButton.Play();
        shipIndex++;
        switch (shipIndex)
        {
            case -1:
                PlayerPrefs.SetInt("ShipIndex", -1);
                mainScript.ShipIndex = -1;
                nextShipButton.interactable = true;
                previousShipButton.interactable = false;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(true);
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                break;
            case 0:
                PlayerPrefs.SetInt("ShipIndex", 0);
                mainScript.ShipIndex = 0;
                nextShipButton.interactable = true;
                previousShipButton.interactable = true;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(true);
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                break;
            case 1:
                PlayerPrefs.SetInt("ShipIndex", 1);
                mainScript.ShipIndex = 1;
                nextShipButton.interactable = true;
                previousShipButton.interactable = true;
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(true);
                    plateGaragePanel.SetActive(false);
                    if (PlayerPrefs.GetInt("WSBuy", 0) == 0)
                    {
                        buyWSPanel.SetActive(true);
                    }
                    else
                    {
                        buyWSPanel.SetActive(false);
                    }
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                if (PlayerPrefs.GetInt("WSBuy", 0) == 0)
                {
                    buyWSButtonObj.SetActive(true);
                }
                else
                {
                    buyWSButtonObj.SetActive(false);
                }
                break;
            case 2:
                PlayerPrefs.SetInt("ShipIndex", 2);
                mainScript.ShipIndex = 2;
                nextShipButton.interactable = false;
                previousShipButton.interactable = true;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(true);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    if (PlayerPrefs.GetInt("KBuy", 0) == 0)
                    {
                        buyKPanel.SetActive(true);
                    }
                    else
                    {
                        buyKPanel.SetActive(false);
                    }
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                if (PlayerPrefs.GetInt("KBuy", 0) == 0)
                {
                    buyKButtonObj.SetActive(true);
                }
                else
                {
                    buyKButtonObj.SetActive(false);
                }
                break;
        }
    }
    public void PreviousShipButton()
    {
        clickButton.Play();
        shipIndex--;
        switch (shipIndex)
        {
            case -1:
                PlayerPrefs.SetInt("ShipIndex", -1);
                mainScript.ShipIndex = -1;
                nextShipButton.interactable = true;
                previousShipButton.interactable = false;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(true);
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                break;
            case 0:
                PlayerPrefs.SetInt("ShipIndex", 0);
                mainScript.ShipIndex = 0;
                nextShipButton.interactable = true;
                previousShipButton.interactable = true;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(true);
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                break;
            case 1:
                PlayerPrefs.SetInt("ShipIndex", 1);
                mainScript.ShipIndex = 1;
                nextShipButton.interactable = true;
                previousShipButton.interactable = true;
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(true);
                    plateGaragePanel.SetActive(false);
                    if (PlayerPrefs.GetInt("WSBuy", 0) == 0)
                    {
                        buyWSPanel.SetActive(true);
                    }
                    else
                    {
                        buyWSPanel.SetActive(false);
                    }
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                if (PlayerPrefs.GetInt("WSBuy", 0) == 0)
                {
                    buyWSButtonObj.SetActive(true);
                }
                else
                {
                    buyWSButtonObj.SetActive(false);
                }
                break;
            case 2:
                PlayerPrefs.SetInt("ShipIndex", 2);
                mainScript.ShipIndex = 2;
                nextShipButton.interactable = false;
                previousShipButton.interactable = true;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(true);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    if (PlayerPrefs.GetInt("KBuy", 0) == 0)
                    {
                        buyKPanel.SetActive(true);
                    }
                    else
                    {
                        buyKPanel.SetActive(false);
                    }
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                if (PlayerPrefs.GetInt("KBuy", 0) == 0)
                {
                    buyKButtonObj.SetActive(true);
                }
                else
                {
                    buyKButtonObj.SetActive(false);
                }
                break;
        }
    }

    public void OpenGarage()
    {
        menuOpenSound.Play();
        garageIsOpen = true;
        switch (shipIndex)
        {
            case -1:
                nextShipButton.interactable = true;
                previousShipButton.interactable = false;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(true);
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    EGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                break;
            case 0:
                nextShipButton.interactable = true;
                previousShipButton.interactable = true;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(true);
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                break;
            case 1:
                nextShipButton.interactable = true;
                previousShipButton.interactable = true;
                buyKPanel.SetActive(false);
                buyKButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(true);
                    plateGaragePanel.SetActive(false);
                    if (PlayerPrefs.GetInt("WSBuy", 0) == 0)
                    {
                        buyWSPanel.SetActive(true);
                    }
                    else
                    {
                        buyWSPanel.SetActive(false);
                    }
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                if (PlayerPrefs.GetInt("WSBuy", 0) == 0)
                {
                    buyWSButtonObj.SetActive(true);
                }
                else
                {
                    buyWSButtonObj.SetActive(false);
                }
                break;
            case 2:
                nextShipButton.interactable = false;
                previousShipButton.interactable = true;
                buyWSPanel.SetActive(false);
                buyWSButtonObj.SetActive(false);
                if (garageIsOpen)
                {
                    knippelGaragePanel.SetActive(true);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    if (PlayerPrefs.GetInt("KBuy", 0) == 0)
                    {
                        buyKPanel.SetActive(true);
                    }
                    else
                    {
                        buyKPanel.SetActive(false);
                    }
                }
                else
                {
                    knippelGaragePanel.SetActive(false);
                    WSGaragePanel.SetActive(false);
                    plateGaragePanel.SetActive(false);
                    buyKPanel.SetActive(false);
                    buyWSPanel.SetActive(false);
                }
                if (PlayerPrefs.GetInt("KBuy", 0) == 0)
                {
                    buyKButtonObj.SetActive(true);
                }
                else
                {
                    buyKButtonObj.SetActive(false);
                }
                break;
        }
    }

    public void CloseGarage()
    {
        clickButton.Play();
        garageIsOpen = false;
        EGaragePanel.SetActive(false);
        plateGaragePanel.SetActive(false);
        WSGaragePanel.SetActive(false);
        knippelGaragePanel.SetActive(false);
    }

    //____Energy
    #region
        public void SetEnergy()
    {
        mainScript.ConsumeEnergy();
    }
    public void OpenEnergyPanel()
    {
        mainScript.checkIfAdsReady();
        energyPanel.SetActive(true);
    }
    public void CloseEnergyPanel()
    {
        energyPanel.SetActive(false);
    }
    public void EnergyRestoreByAds()
    {
        CloseEnergyPanel();
        adsDestination = 2;
        clickButton.Play();
        if (mainScript.ShowRewardedVideo(this))
        {
            Debug.Log("Showing ad");
        }
        else
        {
            Debug.Log("Showing ad failed");
        }
    }

    #endregion

    //____Settings
    #region

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("EffectsVolume", effectsSlider.value);
        settingsPanel.SetActive(false);
    }
    public void ChangeMusicVolume(float volume)
    {
        float newvalue = Mathf.Log(volume) * 10;
        if (newvalue < -80.0f)
            newvalue = -80.0f;
        mixer.audioMixer.SetFloat("Music", newvalue);
    }
    void MusicVolumeOnStart()
    {
        //musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        
        //Debug.Log(PlayerPrefs.GetFloat("MusicVolume", 0));
        //Debug.Log(musicSlider.value);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        //float musicVolume;
        //mixer.audioMixer.GetFloat("Music", out musicVolume);
        //Debug.Log("mixer"+ musicVolume);

    }
    public void ChangeEffectsVolume(float volume)
    {
        float newvalue = Mathf.Log(volume) * 10;
        if (newvalue < -80.0f)
            newvalue = -80.0f;
        mixer.audioMixer.SetFloat("Effects", newvalue);
    }
    void EffectsVolumeOnStart()
    {
        //mixer.audioMixer.SetFloat("Effects", Mathf.Log(PlayerPrefs.GetFloat("EffectsVolume", 1) * 10));
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1);
    }

    #endregion
}
