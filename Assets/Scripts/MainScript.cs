using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    //starting game
    Vector3 startPos;
    Transform startPosTransform;
    public GameObject plate;
    public GameObject warship;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        LoadPlatePrefs();
    }

    public void LoadPlatePrefs()
    {
        P_enginePower = PlayerPrefs.GetFloat("P_enginePower", 120.0f);
        P_maxFuel = PlayerPrefs.GetFloat("P_maxFuel", 120.0f);
        P_rayDacreaserPower = PlayerPrefs.GetFloat("P_rayDecreaserPower", 0.1f);
        P_rayLiftPower = PlayerPrefs.GetFloat("P_rayLiftPower", 50.0f);
        P_forceShieldStrength = PlayerPrefs.GetFloat("P_forceShieldStrength", 100.0f);

        P_fuelLevel = P_maxFuel;
    }
   
    public void StartOnPosition()
    {
        startPosTransform = GameObject.FindGameObjectWithTag("StartPosition").GetComponent<Transform>();
        startPos = new Vector3(startPosTransform.position.x, startPosTransform.position.y, startPosTransform.position.z);
        Instantiate(plate, startPos, Quaternion.identity);
    }

}
