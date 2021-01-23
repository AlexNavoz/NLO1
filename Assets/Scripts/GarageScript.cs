using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageScript : MonoBehaviour
{
    MainScript mainScript;
    playerMoving player_moving;
    ForceShieldScript fs;
    public GameObject canvas;
    int[] prices = new int[] {100,200,400,800,1500,2000,2000,2000,2000,2000};
    //plate variables

    //engine
    int P_EngineLevel;
    int P_enginePrice;
    public Text P_engineText; 
    public Slider P_engineSlider;
    public Button P_engineButton;
    float[] P_enginepowers = new float [] { 120.0f, 130.0f, 140.0f, 150.0f, 170.0f, 200.0f, 220.0f, 240.0f, 270.0f, 300.0f };

    //Ray
    int P_RayLevel;
    int P_rayPrice;
    public Text P_rayText;
    public Slider P_raySlider;
    public Button P_rayButton;
    float[] P_raypowers = new float[] {30.0f, 35.0f, 40.0f, 45.0f, 50.0f, 55.0f, 60.0f, 65.0f, 75.0f, 90.0f };

    //Tank
    int P_TankLevel;
    int P_tankPrice;
    public Text P_tankText;
    public Slider P_tankSlider;
    public Button P_tankButton;
    float[] P_tankpowers = new float[] { 30.0f, 40.0f, 50.0f, 60.0f, 70.0f, 80.0f, 90.0f, 100.0f, 110.0f, 130.0f };

    //Shield
    int P_ShieldLevel;
    int P_shieldPrice;
    public Text P_shieldText;
    public Slider P_shieldSlider;
    public Button P_shieldButton;
    float[] P_shieldpowers = new float[] { 20.0f, 30.0f, 50.0f, 70.0f, 100.0f, 130.0f, 160.0f, 190.0f, 240.0f, 300.0f };
    void Start()
    {
        //plate

        //engine
        P_engineSlider.value = PlayerPrefs.GetFloat("P_enginePower", 120.0f);
        P_EngineLevel = PlayerPrefs.GetInt("P_EngineLevel", 0);
        P_enginePrice = prices[P_EngineLevel];
        P_engineText.text = P_enginePrice.ToString();

        //Ray
        P_raySlider.value = PlayerPrefs.GetFloat("P_rayLiftPower", 30.0f);
        P_RayLevel = PlayerPrefs.GetInt("P_RayLevel", 0);
        P_rayPrice = prices[P_RayLevel];
        P_rayText.text = P_rayPrice.ToString();

        //Tank
        P_tankSlider.value = PlayerPrefs.GetFloat("P_maxFuel", 100.0f);
        P_TankLevel = PlayerPrefs.GetInt("P_TankLevel", 0);
        P_tankPrice = prices[P_TankLevel];
        P_tankText.text = P_tankPrice.ToString();

        //Shield
        P_shieldSlider.value = PlayerPrefs.GetFloat("P_forceShieldStrength", 20.0f);
        P_ShieldLevel = PlayerPrefs.GetInt("P_ShieldLevel", 0);
        P_shieldPrice = prices[P_ShieldLevel];
        P_shieldText.text = P_shieldPrice.ToString();


        player_moving = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
        fs = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();

        //engine
        if (P_enginePrice > mainScript.allMoney)
        {
            P_engineButton.interactable = false;
        }
        else P_engineButton.interactable = true;
        if (P_EngineLevel == 9)
        {
            P_engineButton.gameObject.SetActive(false);
        }
        

        //ray
        if (P_rayPrice > mainScript.allMoney)
        {
            P_rayButton.interactable = false;
        }
        else P_rayButton.interactable = true;
        if (PlayerPrefs.GetInt("P_RayLevel", 0) == 9)
        {
            P_rayButton.gameObject.SetActive(false);
        }

        //tank
        if (P_tankPrice > mainScript.allMoney)
        {
            P_tankButton.interactable = false;
        }
        else P_tankButton.interactable = true;
        if (PlayerPrefs.GetInt("P_TankLevel", 0) == 9)
        {
            P_tankButton.gameObject.SetActive(false);
        }

        //shield
        if (P_shieldPrice > mainScript.allMoney)
        {
            P_shieldButton.interactable = false;
        }
        else P_shieldButton.interactable = true;
        if (PlayerPrefs.GetInt("P_ShieldLevel", 0) == 9)
        {
            P_shieldButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //engine
        if(P_enginePrice > mainScript.allMoney)
        {
            P_engineButton.interactable = false;
        }
        else P_engineButton.interactable = true;

        if (P_EngineLevel == 9)
        {
            P_engineButton.gameObject.SetActive(false);
        }


        //ray
        if (P_rayPrice > mainScript.allMoney)
        {
            P_rayButton.interactable = false;
        }
        else P_rayButton.interactable = true;

        if (PlayerPrefs.GetInt("P_RayLevel", 0) == 9)
        {
            P_rayButton.gameObject.SetActive(false);
        }

        //tank
        if (P_tankPrice > mainScript.allMoney)
        {
            P_tankButton.interactable = false;
        }
        else P_tankButton.interactable = true;
        if (P_TankLevel == 9)
        {
            P_tankButton.gameObject.SetActive(false);
        }

        //shield
        if (P_shieldPrice > mainScript.allMoney)
        {
            P_shieldButton.interactable = false;
        }
        else P_shieldButton.interactable = true;
        if (P_ShieldLevel == 9)
        {
            P_shieldButton.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("CanvasSetActive", 1.0f);
        }
    }

    void CanvasSetActive()
    {
        canvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void ExitCanvas()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);

    }

    public void P_UpgradeEngine()
    {
        P_enginePrice = prices[P_EngineLevel];
        P_EngineLevel++;
        PlayerPrefs.SetInt("P_EngineLevel", P_EngineLevel);
        PlayerPrefs.SetFloat("P_enginePower", P_enginepowers[P_EngineLevel]);
        mainScript.SetMoney(-P_enginePrice);
        P_engineSlider.value = PlayerPrefs.GetFloat("P_enginePower", 120.0f);
        P_enginePrice = prices[P_EngineLevel];
        P_engineText.text = P_enginePrice.ToString();

        player_moving.ReloadPlatePrefs();

        if (PlayerPrefs.GetInt("P_EngineLevel", 0) == 9)
        {
            P_engineButton.gameObject.SetActive(false);
        }

    }
    public void P_UpgradeRay()
    {
        P_rayPrice = prices[P_RayLevel];
        P_RayLevel++;
        PlayerPrefs.SetInt("P_RayLevel", P_RayLevel);
        PlayerPrefs.SetFloat("P_rayLiftPower", P_raypowers[P_RayLevel]);
        mainScript.SetMoney(-P_rayPrice);
        P_raySlider.value = PlayerPrefs.GetFloat("P_rayLiftPower", 30.0f);
        P_rayPrice = prices[P_RayLevel];
        P_rayText.text = P_rayPrice.ToString();

        if (PlayerPrefs.GetInt("P_RayLevel", 0) == 9)
        {
            P_rayButton.gameObject.SetActive(false);
        }

    }

    public void P_UpgradeTank()
    {
        P_tankPrice = prices[P_TankLevel];
        P_TankLevel++;
        PlayerPrefs.SetInt("P_TankLevel", P_TankLevel);
        PlayerPrefs.SetFloat("P_maxFuel", P_tankpowers[P_TankLevel]);
        mainScript.SetMoney(-P_tankPrice);
        P_tankSlider.value = PlayerPrefs.GetFloat("P_maxFuel", 100.0f);
        P_tankPrice = prices[P_TankLevel];
        P_tankText.text = P_tankPrice.ToString();

        player_moving.ReloadPlatePrefs();
        player_moving.SetFuelValues();

        if (PlayerPrefs.GetInt("P_TankLevel", 0) == 9)
        {
            P_tankButton.gameObject.SetActive(false);
        }

    }

    public void P_UpgradeShield()
    {
        P_shieldPrice = prices[P_ShieldLevel];
        P_ShieldLevel++;
        PlayerPrefs.SetInt("P_ShieldLevel", P_TankLevel);
        PlayerPrefs.SetFloat("P_forceShieldStrength", P_shieldpowers[P_ShieldLevel]);
        mainScript.SetMoney(-P_shieldPrice);
        P_shieldSlider.value = PlayerPrefs.GetFloat("P_forceShieldStrength", 20.0f);
        P_shieldPrice = prices[P_ShieldLevel];
        P_shieldText.text = P_shieldPrice.ToString();

        mainScript.LoadPlatePrefs();
        fs.SetHPValue();

        if (PlayerPrefs.GetInt("P_ShieldLevel", 0) == 9)
        {
            P_shieldButton.gameObject.SetActive(false);
        }

    }
}
