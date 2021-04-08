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

public class QuestScript : MonoBehaviour
{
    public Image targetIcon;
    public Image miniIcon;
    MainScript mainScript;
    public Text howMuchNeedText;
    public Text rewardText;
    public Text progressText;
    public Text timeText;

    public Text questPanelProgress;
    public Text questPanelTime;

    int howMuchNeed;
    int reward;
    //public int objectIndex;

    public GameObject questPanel;
    public bool questCompleted;
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

        if (MainScript.questObjectId != idToLoad)
        {
            MainScript.questObjectId = idToLoad;
            MainScript.questObjectCount = 0;
            thisQuestButton.interactable = true;
        }

        howMuchNeed = getQuestById(idToLoad).targetCount;
        reward = getQuestById(idToLoad).reward;
        MainScript.questObjectIndex = getQuestById(idToLoad).targetIndex;
        targetIcon.sprite = getQuestById(idToLoad).icon;
        miniIcon.sprite = getQuestById(idToLoad).icon;

        loadedQuestId = idToLoad;
    }

    void UpdateQuest()
    {
        int timedQuestIndex = DateTime.Now.DayOfYear * 24 * 60 * 60 +
            (((int)DateTime.Now.TimeOfDay.TotalSeconds) / questPeriod) * questPeriod;

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
        progressText.text = (MainScript.questObjectCount.ToString() + "/" + howMuchNeed.ToString());
        questPanelProgress.text = progressText.text;
        if (MainScript.questObjectCount >= howMuchNeed)
        {
            questCompleted = true;
            MainScript.questObjectCount = howMuchNeed;
        }
        if (questCompleted)
        {
            thisQuestButton.interactable = false;
        }
    }
    public void QuestPanelActivation()
    {
        questPanel.SetActive(true);
    }
    public void QuestPanelExit()
    {
        questPanel.SetActive(false);
    }
}
