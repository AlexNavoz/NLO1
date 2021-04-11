using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampaignMenuScript : MonoBehaviour
{
    MainScript mainScript;



    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }
    public void CampPanelExit()
    {
        mainScript.mainMenuPanelIndex = 0;
        gameObject.SetActive(false);
    }
    public void OpenCampStage(string stageName)
    {
        SceneManager.LoadScene(stageName);
    }
}
