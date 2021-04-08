using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuFirstTime : MonoBehaviour
{

    void Start()
    {
        Invoke("LoadMainMenu", 1);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
        /*  if(PlayerPrefs.GetInt("TutorialCompleted",0)==1)
          SceneManager.LoadScene(1);
          else
          {
              SceneManager.LoadScene("FirstStartStage");
          }*/
    }
}
