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

    //refuel panel
    public GameObject refuelPanel;
    public Text refuelPrice;

    //rearm panel
    public GameObject rearmPanel;
    public Text rearmText;

    //_______________Other
    MainScript mainScript;
    playerMoving playerMoving;
    ForceShieldScript fss;
    float currentTime;
    bool gameIsStarted = false;
    bool alreadyRefuled = false;
    int adsdestination = 0;

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
                    screenProgressText.text = mainScript.campaignQuestObjCount.ToString() + "/" + howManyNeed.ToString();
                    if (currentTime <= 0 || playerMoving.currentFuel <= 0 || playerMoving.isDead)
                    {
                        QuestDefeat();
                    }
                    else if (playerMoving.currentFuel <= 5 && !alreadyRefuled)
                    {
                        NeedRefuel();
                    }
                    else if (fss.currentHP <= 10)
                    {
                        NeedRearm();
                    }
                    break;
            }
        }
        
    }

    void QuestWin()
    {
        mainScript.peace = true;
        winPanel.SetActive(true);

    }
    void QuestDefeat()
    {
        mainScript.peace = true;
        defeatPanel.SetActive(false);
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
    void NeedRefuel()
    {
        mainScript.peace = true;
        alreadyRefuled = true;
        refuelPanel.SetActive(true);
        refuelPrice.text = (((int)playerMoving.maxFuel - (int)playerMoving.currentFuel)*3).ToString();
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
    }
    public void RearmByMoney()
    {
        mainScript.SetMoney(-((int)fss.maxHP - (int)fss.currentHP) * 3);
        fss.maxHP = fss.currentHP;
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
            fss.maxHP = fss.currentHP;
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
