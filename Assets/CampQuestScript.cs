using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CampQuestScript : MonoBehaviour, AdsListener
{
    //Quest variables
    public int reward;
    public int timeToQuest;
    public int questObjectIndex;
    public int howManyNeed;

    //screen panel
    public Text screenTimeText;
    public Text screenProgressText;

    //quest panel
    public GameObject questPanel;
    public Text questTimeText;
    public Text questProgressText;

    //win panel
    public GameObject winPanel;
    public Text winTimeText;
    public Text winRewardText;
    public GameObject[] winStars;
    public GameObject ceiling;

    //defeat panel
    public GameObject defeatPanel;
    public GameObject[] defeatReasonTextObj;
    bool defeat = false;

    //refuel panel
    public GameObject refuelPanel;
    public Text refuelPrice;
    public Button refuelByMoneyButton;

    //rearm panel
    public GameObject rearmPanel;
    public Text rearmText;
    public Button rearmByMoneyButton;

    //Evacuation
    public GameObject evacButtonObj;
    public GameObject evacPanel;
    public GameObject evacuator;
    public Text evacPrice;
    public Button evacBuyButton;

    //_______________Other
    MainScript mainScript;
    Animator crossfade;
    playerMoving playerMoving;
    GameObject player;
    ForceShieldScript fss;
    float currentTime;
    bool gameIsStarted = false;
    bool alreadyRefuled = false;
    int adsdestination = 0;
    float percent = 100;

    //________Asteroids
    bool victory = false;
    GameObject box;
    BoxScript boxScript;
    float boxPercent;
    int boxStartHP;
    GameObject asteroidsFinish;

    //___Pause
    public GameObject pausePanel;
    public bool isPaused;

    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoving = player.GetComponent<playerMoving>();
        fss = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
        questPanel.SetActive(true);        
        mainScript.peace = true;
        currentTime = (float)timeToQuest;
        screenTimeText.text = questTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
        mainScript.campaignQuestObjCount = 0;
        pausePanel.SetActive(false);


        switch (mainScript.levelIndex)
        {
            case 2:
                asteroidsFinish = GameObject.FindGameObjectWithTag("Finish");
                box = GameObject.FindGameObjectWithTag("box");
                boxScript = box.GetComponent<BoxScript>();
                boxStartHP = boxScript.boxHP;
                screenProgressText.text = questProgressText.text = (boxScript.boxHP * 100/boxStartHP).ToString() + "%";
                break;
            case 3:
                ceiling.SetActive(true);
                mainScript.campaignQisetObjIndex = questObjectIndex;
                screenProgressText.text = questProgressText.text = mainScript.campaignQuestObjCount.ToString() + "/" + howManyNeed.ToString();
                break;
            case 4:
                ceiling.SetActive(true);
                mainScript.campaignQisetObjIndex = questObjectIndex;
                screenProgressText.text = questProgressText.text = mainScript.campaignQuestObjCount.ToString() + "/" + howManyNeed.ToString();
                break;
        }
    }
    private void Update()
    {
        if (gameIsStarted)
        {
            currentTime -= Time.deltaTime;
            screenTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
            percent = currentTime * 100 / timeToQuest;
            if (percent < 25)
            {
                screenTimeText.color = winTimeText.color = new Color(255, 0, 0, 170);
            }
            else if (percent < 50)
            {
                screenTimeText.color = winTimeText.color = new Color(255, 255, 0, 170);
            }
            else if (percent <= 100)
            {
                screenTimeText.color = winTimeText.color = new Color(0, 255, 0, 170);
            }

            switch (mainScript.levelIndex)
            {
                case 2:
                    boxPercent = boxScript.boxHP * 100 / boxStartHP;
                    screenProgressText.text = boxPercent.ToString() + "%";

                    if (currentTime <= 0 || boxPercent <= 0 || playerMoving.isDead|| box == null || playerMoving.currentFuel <= 0 || boxScript.boxIsLost)
                    {
                        if (!defeat && !victory)
                        {
                            defeat = true;
                            Invoke("QuestDefeat", 2.0f);
                        }
                    }
                    else if (playerMoving.currentFuel <= 5 && !alreadyRefuled)
                    {
                        NeedRefuel();
                    }
                    else if (fss.currentHP <= 15 && fss.currentHP >= 5 && !alreadyRefuled)
                    {
                        NeedRearm();
                    }

                    if (boxPercent < 25)
                    {
                        screenProgressText.color = new Color(255, 0, 0, 170);
                    }
                    else if (boxPercent < 50)
                    {
                        screenProgressText.color = new Color(255, 255, 0, 170);
                    }
                    else if (boxPercent <= 100)
                    {
                        screenProgressText.color = new Color(0, 255, 0, 170);
                    }
                    if ((player.transform.position.y-asteroidsFinish.transform.position.y)>15 && !defeat)
                    {
                        victory = true;
                        QuestWin();
                    }

                    break;
                case 3:
                    if (mainScript.campaignQuestObjCount >= howManyNeed && !defeat)
                    {
                        victory = true;
                        ceiling.SetActive(false);
                        evacButtonObj.SetActive(true);
                        mainScript.campaignQuestObjCount = howManyNeed;
                    }
                    screenProgressText.text = mainScript.campaignQuestObjCount.ToString() + "/" + howManyNeed.ToString();

                    if (currentTime <= 0 || playerMoving.currentFuel <= 0 || playerMoving.isDead)
                    {
                        if (!defeat && !victory)
                        {
                            defeat = true;
                            Invoke("QuestDefeat", 2.0f);
                        }
                    }
                    else if (playerMoving.currentFuel <= 5 && !alreadyRefuled)
                    {
                        NeedRefuel();
                    }
                    else if (fss.currentHP <= 15 && fss.currentHP >= 5 && !alreadyRefuled)
                    {
                        NeedRearm();
                    }
                    if (victory && player.transform.position.y > 27)
                    {
                        QuestWin();
                        victory = false;
                    }
                    break;
                case 4:
                    if (mainScript.campaignQuestObjCount >= howManyNeed && !defeat)
                    {
                        victory = true;
                        ceiling.SetActive(false);
                        evacButtonObj.SetActive(true);
                        mainScript.campaignQuestObjCount = howManyNeed;
                    }
                    screenProgressText.text = mainScript.campaignQuestObjCount.ToString() + "/" + howManyNeed.ToString();

                    if (currentTime <= 0 || playerMoving.currentFuel <= 0 || playerMoving.isDead)
                    {
                        if (!defeat && !victory)
                        {
                            defeat = true;
                            Invoke("QuestDefeat", 2.0f);
                        }
                    }
                    else if (playerMoving.currentFuel <= 5 && !alreadyRefuled)
                    {
                        NeedRefuel();
                    }
                    else if (fss.currentHP <= 15 && fss.currentHP >= 5 && !alreadyRefuled)
                    {
                        NeedRearm();
                    }
                    if (victory && player.transform.position.y > 27)
                    {
                        QuestWin();
                        victory = false;
                    }
                    break;
            }
        }
        
    }
    //____WinPanel
    #region
    void QuestWin()
    {
        mainScript.peace = true;
        winPanel.SetActive(true);
        gameIsStarted = false;
        switch (mainScript.levelIndex)
        {
            case 2:
                if (boxPercent < 25)
                {
                    winTimeText.color = new Color(255, 0, 0, 170);
                    winStars[0].SetActive(true);
                    if (PlayerPrefs.GetInt("campStage" + SceneManager.GetActiveScene().name[0], 0) < 1)
                    {
                        PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 1);
                    }
                }
                else if (boxPercent < 50)
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    reward *= 2;
                    winTimeText.color = new Color(255, 255, 0, 170);
                    if (PlayerPrefs.GetInt("campStage" + SceneManager.GetActiveScene().name[0], 0) < 2)
                    {
                        PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 2);
                    }
                }
                else
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    winStars[2].SetActive(true);
                    reward *= 3;
                    winTimeText.color = new Color(0, 255, 0, 170);
                    if (PlayerPrefs.GetInt("campStage" + SceneManager.GetActiveScene().name[0], 0) < 3)
                    {
                        PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 3);
                    }
                }
                winTimeText.text = boxPercent.ToString() + "%";
                winRewardText.text = (mainScript.collection + reward).ToString();
                break;
            case 3:
                if (percent < 25)
                {
                    winStars[0].SetActive(true);
                    if (PlayerPrefs.GetInt("campStage" + SceneManager.GetActiveScene().name[0], 0) < 1)
                    {
                        PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 1);
                    }
                }
                else if (percent < 50)
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    reward *= 2;
                    if (PlayerPrefs.GetInt("campStage" + SceneManager.GetActiveScene().name[0], 0) < 2)
                    {
                        PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 2);
                    }
                }
                else
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    winStars[2].SetActive(true);
                    reward *= 3;
                    if (PlayerPrefs.GetInt("campStage" + SceneManager.GetActiveScene().name[0], 0) < 3)
                    {
                        PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 3);
                    }
                }
                winTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
                winRewardText.text = (mainScript.collection + reward).ToString();
                break;
            case 4:
                if (percent < 25)
                {
                    winStars[0].SetActive(true);
                    if (PlayerPrefs.GetInt("campStage" + SceneManager.GetActiveScene().name[0], 0) < 1)
                    {
                        PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 1);
                    }
                }
                else if (percent < 50)
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    reward *= 2;
                    if (PlayerPrefs.GetInt("campStage" + SceneManager.GetActiveScene().name[0], 0) < 2)
                    {
                        PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 2);
                    }
                }
                else
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    winStars[2].SetActive(true);
                    reward *= 3;
                    if (PlayerPrefs.GetInt("campStage" + SceneManager.GetActiveScene().name[0], 0) < 3)
                    {
                        PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 3);
                    }
                }
                winTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
                winRewardText.text = (mainScript.collection + reward).ToString();
                break;
        }
        mainScript.SetMoney(mainScript.collection + reward);
        mainScript.collection = 0;
    }

    public void WinOkButton()
    {
        StartCoroutine(CrossFade(1));
    }
    public void Restart()
    {
        mainScript.collection = 0;
        StartCoroutine(CrossFade(SceneManager.GetActiveScene().buildIndex));
    }
    #endregion

    //___Evacuation
    #region

    public void EvacPanelOpen()
    {
        mainScript.peace = true;
        evacPanel.SetActive(true);
        mainScript.checkIfAdsReady();
        evacPrice.text = mainScript.collection.ToString();
        if (mainScript.collection > mainScript.allMoney)
        {
            evacPrice.color = new Color(255, 0, 0);
            evacBuyButton.interactable = false;
        }
    }
    public void EvacPanelClose()
    {
        mainScript.peace = false;
        evacPanel.SetActive(false);
    }
    public void Evacuate()
    {
        mainScript.peace = false;
        evacPanel.SetActive(false);
        Instantiate(evacuator, player.transform.position, Quaternion.identity);
    }
    public void BuyEvac()
    {
        mainScript.collection = 0;
        mainScript.peace = false;
        Evacuate();
    }

    public void BuyEvacByAdd()                                                //change after add
    {
        if (mainScript.ShowRewardedVideo(this))
        {
            adsdestination = 3;
            Debug.Log("Showing ad");
        }
        else
        {
            Debug.Log("Showing ad failed");
        }
    }

    #endregion
    void QuestDefeat()
    {
        gameIsStarted = false;
        mainScript.peace = true;
        defeatPanel.SetActive(true);
        switch (mainScript.levelIndex) {
            case 2:
                if (currentTime <= 0)
                {
                    defeatReasonTextObj[2].SetActive(true);
                }
                if (playerMoving.currentFuel <= 0)
                {
                    defeatReasonTextObj[0].SetActive(true);
                }
                if (playerMoving.isDead)
                {
                    defeatReasonTextObj[1].SetActive(true);
                }
                if (box == null || boxPercent <=0)
                {
                    defeatReasonTextObj[3].SetActive(true);
                }
                if (boxScript.boxIsLost)
                {
                    defeatReasonTextObj[4].SetActive(true);
                }
                break;
            case 3:
                if (currentTime <= 0)
                {
                    defeatReasonTextObj[2].SetActive(true);
                }
                if (playerMoving.currentFuel <= 0)
                {
                    defeatReasonTextObj[0].SetActive(true);
                }
                if (playerMoving.isDead)
                {
                    defeatReasonTextObj[1].SetActive(true);
                }

                break;
            case 4:
                if (currentTime <= 0)
                {
                    defeatReasonTextObj[2].SetActive(true);
                }
                if (playerMoving.currentFuel <= 0)
                {
                    defeatReasonTextObj[0].SetActive(true);
                }
                if (playerMoving.isDead)
                {
                    defeatReasonTextObj[1].SetActive(true);
                }

                break;
        }
    }

    //____Refuel and Rearm
    #region
    void NeedRefuel()
    {
        mainScript.peace = true;
        alreadyRefuled = true;
        refuelPanel.SetActive(true);
        refuelPrice.text = (((int)playerMoving.maxFuel - (int)playerMoving.currentFuel)*3).ToString();
        if ((((int)playerMoving.maxFuel - (int)playerMoving.currentFuel) * 3) > mainScript.allMoney)
        {
            refuelPrice.color = new Color(255, 0, 0);
            refuelByMoneyButton.interactable = false;
        }
        mainScript.checkIfAdsReady();
    }
    public void RefuelCloseButton()
    {
        mainScript.peace = false;
        refuelPanel.SetActive(false);
    }
    public void RefuelByMoney()
    {
        mainScript.SetMoney(-((int)playerMoving.maxFuel - (int)playerMoving.currentFuel) * 3);
        playerMoving.currentFuel = playerMoving.maxFuel;
        playerMoving.SetFuelValues();
        refuelPanel.SetActive(false);
        mainScript.peace = false;
    }
    public void RefuelByAds()
    {
        if (mainScript.ShowRewardedVideo(this))
        {
            adsdestination = 1;
            Debug.Log("Showing ad");
        }
        else
        {
            Debug.Log("Showing ad failed");
        }
    }
    void NeedRearm()
    {
        mainScript.peace = true;
        alreadyRefuled = true;
        rearmPanel.SetActive(true);
        rearmText.text = (((int)fss.maxHP - (int)fss.currentHP) * 3).ToString();
        if ((((int)fss.maxHP - (int)fss.currentHP) * 3) > mainScript.allMoney)
        {
            rearmText.color = new Color(255, 0, 0);
            rearmByMoneyButton.interactable = false;
        }
        mainScript.checkIfAdsReady();
    }
    public void RearmCloseButton()
    {
        mainScript.peace = false;
        rearmPanel.SetActive(false);
    }
    public void RearmByMoney()
    {
        mainScript.SetMoney(-((int)fss.maxHP - (int)fss.currentHP) * 3);
        fss.currentHP = fss.maxHP;
        fss.SetHPValue();
        rearmPanel.SetActive(false);
        mainScript.peace = false;
    }
    public void RearmByAds()
    {
        if (mainScript.ShowRewardedVideo(this))
        {
            adsdestination = 2;
            Debug.Log("Showing ad");
        }
        else
        {
            Debug.Log("Showing ad failed");
        }
    }
    #endregion

    //____Pause
    #region
        public void Pause()
    {
        if (!isPaused)
        {
            pausePanel.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }
    public void PauseRestart()
    {
        int sc = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sc);
    }
    public void PauseExit()
    {
        SceneManager.LoadScene(1);
    }
    #endregion
    public void QuestPanelOKButton()
    {
        mainScript.peace = false;
        questPanel.SetActive(false);
        gameIsStarted = true;
    }




    // _________________ads

    public void AdsShowed()
    {
        if (adsdestination == 1)
        {
            Debug.Log("AdsSkipped");
            playerMoving.currentFuel = playerMoving.maxFuel;
            playerMoving.SetFuelValues();
            refuelPanel.SetActive(false);
            mainScript.peace = false;
        }
        else if (adsdestination == 2)
        {
            Debug.Log("AdsSkipped");
            fss.currentHP = fss.maxHP;
            fss.SetHPValue();
            rearmPanel.SetActive(false);
            mainScript.peace = false;
        }
        else if (adsdestination == 3)
        {
            Debug.Log("AdsSkipped");
            Evacuate();
        }

    }

    public void AdsFailed()
    {
        Debug.Log("AdsFailed");
    }
    public void AdsSkipped()
    {
        Debug.Log("AdsSkipped");
    }
    IEnumerator CrossFade(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelIndex);
    }
}
