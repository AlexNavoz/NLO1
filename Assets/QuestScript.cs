using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UFOQuest
{
    public int targetIndex;
    public int targetCount;
    public int reward;
    public Sprite icon;
};

public class QuestScript : MonoBehaviour, AdsListener
{
    public GameObject buttonCompleteObj;
    public GameObject completeObj;
    public GameObject buttonProgressObj;
    public GameObject progressObj;

    public Image targetIcon;
    public Image miniIcon;
    MainScript mainScript;
    public Text howMuchNeedText;
    public Text rewardText;
    public Text progressText;
    public Text timeText;

    public Text questPanelProgress;
    public Text questPanelTime;

    public GameObject completeTextBtn;

    int howMuchNeed;
    int reward;
    //public int objectIndex;

    public GameObject questPanel;
    public bool questCompleted;
    public bool alreadyRewarded;
    Button thisQuestButton;

    public UFOQuest[] quests;

    int loadedQuestId = 0;

    private void Start()
    {
        thisQuestButton = GetComponent<Button>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }

    const int questPeriod = 60 * 60;
    UFOQuest getQuestById(int questid)
    {
        return quests[(questid / questPeriod) % quests.Length];
    }
    void LoadQuest(int idToLoad) {

        if (mainScript.questObjectId != idToLoad)
        {
            mainScript.questObjectId = idToLoad;
            mainScript.questObjectCount = 0;
            thisQuestButton.interactable = true;
        }

        howMuchNeed = getQuestById(idToLoad).targetCount;
        reward = getQuestById(idToLoad).reward;
        mainScript.questObjectIndex = getQuestById(idToLoad).targetIndex;
        targetIcon.sprite = getQuestById(idToLoad).icon;
        miniIcon.sprite = getQuestById(idToLoad).icon;

        loadedQuestId = idToLoad;
    }

    void refreshQuest()
    {
        mainScript.questObjectCount = 0;
        mainScript.questIdSeed += (questPeriod * UnityEngine.Random.Range(1, quests.Length));
        
        UpdateQuest();
        mainScript.SaveQuestPrefs();
        buttonCompleteObj.SetActive(false);
        completeObj.SetActive(false);
        progressObj.SetActive(true);
        buttonProgressObj.SetActive(true);
        alreadyRewarded = false;
        questCompleted = false;
    }

    void UpdateQuest()
    {
        int timedQuestIndex = ((DateTime.Now.DayOfYear * 24 * 60 * 60 + (int)DateTime.Now.TimeOfDay.TotalSeconds + mainScript.questIdSeed) / questPeriod) * questPeriod;

        if(timedQuestIndex != loadedQuestId)
            LoadQuest(timedQuestIndex);
    }

    private void LateUpdate()
    {
        //MainScript.questObjectIndex = objectIndex;
        UpdateQuest();

        int timeToNextQuest = ((((int)DateTime.Now.TimeOfDay.TotalSeconds) / questPeriod) + 1) * questPeriod - 1 - (int)DateTime.Now.TimeOfDay.TotalSeconds;
        timeText.text = questPanelTime.text = (timeToNextQuest / (60 * 60)).ToString("D2") + ":" + ((timeToNextQuest / 60 ) % 60).ToString("D2") + ":" + (timeToNextQuest % 60).ToString("D2");

        rewardText.text = reward.ToString();
        progressText.text = (mainScript.questObjectCount.ToString() + "/" + howMuchNeed.ToString());
        questPanelProgress.text = progressText.text;
        {
            if (mainScript.questObjectCount >= howMuchNeed)
            {
                questCompleted = true;
                mainScript.questObjectCount = howMuchNeed;
                buttonCompleteObj.SetActive(true);
                completeObj.SetActive(true);
                progressObj.SetActive(false);
                buttonProgressObj.SetActive(false);
            }
            else
            {
                buttonCompleteObj.SetActive(false);
                completeObj.SetActive(false);
                progressObj.SetActive(true);
                buttonProgressObj.SetActive(true);
            }
        }
        if (questCompleted && !alreadyRewarded)
        {
            alreadyRewarded = true;
            mainScript.SetMoney(reward);
            //rewardText.text = completeTextBtn.GetComponent<Text>().text;
        }
    }
    public void QuestPanelActivation()
    {
        questPanel.SetActive(true);
        mainScript.peace = true;
    }
    public void QuestPanelExit()
    {
        questPanel.SetActive(false);
        mainScript.peace = false;
    }

    public void Change()
    {
        if (mainScript.ShowRewardedVideo(this))
        {
            Debug.Log("Showing ad");
        }
        else
        {
            Debug.Log("Showing ad failed");
        }
    }
    public void AdsShowed()
    {
        Debug.Log("AdsSkipped");
        refreshQuest();
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
