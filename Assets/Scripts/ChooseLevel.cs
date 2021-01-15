using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    MainScript mainScript;
    playerMoving player;
    ForceShieldScript fs;
    public GameObject canvas;

    private void Awake()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            canvas.SetActive(true);
            Time.timeScale = 0;
            mainScript.SafeShortPlatePrefs();
            
            
        }
            
        
    }

    public void ChooseEarthStage(int stage)
    {
        SceneManager.LoadScene(stage);
        Time.timeScale = 1;
    }
}
