using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour, AdsListener
{
    MainScript mainScript;
    GameObject mainCameraObj;
    bool garageIsOpen;

    public Text moneyText;
    int currentMoney;

    public AudioSource tunButton;
    public AudioSource clickButton;                       //Sounds
    public AudioSource menuOpenSound;

    //Campaign
    public GameObject campaignPanel;

    //ShipRent
    public GameObject rentPanel;
    public Button rentByMoneyButton;
    int rentIndex = 0;

    public Button nextShipButton;
    public Button previousShipButton;
    public GameObject plateGaragePanel;
    public GameObject WSGaragePanel;
    public GameObject knippelGaragePanel;
    public Transform[] transformPoints;
    public GameObject buyMoneyPanel;
    public GameObject chooseGameStagePanel;

    int[] prices = new int[] { 200, 400, 800, 1500, 2000, 3000, 4000, 5000, 6000, 7000 };
    int[] WSprices = new int[] { 300, 600, 1000, 2000, 3000, 5000, 6000, 7000, 8000, 10000 };
    int[] Kprices = new int[] { 500, 1000, 2000, 4000, 6000, 8000, 10000, 15000, 20000, 30000 };

    //Buy ship

    public GameObject buyWSPanel;
    public Button buyWSButton;
    public GameObject buyWSButtonObj;
    public GameObject buyKPanel;
    public Button buyKButton;
    public GameObject buyKButtonObj;


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
    float[] P_raypowers = new float[] { 6.0f, 7.0f, 8.0f, 10.0f, 12.0f, 14.0f, 16.0f, 18.0f, 19.0f, 20.0f };
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
        mainScript.questButton.SetActive(false);
        rentPanel.SetActive(false);
        
        #region

        //plate_______________________________________________________________________________________________________________________________

        //engine
        P_engineSlider.value = PlayerPrefs.GetFloat("P_enginePower", 120.0f);
        P_EngineLevel = PlayerPrefs.GetInt("P_EngineLevel", 0);
        P_enginePrice = prices[P_EngineLevel];
        P_engineText.text = P_enginePrice.ToString();

        //Ray
        P_raySlider.value = PlayerPrefs.GetFloat("P_rayLiftPower", 6.0f);
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
        shipIndex = PlayerPrefs.GetInt("ShipIndex", 0);
        switch (shipIndex)
        {
            case 0:
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

        switch (mainScript.mainMenuPanelIndex)
        {
            case 1:
                GameButtonClick();
                break;
            case 2:
                CampaignButtonClick();
                break;
        }
        currentMoney = mainScript.allMoney;
        moneyText.text = currentMoney.ToString();

    }

    float changingPercent = 0;
    private void Update()
    {
        mainCameraObj.transform.position = Vector3.Lerp(mainCameraObj.transform.position, transformPoints[shipIndex].position, 0.05f);

        //MoneyButton
        bool doupdate = false; // Не обновляем значение лишний раз чтобы не грузить
        if (currentMoney != mainScript.allMoney && changingPercent < 1.0f)
        {
            changingPercent += Time.deltaTime / (1.0f); // Подгоночный коэффицент, количество секунд для прокрутки денег
            doupdate = true;
        }
        else {
            if (changingPercent != 0)
                doupdate = true;
            currentMoney = mainScript.allMoney;
            changingPercent = 0;
        }
        if(doupdate)
            moneyText.text = ((int)Mathf.Lerp((float)currentMoney,(float)mainScript.allMoney,changingPercent)).ToString();

        //Buy ships
        if (mainScript.allMoney < 10000)
        {
            buyWSButton.interactable = false;
        }
        else buyWSButton.interactable = true;


        //_______________________Knippel Buy

        if (mainScript.allMoney < 100000)
        {
            buyKButton.interactable = false;
        }
        else buyKButton.interactable = true;

        if (mainScript.allMoney < 1000)
        {
            rentByMoneyButton.interactable = false;
        }
        else rentByMoneyButton.interactable = true;

        #region

        //_____________________________________________________________________PLATE_________________________________________
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
        //___________________________________________________________________________Knippel____________________________________________________

        //engine
        if (K_enginePrice > mainScript.allMoney)
        {
            K_engineButton.interactable = false;
        }
        else K_engineButton.interactable = true;
        if (K_EngineLevel == 9)
        {
            K_engineButton.gameObject.SetActive(false);
        }


        //ray
        if (K_rayPrice > mainScript.allMoney)
        {
            K_rayButton.interactable = false;
        }
        else K_rayButton.interactable = true;
        if (PlayerPrefs.GetInt("K_RayLevel", 0) == 9)
        {
            K_rayButton.gameObject.SetActive(false);
        }

        //tank
        if (K_tankPrice > mainScript.allMoney)
        {
            K_tankButton.interactable = false;
        }
        else K_tankButton.interactable = true;
        if (PlayerPrefs.GetInt("K_TankLevel", 0) == 9)
        {
            K_tankButton.gameObject.SetActive(false);
        }

        //shield
        if (K_shieldPrice > mainScript.allMoney)
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

    //___________________________________________________TUNNING!!!_________________________________________________________________________________
    #region

    //___________________________________________________________PLATE_UPGRADE_____________________________________________________
    public void P_UpgradeEngine()
    {
        tunButton.Play();
        P_enginePrice = prices[P_EngineLevel];
        P_EngineLevel++;
        PlayerPrefs.SetInt("P_EngineLevel", P_EngineLevel);
        PlayerPrefs.SetFloat("P_enginePower", P_enginepowers[P_EngineLevel]);
        mainScript.SetMoney(-P_enginePrice);
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
        mainScript.SetMoney(-P_rayPrice);
        P_raySlider.value = PlayerPrefs.GetFloat("P_rayLiftPower", 6.0f);
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
        mainScript.SetMoney(-P_tankPrice);
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
        mainScript.SetMoney(-P_shieldPrice);
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
        mainScript.SetMoney(-WS_enginePrice);
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
        mainScript.SetMoney(-WS_rayPrice);
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
        mainScript.SetMoney(-WS_tankPrice);
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
        mainScript.SetMoney(-WS_shieldPrice);
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
        mainScript.SetMoney(-K_enginePrice);
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
        mainScript.SetMoney(-K_rayPrice);
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
        mainScript.SetMoney(-K_tankPrice);
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
        mainScript.SetMoney(-K_shieldPrice);
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
        mainScript.SetMoney(-10000);
        buyWSPanel.SetActive(false);
        buyWSButtonObj.SetActive(false);
        PlayerPrefs.SetInt("WSBuy", 1);
    }
    public void BuyK()
    {
        mainScript.SetMoney(-100000);
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
    public void BuyMoneyByAds()
    {
        mainScript.SetMoney(500);
        //AfterAds                                                                                                                                  AfterAds!!!
    }
    public void BuyMoneyByMoney(int reward)
    {
        mainScript.SetMoney(reward);
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
        mainScript.SetMoney(-1000);
    }
    public void RentByAds()
    {
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
        rentIndex = 1;
        rentPanel.SetActive(false);
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
        //PlayerPrefs.DeleteAll();                                                                    //TESTS!!!!!!!!!!!
        Application.Quit();
    }

    public void NextShipButton()
    {
        clickButton.Play();
        shipIndex++;
        switch (shipIndex)
        {
            case 0:
                PlayerPrefs.SetInt("ShipIndex", 0);
                mainScript.ShipIndex = 0;
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
            case 0:
                PlayerPrefs.SetInt("ShipIndex", 0);
                mainScript.ShipIndex = 0;
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
            case 0:
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
        plateGaragePanel.SetActive(false);
        WSGaragePanel.SetActive(false);
        knippelGaragePanel.SetActive(false);
    }
    }
