using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageScript : MonoBehaviour
{
    MainScript mainScript;
    playerMoving player_moving;
    public GameObject canvas;
    int[] prices = new int[] {100,200,400,800,1500,2000,2000,2000,2000,2000};
    //plate variables
    int P_EngineLevel;
    int P_enginePrice;
    public Text P_engineText; 
    public Slider P_engineSlider;
    public Button P_engineButton;
    float[] P_enginepowers = new float [] { 120.0f, 130.0f, 140.0f, 150.0f, 170.0f, 200.0f, 220.0f, 240.0f, 270.0f, 300.0f };
    int P_RayLevel;
    int P_rayPrice;
    public Text P_rayText;
    public Slider P_raySlider;
    public Button P_rayButton;
    float[] P_raypowers = new float[] {30.0f, 35.0f, 40.0f, 45.0f, 50.0f, 55.0f, 60.0f, 65.0f, 75.0f, 85.0f };

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



        player_moving = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }

    private void Update()
    {
        //engine
        if(P_enginePrice > mainScript.allMoney|| P_EngineLevel > 9)
        {
            P_engineButton.interactable = false;
        }
        else P_engineButton.interactable = true;

        //ray
        if (P_rayPrice > mainScript.allMoney || P_RayLevel > 9)
        {
            P_rayButton.interactable = false;
        }
        else P_rayButton.interactable = true;
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
    }
    public void ExitCanvas()
    {
        canvas.SetActive(false);

    }

    public void P_UpgradeEngine()
    {

        PlayerPrefs.SetInt("P_EngineLevel", P_EngineLevel++);
        PlayerPrefs.SetFloat("P_enginePower", P_enginepowers[P_EngineLevel]);
        mainScript.SetMoney(-P_enginePrice);
        P_engineSlider.value = PlayerPrefs.GetFloat("P_enginePower", 120.0f);
        P_enginePrice = prices[P_EngineLevel];
        P_engineText.text = P_enginePrice.ToString();

        player_moving.ReloadPlatePrefs();
        
    }
    public void P_UpgradeRay()
    {

        PlayerPrefs.SetInt("P_RayLevel", P_RayLevel++);
        PlayerPrefs.SetFloat("P_rayLiftPower", P_raypowers[P_RayLevel]);
        mainScript.SetMoney(-P_rayPrice);
        P_raySlider.value = PlayerPrefs.GetFloat("P_rayLiftPower", 30.0f);
        P_rayPrice = prices[P_RayLevel];
        P_rayText.text = P_rayPrice.ToString();

    }
}
