using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasStationScript : MonoBehaviour
{
    public GameObject canvas;
    public Text text;
    int price;
    int i = 0;
    MainScript mainScript;
    playerMoving playerScript;
    ForceShieldScript forceShield;
    public Button refuelByMoneyButton;

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
            price = (int)((playerScript.maxFuel - playerScript.currentFuel) + (forceShield.maxHP - forceShield.currentHP));
            i++;
            if (i == 1 && price!=0)
            {
                Invoke("EnterToStation", 1.0f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            i = 0;
        }
    }
    public void RefuelByMoney()
    {
        mainScript.SetMoney(-price);
        price = 0;
        playerScript.currentFuel = playerScript.maxFuel;
        forceShield.currentHP = forceShield.maxHP;
        //change after garage
        mainScript.SafeShortPlatePrefs();
        mainScript.SafeShortWSPrefs();

        forceShield.SetHPValue();
        playerScript.SetFuelValues();
        Exit();
        
        
    }
    public void RefuelByAds()
    {
        price = 0;
        playerScript.currentFuel = playerScript.maxFuel;
        forceShield.currentHP = forceShield.maxHP;
        //change after garage
        mainScript.SafeShortPlatePrefs();
        mainScript.SafeShortWSPrefs();

        forceShield.SetHPValue();
        playerScript.SetFuelValues();
        Exit();
    }

    void EnterToStation()
    {
        Time.timeScale = 0;
        canvas.SetActive(true);
        price = (int)((playerScript.maxFuel - playerScript.currentFuel) + (forceShield.maxHP - forceShield.currentHP));
        text.text = price.ToString();
        if (price > mainScript.allMoney)
        {
            text.color = new Color(255, 0, 0);
            refuelByMoneyButton.interactable = false;
        }
        else
        {
            text.color = new Color(255, 255, 0);
            refuelByMoneyButton.interactable = true;
        }
    }
     public void Exit()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
    }
}
