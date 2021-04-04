using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBlockScript : MonoBehaviour
{
    public string[] trainingLevelName;
    MainScript mainScript;
    playerMoving playerScript;
    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 13|| collision.gameObject.layer == 10)
        {
            SceneManager.LoadScene(trainingLevelName[Random.Range(0,trainingLevelName.Length)]);
        }
    }
}
