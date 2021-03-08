using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    MainScript mainScript;
    GameObject player;
    ForceShieldScript fs;
    public GameObject canvas;
    int i = 0;

    Animator crossfade;

    //Buy levels variables
    public GameObject[] blockPanels;
    public Button[] LevelBuyButtons;
    public int[] LevelBuyPrices = new int[] {1000,2000,4000,6000,8000 };

    private void Awake()
    {
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i<blockPanels.Length; i++)
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
                if (LevelBuyPrices[i] > mainScript.allMoney)
                {
                    LevelBuyButtons[i].interactable = false;
                }
            }
        }

        if (collision.gameObject.layer == 10&& i == 0)
        {
            i++;
            canvas.SetActive(true);
            Time.timeScale = 0;
            mainScript.SafeShortPlatePrefs();
            mainScript.SafeShortWSPrefs();  
        }
    }
    public void BuyEarthStage(int stageIndex)
    {
        PlayerPrefs.SetInt("Stage" + stageIndex, 1);
        blockPanels[stageIndex].SetActive(false);
        mainScript.SetMoney(-LevelBuyPrices[stageIndex]);
    }

    public void ChooseEarthStage(int stage)
    {
        Time.timeScale = 1;
        StartCoroutine(CrossFade(stage));
        
    }
    public void ExitChoosePanel()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
        i = 0;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 15, player.transform.position.z);
    }

    IEnumerator CrossFade(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        i = 0;
        SceneManager.LoadScene(levelIndex);
    }
}
