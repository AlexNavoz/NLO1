using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBlockScript : MonoBehaviour
{
    public string trainingLevelName;
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 13|| collision.gameObject.layer == 10)
        {
            Debug.Log("est contakt");
            SceneManager.LoadScene(trainingLevelName);
        }
    }
}
