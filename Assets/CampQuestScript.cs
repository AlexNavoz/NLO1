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

    //_______________Other
    MainScript mainScript;
    playerMoving playerMoving;
    ForceShieldScript fss;
    float currentTime;
    bool gameIsStarted = false;
    bool alreadyRefuled = false;
    int adsdestination = 0;
    float percent;
    public bool victory;

    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        playerMoving = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
        fss = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
        questPanel.SetActive(true);
        mainScript.peace = true;

        currentTime = (float)timeToQuest;
        screenTimeText.text = questTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");

        mainScript.campaignQuestObjCount = 0;
        screenProgressText.text = questProgressText.text = mainScript.campaignQuestObjCount.ToString() + "/" + howManyNeed.ToString();

        mainScript.campaignQisetObjIndex = questObjectIndex;

        switch (mainScript.levelIndex)
        {
            case 2:
                break;
            case 3:
                break;
        }
    }
    private void Update()
    {
        if (gameIsStarted)
        {
            currentTime -= Time.deltaTime;
            screenTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
            switch (mainScript.levelIndex)
            {
                case 2:
                    break;
                case 3:
                    percent = timeToQuest * 100 / currentTime;
                    screenProgressText.text = mainScript.campaignQuestObjCount.ToString() + "/" + howManyNeed.ToString();
                    if (currentTime <= 0 || playerMoving.currentFuel <= 0 || playerMoving.isDead)
                    {
                        if (!defeat)
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
                    if(howManyNeed == mainScript.campaignQuestObjCount)
                    {
                        QuestWin();
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
                break;
            case 3:
                if (percent < 25)
                {
                    winStars[0].SetActive(true);
                    PlayerPrefs.SetInt("campStage" + SceneManager.GetActiveScene().name[0], 1);
                }
                else if (percent < 50)
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    PlayerPrefs.SetInt("A_Stage" + SceneManager.GetActiveScene().name[0], 2);
                }
                else
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    winStars[2].SetActive(true);
                    PlayerPrefs.SetInt("A_Stage" + SceneManager.GetActiveScene().name[0], 3);
                }
                winTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
                winRewardText.text = (mainScript.collection + reward).ToString();
                break;
        }
        mainScript.SetMoney(mainScript.collection + reward);
        mainScript.collection = 0;
    }
    #endregion
    void QuestDefeat()
    {
        gameIsStarted = false;
        mainScript.peace = true;
        defeatPanel.SetActive(true);
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
    }

    public void AdsFailed()
    {
        Debug.Log("AdsFailed");
    }
    public void AdsSkipped()
    {
        Debug.Log("AdsSkipped");
    }
}
