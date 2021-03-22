using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GarageScript : MonoBehaviour
{
    public AudioSource tunButton;
    MainScript mainScript;
    playerMoving player_moving;
    ForceShieldScript fs;
    public GameObject canvas;
    Animator crossfade;
    public GameObject P_ChoosePanel;
    public GameObject WS_ChoosePanel;
    public GameObject K_ChoosePanel;
    bool HaveChoosen = false;
    int[] prices = new int[] {100,200,400,800,1500,2000,2000,2000,3000,4000};
    int[] WSprices = new int[] { 200, 400, 800, 1500, 2000, 3000, 4000, 5000, 6000, 7000 };
    int[] Kprices = new int[] { 400, 800, 1500, 2000, 3000, 5000, 7000, 10000, 15000, 20000 };

    //Buy ship

    public GameObject buyWSPanel;
    public Button buyWSButton;
    public GameObject buyKPanel;
    public Button buyKButton;


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

    //Knippel variables_________________________________________________________________________________________________________

    //engine
    int K_EngineLevel;
    int K_enginePrice;
    public Text K_engineText;
    public Slider K_engineSlider;
    public Button K_engineButton;
    float[] K_enginepowers = new float[] { 80.0f, 90.0f, 100.0f, 110.0f, 120.0f, 130.0f, 140.0f, 150.0f, 170.0f, 200.0f };

    //Ray
    int K_RayLevel;
    int K_rayPrice;
    public Text K_rayText;
    public Slider K_raySlider;
    public Button K_rayButton;
    float[] K_raypowers = new float[] { 30.0f, 40.0f, 50.0f, 60.0f, 70.0f, 80.0f, 100.0f, 120.0f, 150.0f, 200.0f };

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
        WS_engineSlider.value = PlayerPrefs.GetFloat("WS_enginePower", 80.0f);
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
        //Knippel_____________________________________________________________________________________________________________________

        //engine
        K_engineSlider.value = PlayerPrefs.GetFloat("K_enginePower", 80.0f);
        K_EngineLevel = PlayerPrefs.GetInt("K_EngineLevel", 0);
        K_enginePrice = Kprices[K_EngineLevel];
        K_engineText.text = K_enginePrice.ToString();

        //Ray
        K_raySlider.value = PlayerPrefs.GetFloat("K_rayLiftPower", 30.0f);
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
        //+_________________________________________________________+

        if (mainScript.ShipIndex == 0)
        {
            P_ChoosePanel.SetActive(false);
            WS_ChoosePanel.SetActive(true);
            K_ChoosePanel.SetActive(true);
        }
        if (mainScript.ShipIndex == 1)
        {
            P_ChoosePanel.SetActive(true);
            WS_ChoosePanel.SetActive(false);
            K_ChoosePanel.SetActive(true);
        }
        if (mainScript.ShipIndex == 2)
        {
            P_ChoosePanel.SetActive(true);
            WS_ChoosePanel.SetActive(true);
            K_ChoosePanel.SetActive(false);
        }
    }

    private void Update()
    {
        //Buy ships
        if(mainScript.allMoney < 10000)
        {
            buyWSButton.interactable = false;
        }
        else buyWSButton.interactable = true;

        if (PlayerPrefs.GetInt("WSBuy", 0) == 0)
        {
            buyWSPanel.SetActive(true);
        }
        else buyWSPanel.SetActive(false);

        //_______________________Knippel Buy

        if (mainScript.allMoney < 100000)
        {
            buyKButton.interactable = false;
        }
        else buyKButton.interactable = true;

        if (PlayerPrefs.GetInt("KBuy", 0) == 0)
        {
            buyKPanel.SetActive(true);
        }
        else buyKPanel.SetActive(false);

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
        Time.timeScale = 1;
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
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            player.transform.rotation = new Quaternion(0,0,0,0);
            playerRb.AddForce(Vector3.up*playerRb.mass*50, ForceMode2D.Impulse);
        }
        canvas.SetActive(false);

    }

    //___________________________________________________________PLATE_UPGRADE_____________________________________________________
    public void P_UpgradeEngine()
    {
        tunButton.Play();
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
        tunButton.Play();
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
        tunButton.Play();
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
        tunButton.Play();
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
        tunButton.Play();
        WS_enginePrice = WSprices[WS_EngineLevel];
        WS_EngineLevel++;
        PlayerPrefs.SetInt("WS_EngineLevel", WS_EngineLevel);
        PlayerPrefs.SetFloat("WS_enginePower", WS_enginepowers[WS_EngineLevel]);
        mainScript.SetMoney(-WS_enginePrice);
        WS_engineSlider.value = PlayerPrefs.GetFloat("WS_enginePower", 80.0f);
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
        tunButton.Play();
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
        tunButton.Play();
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
        tunButton.Play();
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

    //_______________________________________________________________________KNIPPEL_UPGRADE________________________________________________

    public void K_UpgradeEngine()
    {
        tunButton.Play();
        K_enginePrice = Kprices[K_EngineLevel];
        K_EngineLevel++;
        PlayerPrefs.SetInt("K_EngineLevel", K_EngineLevel);
        PlayerPrefs.SetFloat("K_enginePower", K_enginepowers[K_EngineLevel]);
        mainScript.SetMoney(-K_enginePrice);
        K_engineSlider.value = PlayerPrefs.GetFloat("K_enginePower", 80.0f);
        K_enginePrice = Kprices[K_EngineLevel];
        K_engineText.text = K_enginePrice.ToString();

        player_moving.ReloadKPrefs();

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
        mainScript.SetMoney(-K_rayPrice);
        K_raySlider.value = PlayerPrefs.GetFloat("K_rayLiftPower", 30.0f);
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

        player_moving.ReloadKPrefs();
        player_moving.SetFuelValues();

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

        mainScript.LoadKPrefs();
        fs.SetHPValue();

        if (PlayerPrefs.GetInt("K_ShieldLevel", 0) == 9)
        {
            K_shieldButton.gameObject.SetActive(false);
        }

    }
    public void TakeK()
    {
        tunButton.Play();
        HaveChoosen = true;
        mainScript.ShipIndex = 2;
        PlayerPrefs.SetInt("ShipIndex", 2);
        P_ChoosePanel.SetActive(true);
        WS_ChoosePanel.SetActive(true);
        K_ChoosePanel.SetActive(false);
    }
    public void TakeWS()
    {
        tunButton.Play();
        HaveChoosen = true;
        mainScript.ShipIndex = 1;
        PlayerPrefs.SetInt("ShipIndex",1);
        P_ChoosePanel.SetActive(true);
        WS_ChoosePanel.SetActive(false);
        K_ChoosePanel.SetActive(true);
    }
    public void TakePlate()
    {
        tunButton.Play();
        HaveChoosen = true;
        mainScript.ShipIndex = 0;
        PlayerPrefs.SetInt("ShipIndex", 0);
        P_ChoosePanel.SetActive(false);
        WS_ChoosePanel.SetActive(true);
        K_ChoosePanel.SetActive(true);
    }

    IEnumerator CrossFade(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelIndex);
    }

    public void BuyWS()
    {
        mainScript.SetMoney(-10000);
        buyWSPanel.SetActive(false);
        PlayerPrefs.SetInt("WSBuy", 1);
    }
    public void BuyK()
    {
        mainScript.SetMoney(-100000);
        buyKPanel.SetActive(false);
        PlayerPrefs.SetInt("KBuy", 1);
    }
}
