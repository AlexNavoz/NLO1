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

    void Start()
    {
        //plate

        P_engineSlider.value = PlayerPrefs.GetFloat("P_enginePower", 120.0f);
        P_EngineLevel = PlayerPrefs.GetInt("P_EngineLevel", 0);
        P_enginePrice = prices[P_EngineLevel];
        P_engineText.text = P_enginePrice.ToString();


        player_moving = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }

    private void Update()
    {
        if(P_enginePrice > mainScript.allMoney|| P_EngineLevel == 9)
        {
            P_engineButton.interactable = false;
        }
        else P_engineButton.interactable = true;

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
}
