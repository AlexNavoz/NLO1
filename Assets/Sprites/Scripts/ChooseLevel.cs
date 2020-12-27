using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    MainScript mainScript;
    playerMoving player;
    ForceShieldScript fs;

    private void Awake()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            /*
            player = collision.gameObject.GetComponent<playerMoving>();
            mainScript.P_fuelLevel = player.currentFuel;
            fs = collision.gameObject.GetComponent<ForceShieldScript>();
            mainScript.P_forceShieldLevel = fs.currentHP;*/
            mainScript.SafeShortPlatePrefs();
            
            SceneManager.LoadScene(2);
        }
            
        
    }
}
