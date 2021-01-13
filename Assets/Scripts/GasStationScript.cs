using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasStationScript : MonoBehaviour
{
    public GameObject canvas;
    public Text text;
    int price;
    MainScript mainScript;
    playerMoving playerScript;
    ForceShieldScript forceShield;

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
        forceShield = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            canvas.SetActive(true);
            price = (int)((playerScript.maxFuel - playerScript.currentFuel)+(forceShield.maxHP - forceShield.currentHP));
            text.text = price.ToString();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            //if(canvas)
            canvas.SetActive(false);
        }
    }

    public void Refuel()
    {
        mainScript.SetMoney(-price);
        price = 0;
        text.text = "Full Tank!!";
        playerScript.currentFuel = playerScript.maxFuel;
        forceShield.currentHP = forceShield.maxHP;
        //change after garage
        mainScript.SafeShortPlatePrefs();

        forceShield.SetHPValue();
        playerScript.SetFuelValues();
        
        
    }
     public void Exit()
    {
        canvas.SetActive(false);
    }
}
