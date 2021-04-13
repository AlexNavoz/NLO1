using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CampaignMenuScript : MonoBehaviour
{
    MainScript mainScript;
    public GameObject[] campStages;
    public Button[] campButtons;



    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.CampLevels();
        StarsSwitch();
    }

    void StarsSwitch()
    {
        for (int i = 0; i < campStages.Length; i++)
        {
            Transform stars = campStages[i].transform.Find("Stars");
            GameObject star1 = stars.Find("Star1").gameObject;
            GameObject star2 = stars.Find("Star2").gameObject;
            GameObject star3 = stars.Find("Star3").gameObject;
            switch (mainScript.campLevels == null ? 0 : mainScript.campLevels[i])
            {
                case 3:
                    star3.SetActive(true);
                    goto case 2;
                case 2:
                    star2.SetActive(true);
                    goto case 1;
                case 1:
                    star1.SetActive(true);
                    if ((i + 1) < campButtons.Length)
                        campButtons[i + 1].interactable = true;
                    break;
                default:
                    if ((i + 1) < campButtons.Length)
                        campButtons[i + 1].interactable = false;
                    break;
            }
        }
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