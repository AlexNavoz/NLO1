using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    MainScript mainScript;
    GameObject player;
    MainMenuScript MMscript;

    Animator crossfade;

    //Buy levels variables
    public GameObject[] blockPanels;
    public Button[] LevelBuyButtons;
    public int[] LevelBuyPrices = new int[] {1000,2000,4000,6000,8000 };

    private void Awake()
    {
        MMscript = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenuScript>();
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        for (int i = 0; i < blockPanels.Length; i++)
        {
            if (blockPanels[i] != null)
            {
                if (PlayerPrefs.GetInt("Stage" + i, 0) == 1)
                {
                    blockPanels[i].SetActive(false);
                }
            }
            if (LevelBuyButtons[i] != null)
            {
                if (LevelBuyPrices[i] > mainScript.allMilk)
                {
                    LevelBuyButtons[i].interactable = false;
                }
            }
        }
    }
    public void BuyEarthStage(int stageIndex)
    {
        PlayerPrefs.SetInt("Stage" + stageIndex, 1);
        blockPanels[stageIndex].SetActive(false);
        mainScript.SetMilk(-LevelBuyPrices[stageIndex]);
    }

    public void ChooseEarthStage(int stage)
    {
        if (mainScript.GetCurrentEnergy() < 1)
        {
            MMscript.OpenEnergyPanel();
        }
        else
        {
            mainScript.ConsumeEnergy();
            StartCoroutine(CrossFade(stage));
        }
    }
    public void ExitChoosePanel()
    {
        mainScript.mainMenuPanelIndex = 0;
        gameObject.SetActive(false);
    }

    IEnumerator CrossFade(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelIndex);
    }
}
