using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GarageScript : MonoBehaviour
{
    MainScript mainScript;
    playerMoving player_moving;
    ForceShieldScript fs;
    public GameObject canvas;
    Animator crossfade;
    public GameObject P_ChoosePanel;
    public GameObject WS_ChoosePanel;
    bool HaveChoosen = false;
    int[] prices = new int[] {100,200,400,800,1500,2000,2000,2000,3000,4000};
    int[] WSprices = new int[] { 200, 400, 800, 1500, 2000, 3000, 4000, 5000, 6000, 7000 };

    //plate variables_______________________________________________________________________________________________

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



    //WarShip variables_________________________________________________________________________________________________________

    //engine
    int WS_EngineLevel;
    int WS_enginePrice;
    public Text WS_engineText;
    public Slider WS_engineSlider;
    public Button WS_engineButton;
    float[] WS_enginepowers = new float[] { 80.0f, 90.0f, 100.0f, 110.0f, 120.0f, 130.0f, 140.0f, 150.0f, 170.0f, 200.0f };

    //Ray
    int WS_RayLevel;
    int WS_rayPrice;
    public Text WS_rayText;
    public Slider WS_raySlider;
    public Button WS_rayButton;
    float[] WS_raypowers = new float[] { 30.0f, 40.0f, 50.0f, 60.0f, 70.0f, 75.0f, 80.0f, 90.0f, 105.0f, 120.0f };

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
    void Start()
    {
        player_moving = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
        fs = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        //plate_______________________________________________________________________________________________________________________________

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
        P_tankSlider.value = PlayerPrefs.GetFloat("P_maxFuel", 30.0f);
        P_TankLevel = PlayerPrefs.GetInt("P_TankLevel", 0);
        P_tankPrice = prices[P_TankLevel];
        P_tankText.text = P_tankPrice.ToString();

        //Shield
        P_shieldSlider.value = PlayerPrefs.GetFloat("P_forceShieldStrength", 20.0f);
        P_ShieldLevel = PlayerPrefs.GetInt("P_ShieldLevel", 0);
        P_shieldPrice = prices[P_ShieldLevel];
        P_shieldText.text = P_shieldPrice.ToString();

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

        //WarShip_____________________________________________________________________________________________________________________

        //engine
        WS_engineSlider.value = PlayerPrefs.GetFloat("WS_enginePower", 120.0f);
        WS_EngineLevel = PlayerPrefs.GetInt("WS_EngineLevel", 0);
        WS_enginePrice = WSprices[WS_EngineLevel];
        WS_engineText.text = WS_enginePrice.ToString();

        //Ray
        WS_raySlider.value = PlayerPrefs.GetFloat("WS_rayLiftPower", 30.0f);
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

        //engine
        if (WS_enginePrice > mainScript.allMoney)
        {
            WS_engineButton.interactable = false;
        }
        else WS_engineButton.interactable = true;
        if (WS_EngineLevel == 9)
        {
            WS_engineButton.gameObject.SetActive(false);
        }


        //ray
        if (WS_rayPrice > mainScript.allMoney)
        {
            WS_rayButton.interactable = false;
        }
        else WS_rayButton.interactable = true;
        if (PlayerPrefs.GetInt("WS_RayLevel", 0) == 9)
        {
            WS_rayButton.gameObject.SetActive(false);
        }

        //tank
        if (WS_tankPrice > mainScript.allMoney)
        {
            WS_tankButton.interactable = false;
        }
        else WS_tankButton.interactable = true;
        if (PlayerPrefs.GetInt("WS_TankLevel", 0) == 9)
        {
            WS_tankButton.gameObject.SetActive(false);
        }

        //shield
        if (WS_shieldPrice > mainScript.allMoney)
        {
            WS_shieldButton.interactable = false;
        }
        else WS_shieldButton.interactable = true;
        if (PlayerPrefs.GetInt("WS_ShieldLevel", 0) == 9)
        {
            WS_shieldButton.gameObject.SetActive(false);
        }

        if (mainScript.ShipIndex == 0)
        {
            P_ChoosePanel.SetActive(false);
            WS_ChoosePanel.SetActive(true);
        }
        if (mainScript.ShipIndex == 1)
        {
            P_ChoosePanel.SetActive(true);
            WS_ChoosePanel.SetActive(false);
        }
    }

    private void Update()
    {
        //_____________________________________________________________________PLATE_________________________________________
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

        //___________________________________________________________________________WARSHIP____________________________________________________

        //engine
        if (WS_enginePrice > mainScript.allMoney)
        {
            WS_engineButton.interactable = false;
        }
        else WS_engineButton.interactable = true;
        if (WS_EngineLevel == 9)
        {
            WS_engineButton.gameObject.SetActive(false);
        }


        //ray
        if (WS_rayPrice > mainScript.allMoney)
        {
            WS_rayButton.interactable = false;
        }
        else WS_rayButton.interactable = true;
        if (PlayerPrefs.GetInt("WS_RayLevel", 0) == 9)
        {
            WS_rayButton.gameObject.SetActive(false);
        }

        //tank
        if (WS_tankPrice > mainScript.allMoney)
        {
            WS_tankButton.interactable = false;
        }
        else WS_tankButton.interactable = true;
        if (PlayerPrefs.GetInt("WS_TankLevel", 0) == 9)
        {
            WS_tankButton.gameObject.SetActive(false);
        }

        //shield
        if (WS_shieldPrice > mainScript.allMoney)
        {
            WS_shieldButton.interactable = false;
        }
        else WS_shieldButton.interactable = true;
        if (PlayerPrefs.GetInt("WS_ShieldLevel", 0) == 9)
        {
            WS_shieldButton.gameObject.SetActive(false);
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
        Time.timeScale = 0.1f;
    }
    public void ExitCanvas()
    {
        Time.timeScale = 1;
        if (HaveChoosen)
        {
            HaveChoosen = false;
            StartCoroutine(CrossFade(SceneManager.GetActiveScene().buildIndex));
        }
        else
        {
            Rigidbody2D playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            playerRb.AddForce(Vector3.up*playerRb.mass*20, ForceMode2D.Impulse);
        }
        canvas.SetActive(false);

    }

    //___________________________________________________________PLATE_UPGRADE_____________________________________________________
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
        PlayerPrefs.SetInt("P_ShieldLevel", P_ShieldLevel);
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

    //_______________________________________________________________________WARSHIP_UPGRADE________________________________________________

    public void WS_UpgradeEngine()
    {
        WS_enginePrice = WSprices[WS_EngineLevel];
        WS_EngineLevel++;
        PlayerPrefs.SetInt("WS_EngineLevel", WS_EngineLevel);
        PlayerPrefs.SetFloat("WS_enginePower", WS_enginepowers[WS_EngineLevel]);
        mainScript.SetMoney(-WS_enginePrice);
        WS_engineSlider.value = PlayerPrefs.GetFloat("WS_enginePower", 120.0f);
        WS_enginePrice = WSprices[WS_EngineLevel];
        WS_engineText.text = WS_enginePrice.ToString();

        player_moving.ReloadWSPrefs();

        if (PlayerPrefs.GetInt("WS_EngineLevel", 0) == 9)
        {
            WS_engineButton.gameObject.SetActive(false);
        }

    }
    public void WS_UpgradeRay()
    {
        WS_rayPrice = WSprices[WS_RayLevel];
        WS_RayLevel++;
        PlayerPrefs.SetInt("WS_RayLevel", WS_RayLevel);
        PlayerPrefs.SetFloat("WS_rayLiftPower", WS_raypowers[WS_RayLevel]);
        mainScript.SetMoney(-WS_rayPrice);
        WS_raySlider.value = PlayerPrefs.GetFloat("WS_rayLiftPower", 30.0f);
        WS_rayPrice = WSprices[WS_RayLevel];
        WS_rayText.text = WS_rayPrice.ToString();

        if (PlayerPrefs.GetInt("WS_RayLevel", 0) == 9)
        {
            WS_rayButton.gameObject.SetActive(false);
        }

    }

    public void WS_UpgradeTank()
    {
        WS_tankPrice = WSprices[WS_TankLevel];
        WS_TankLevel++;
        PlayerPrefs.SetInt("WS_TankLevel", WS_TankLevel);
        PlayerPrefs.SetFloat("WS_maxFuel", WS_tankpowers[WS_TankLevel]);
        mainScript.SetMoney(-WS_tankPrice);
        WS_tankSlider.value = PlayerPrefs.GetFloat("WS_maxFuel", 100.0f);
        WS_tankPrice = WSprices[WS_TankLevel];
        WS_tankText.text = WS_tankPrice.ToString();

        player_moving.ReloadWSPrefs();
        player_moving.SetFuelValues();

        if (PlayerPrefs.GetInt("WS_TankLevel", 0) == 9)
        {
            WS_tankButton.gameObject.SetActive(false);
        }

    }

    public void WS_UpgradeShield()
    {
        WS_shieldPrice = WSprices[WS_ShieldLevel];
        WS_ShieldLevel++;
        PlayerPrefs.SetInt("WS_ShieldLevel", WS_ShieldLevel);
        PlayerPrefs.SetFloat("WS_forceShieldStrength", WS_shieldpowers[WS_ShieldLevel]);
        mainScript.SetMoney(-WS_shieldPrice);
        WS_shieldSlider.value = PlayerPrefs.GetFloat("WS_forceShieldStrength", 20.0f);
        WS_shieldPrice = WSprices[WS_ShieldLevel];
        WS_shieldText.text = WS_shieldPrice.ToString();

        mainScript.LoadWSPrefs();
        fs.SetHPValue();

        if (PlayerPrefs.GetInt("WS_ShieldLevel", 0) == 9)
        {
            WS_shieldButton.gameObject.SetActive(false);
        }

    }
    public void TakeWS()
    {
        HaveChoosen = true;
        mainScript.ShipIndex = 1;
        PlayerPrefs.SetInt("ShipIndex",1);
        P_ChoosePanel.SetActive(true);
        WS_ChoosePanel.SetActive(false);
    }
    public void TakePlate()
    {
        HaveChoosen = true;
        mainScript.ShipIndex = 0;
        PlayerPrefs.SetInt("ShipIndex", 0);
        P_ChoosePanel.SetActive(false);
        WS_ChoosePanel.SetActive(true);       
    }

    IEnumerator CrossFade(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelIndex);
    }
}
