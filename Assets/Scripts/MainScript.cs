﻿using System.Collections;
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

    //plate short time variables
    public float P_fuelLevel;
    public float P_forceShieldLevel;
    public int P_cowCount;
    playerMoving player;
    ForceShieldScript fs;
    public bool shieldIsActive;


    //starting game
    Vector3 startPos;
    Transform startPosTransform;
    public GameObject plate;
    public GameObject warship;

    //money variables
    string lname;
    public int collection;
    public int allMoney;
    public Text text;
    public GameObject canvas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        LoadPlatePrefs();
        LoadShortPlatePrefs();

        allMoney = PlayerPrefs.GetInt("allMoney", 1000);
        text.text = allMoney.ToString();
        CanvasOrNotCanvas();

    }

    public void LoadPlatePrefs()
    {
        P_enginePower = PlayerPrefs.GetFloat("P_enginePower", 250.0f);
        P_maxFuel = PlayerPrefs.GetFloat("P_maxFuel", 120.0f);
        P_rayDacreaserPower = PlayerPrefs.GetFloat("P_rayDecreaserPower", 0.1f);
        P_rayLiftPower = PlayerPrefs.GetFloat("P_rayLiftPower", 50.0f);
        P_forceShieldStrength = PlayerPrefs.GetFloat("P_forceShieldStrength", 100.0f);
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
        if (shieldIsActive)
        {
            fs = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
            P_forceShieldLevel = fs.currentHP;
            PlayerPrefs.SetFloat("P_forceShieldLevel", P_forceShieldLevel);
        }
        else
            PlayerPrefs.SetFloat("P_forceShieldLevel", 1.0f);
        PlayerPrefs.SetFloat("P_fuelLevel", P_fuelLevel);
        
        PlayerPrefs.SetInt("P_cowCount", P_cowCount);
    }
    public void StartOnPosition()
    {
        startPosTransform = GameObject.FindGameObjectWithTag("StartPosition").GetComponent<Transform>();
        startPos = new Vector3(startPosTransform.position.x, startPosTransform.position.y, startPosTransform.position.z);

        //change after garage menu
        Instantiate(plate, startPos, Quaternion.identity);
    }

    public void SetMoney (int money)
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

}