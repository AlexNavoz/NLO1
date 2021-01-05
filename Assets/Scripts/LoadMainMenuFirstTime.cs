using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuFirstTime : MonoBehaviour
{

    void Start()
    {
        Invoke("LoadMainMenu", 2);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
