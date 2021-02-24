using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    //plate variables
    public float P_enginePower;
    public float P_maxFuel;
    public float P_rayDacreaserPower;
    public float P_rayLiftPower;
    public float P_forceShieldStrength;

    //WS variables
    public float WS_enginePower;
    public float WS_maxFuel;
    public float WS_rayDacreaserPower;
    public float WS_rayLiftPower;
    public float WS_forceShieldStrength;

    //plate short time variables
    public float P_fuelLevel;
    public float P_forceShieldLevel;
    public int P_cowCount;

    //WS short time variables
    public float WS_fuelLevel;
    public float WS_forceShieldLevel;
    public int WS_cowCount;

    //starting game
    Vector3 startPos;
    Transform startPosTransform;
    public GameObject plate;
    public GameObject warship;
    public int ShipIndex;
    public int levelIndex = 0;
    playerMoving player;
    ForceShieldScript fs;
    public bool shieldIsActive;
    public int[] postLevels = null;

    //money variables
    string lname;
    public int collection;
    public int allMoney;
    public Text text;
    public GameObject canvas;

    public static float forceBatchingMultiplier = 0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        LoadPlatePrefs();
        LoadShortPlatePrefs();
        LoadWSPrefs();
        LoadShortWSPrefs();
        
        ShipIndex = PlayerPrefs.GetInt("ShipIndex",0);
        allMoney = PlayerPrefs.GetInt("allMoney", 1500);
        text.text = allMoney.ToString();
        CanvasOrNotCanvas();

        forceBatchingMultiplier = (0.02f / Time.fixedDeltaTime);
    }
    public void MoreMoney()
    {
        SetMoney(1000);
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
    }

    public void SetMoney(int money)
    {
        allMoney += money;
        PlayerPrefs.SetInt("allMoney", allMoney);
        text.text = allMoney.ToString();
    }

    public void CanvasOrNotCanvas()
    {
        lname = SceneManager.GetActiveScene().name;
        if (lname != "Main menu")
        {
            canvas.SetActive(false);
        }
        else canvas.SetActive(true);
    }

    //__________________________________________________________________________PLATE______________________________________________________________________
    public void LoadPlatePrefs()
    {
        P_enginePower = PlayerPrefs.GetFloat("P_enginePower", 120.0f);
        P_maxFuel = PlayerPrefs.GetFloat("P_maxFuel", 30.0f);
        //P_rayDacreaserPower = PlayerPrefs.GetFloat("P_rayDecreaserPower", 0.1f);
        P_rayLiftPower = PlayerPrefs.GetFloat("P_rayLiftPower", 30.0f);
        P_forceShieldStrength = PlayerPrefs.GetFloat("P_forceShieldStrength", 20.0f);
    }
    public void LoadShortPlatePrefs()
    {
        P_fuelLevel = PlayerPrefs.GetFloat("P_fuelLevel", P_maxFuel);
        P_forceShieldLevel = PlayerPrefs.GetFloat("P_forceShieldLevel", P_forceShieldStrength);
        P_cowCount = PlayerPrefs.GetInt("P_cowCount", 0);
    }

    public void SafeShortPlatePrefs()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
        P_fuelLevel = player.currentFuel;
        if (P_fuelLevel > 0)
        {
            PlayerPrefs.SetFloat("P_fuelLevel", P_fuelLevel);
        }
        else
            PlayerPrefs.SetFloat("P_fuelLevel", 1.0f);
        if (shieldIsActive)
        {
            fs = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
            P_forceShieldLevel = fs.currentHP;
            PlayerPrefs.SetFloat("P_forceShieldLevel", P_forceShieldLevel);
        }
        else
            PlayerPrefs.SetFloat("P_forceShieldLevel", 1.0f);
        
        PlayerPrefs.SetInt("P_cowCount", P_cowCount);
    }

    //__________________________________________________________________________________WARSHIP__________________________________________________________


    public void LoadWSPrefs()
    {
        WS_enginePower = PlayerPrefs.GetFloat("WS_enginePower", 80.0f);
        WS_maxFuel = PlayerPrefs.GetFloat("WS_maxFuel", 40.0f);
        //WS_rayDacreaserPower = PlayerPrefs.GetFloat("WS_rayDecreaserPower", 0.1f);
        WS_rayLiftPower = PlayerPrefs.GetFloat("WS_rayLiftPower", 30.0f);
        WS_forceShieldStrength = PlayerPrefs.GetFloat("WS_forceShieldStrength", 40.0f);
    }
    public void LoadShortWSPrefs()
    {
        WS_fuelLevel = PlayerPrefs.GetFloat("WS_fuelLevel", WS_maxFuel);
        WS_forceShieldLevel = PlayerPrefs.GetFloat("WS_forceShieldLevel", WS_forceShieldStrength);
        WS_cowCount = PlayerPrefs.GetInt("WS_cowCount", 0);
    }

    public void SafeShortWSPrefs()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
        WS_fuelLevel = player.currentFuel;
        if (WS_fuelLevel > 0)
        {
            PlayerPrefs.SetFloat("WS_fuelLevel", WS_fuelLevel);
        }
        else
            PlayerPrefs.SetFloat("WS_fuelLevel", 1.0f);
        if (shieldIsActive)
        {
            fs = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
            WS_forceShieldLevel = fs.currentHP;
            PlayerPrefs.SetFloat("WS_forceShieldLevel", WS_forceShieldLevel);
        }
        else
            PlayerPrefs.SetFloat("WS_forceShieldLevel", 1.0f);

        PlayerPrefs.SetInt("WS_cowCount", WS_cowCount);
    }
    public void PostLevels()
    {
        PostScript postScript = GameObject.FindGameObjectWithTag("Post").GetComponent<PostScript>();
        postLevels = new int[postScript.a_Stages.Length];
        for (int i = 0; i < postScript.a_Stages.Length; i++)
        {
            postLevels[i] = PlayerPrefs.GetInt("A_Stage" + i, 0);
        }
    }
}
