using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppsFlyerSDK;

public class CampQuestScript : MonoBehaviour, AdsListener
{
    //Quest variables
    public int milkReward;
    public int brainsReward;
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
    public Text winMilkRewardText;
    public Text winBrainsRewardText;
    public GameObject[] winStars;
    public GameObject ceiling;
    public GameObject pointerUp;

    //defeat panel
    public GameObject defeatPanel;
    public GameObject[] defeatReasonTextObj;
    bool defeat = false;
    bool defeatSwithed = false;

    //refuel panel
    public GameObject refuelPanel;
    public Text refuelPrice;
    public Button refuelByMoneyButton;
    int priceOfRefuel;

    //rearm panel
    public GameObject rearmPanel;
    public Text rearmText;
    public Button rearmByMoneyButton;
    int priceOfRearm;

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

    //____Claw
    public bool objectIsInClaw = false;
    public GameObject greenLamp;
    public GameObject redLamp;

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
        pointerUp.SetActive(false);


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
            case 5:
                ceiling.SetActive(true);
                screenProgressText.text = questProgressText.text = null;
                redLamp.SetActive(true);
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
                        if (!defeat)
                        {
                            victory = false;
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
                        pointerUp.SetActive(true);
                        victory = true;
                        ceiling.SetActive(false);
                        evacButtonObj.SetActive(true);
                        mainScript.campaignQuestObjCount = howManyNeed;
                    }
                    screenProgressText.text = mainScript.campaignQuestObjCount.ToString() + "/" + howManyNeed.ToString();

                    if (currentTime <= 0 || playerMoving.currentFuel <= 0 || playerMoving.isDead)
                    {
                        if (!defeat)
                        {
                            victory = false;
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
                    if (victory && player.transform.position.y > 24)
                    {
                        QuestWin();
                        victory = false;
                    }
                    break;
                case 4:
                    if (mainScript.campaignQuestObjCount >= howManyNeed && !defeat)
                    {
                        pointerUp.SetActive(true);
                        victory = true;
                        ceiling.SetActive(false);
                        evacButtonObj.SetActive(true);
                        mainScript.campaignQuestObjCount = howManyNeed;
                    }
                    screenProgressText.text = mainScript.campaignQuestObjCount.ToString() + "/" + howManyNeed.ToString();

                    if (currentTime <= 0 || playerMoving.currentFuel <= 0 || playerMoving.isDead)
                    {
                        if (!defeat)
                        {
                            victory = false;
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
                    if (victory && player.transform.position.y > 24)
                    {
                        QuestWin();
                        victory = false;
                    }
                    break;
                case 5:
                    {
                        if (objectIsInClaw && !defeat)
                        {
                            pointerUp.SetActive(true);
                            victory = true;
                            ceiling.SetActive(false);
                            evacButtonObj.SetActive(true);
                            greenLamp.SetActive(true);
                            redLamp.SetActive(false);
                        }
                        else
                        {
                            pointerUp.SetActive(false);
                            victory = false;
                            ceiling.SetActive(true);
                            evacButtonObj.SetActive(false);
                            greenLamp.SetActive(false);
                            redLamp.SetActive(true);
                        }
                    }

                    if (currentTime <= 0 || playerMoving.currentFuel <= 0 || playerMoving.isDead)
                    {
                        if (!defeat)
                        {
                            victory = false;
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
                    if (victory && player.transform.position.y > 23)
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
    int GetCurrentLevelNumb() {
        string levelname = SceneManager.GetActiveScene().name;
        int levelnum;
        if (int.TryParse(levelname.Substring(0, 3), out levelnum))
        {
            return levelnum;
        }
        if (int.TryParse(levelname.Substring(0, 2), out levelnum))
        {
            return levelnum;
        }
        if (int.TryParse(levelname.Substring(0, 1), out levelnum))
        {
            return levelnum;
        }
        return 0;
    }
    void QuestWin()
    {
        mainScript.peace = true;
        winPanel.SetActive(true);
        gameIsStarted = false;
        int rewardmultiplier = 0;
        int previousreward = 0;


        switch (mainScript.levelIndex)
        {
            case 2:
                if (boxPercent < 25)
                {
                    winTimeText.color = new Color(255, 0, 0, 170);
                    winStars[0].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 1)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 1);
                    }
                    rewardmultiplier = 1 - previousreward;
                }
                else if (boxPercent < 50)
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    winTimeText.color = new Color(255, 255, 0, 170);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 2)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 2);
                    }
                    rewardmultiplier = 2 - previousreward;
                }
                else
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    winStars[2].SetActive(true);
                    winTimeText.color = new Color(0, 255, 0, 170);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 3)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 3);
                    }
                    rewardmultiplier = 3 - previousreward;
                }
                winTimeText.text = boxPercent.ToString() + "%";
                winMilkRewardText.text = (mainScript.milkCollection + milkReward).ToString();
                winBrainsRewardText.text = (mainScript.brainsCollection + brainsReward).ToString();
                break;
            case 3:
                if (percent < 25)
                {
                    winStars[0].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 1)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 1);
                    }
                    rewardmultiplier = 1 - previousreward;
                }
                else if (percent < 50)
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 2)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 2);
                    }
                    rewardmultiplier = 2 - previousreward;
                }
                else
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    winStars[2].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 3)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 3);
                    }
                    rewardmultiplier = 3 - previousreward;
                }
                winTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
                winMilkRewardText.text = (mainScript.milkCollection + milkReward).ToString();
                winBrainsRewardText.text = (mainScript.brainsCollection + brainsReward).ToString();
                break;
            case 4:
                if (percent < 25)
                {
                    winStars[0].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 1)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 1);
                    }
                    rewardmultiplier = 1 - previousreward;
                }
                else if (percent < 50)
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 2)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 2);
                    }
                    rewardmultiplier = 2 - previousreward;
                }
                else
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    winStars[2].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 3)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 3);
                    }
                    rewardmultiplier = 3 - previousreward;
                }
                winTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
                winMilkRewardText.text = (mainScript.milkCollection + milkReward).ToString();
                winBrainsRewardText.text = (mainScript.brainsCollection + brainsReward).ToString();
                break;
            case 5:
                if (percent < 25)
                {
                    winStars[0].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 1)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 1);
                    }
                    rewardmultiplier = 1 - previousreward;
                }
                else if (percent < 50)
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 2)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 2);
                    }
                    rewardmultiplier = 2 - previousreward;
                }
                else
                {
                    winStars[0].SetActive(true);
                    winStars[1].SetActive(true);
                    winStars[2].SetActive(true);
                    if ((previousreward = PlayerPrefs.GetInt("campStage" + GetCurrentLevelNumb(), 0)) < 3)
                    {
                        PlayerPrefs.SetInt("campStage" + GetCurrentLevelNumb(), 3);
                    }
                    rewardmultiplier = 3 - previousreward;
                }
                winTimeText.text = (((int)currentTime / 60) % 60).ToString("D2") + ":" + ((int)currentTime % 60).ToString("D2");
                winMilkRewardText.text = (mainScript.milkCollection + milkReward).ToString();
                winBrainsRewardText.text = (mainScript.brainsCollection + brainsReward).ToString();
                break;
        }


        {
            Dictionary<string, string> LevelAchievedEvent = new Dictionary<string, string>();
            LevelAchievedEvent.Add("af_level", GetCurrentLevelNumb().ToString());
            LevelAchievedEvent.Add("af_score", previousreward.ToString());
            AppsFlyer.sendEvent("af_level_achieved", LevelAchievedEvent);
        }

        milkReward *= rewardmultiplier;
        brainsReward *= rewardmultiplier;
        mainScript.SetMilk(mainScript.milkCollection + milkReward);
        mainScript.SetBrains(mainScript.brainsCollection + brainsReward);
        mainScript.milkCollection = 0;
        mainScript.brainsCollection = 0;
    }

    public void WinOkButton()
    {
        StartCoroutine(CrossFade(1));
    }
    public void Restart()
    {
        if (mainScript.GetCurrentEnergy() < 1)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            mainScript.milkCollection = 0;
            mainScript.brainsCollection = 0;
            StartCoroutine(CrossFade(SceneManager.GetActiveScene().buildIndex));
        }
    }
    #endregion

    //___Evacuation
    #region

    public void EvacPanelOpen()
    {
        mainScript.peace = true;
        evacPanel.SetActive(true);
        mainScript.checkIfAdsReady();
        evacPrice.text = (mainScript.brainsCollection).ToString();
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
        mainScript.brainsCollection = 0;
        mainScript.peace = false;
        Evacuate();

        {
            Dictionary<string, string> LevelAchievedEvent = new Dictionary<string, string>();
            LevelAchievedEvent.Add("af_level", GetCurrentLevelNumb().ToString());
            AppsFlyer.sendEvent("EvacuateByMoney", LevelAchievedEvent);
        }
    }

    public void BuyEvacByAdd()                                                //change after add
    {
        {
            Dictionary<string, string> LevelAchievedEvent = new Dictionary<string, string>();
            LevelAchievedEvent.Add("af_level", GetCurrentLevelNumb().ToString());
            AppsFlyer.sendEvent("EvacuateByAds", LevelAchievedEvent);
        }

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
        if (!defeatSwithed)
        {

            {
                Dictionary<string, string> LevelAchievedEvent = new Dictionary<string, string>();
                LevelAchievedEvent.Add("af_level", GetCurrentLevelNumb().ToString());
                AppsFlyer.sendEvent("CampaignDefeat", LevelAchievedEvent);
            }
            defeatSwithed = true;
            mainScript.milkCollection = 0;
            mainScript.brainsCollection = 0;
            gameIsStarted = false;
            mainScript.peace = true;
            defeatPanel.SetActive(true);
            switch (mainScript.levelIndex)
            {
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
                    if (box == null || boxPercent <= 0)
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
                case 5:
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
    }

    //____Refuel and Rearm
    #region
    void NeedRefuel()
    {
        
        mainScript.peace = true;
        alreadyRefuled = true;
        refuelPanel.SetActive(true);
        priceOfRefuel = mainScript.milkCollection / 2;
        if(priceOfRefuel < 10)
        {
            priceOfRefuel = 10;
        }
        refuelPrice.text = priceOfRefuel.ToString();
        if (priceOfRefuel > mainScript.allMilk)
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
        mainScript.SetMilk(-priceOfRefuel);
        playerMoving.currentFuel = playerMoving.maxFuel;
        playerMoving.SetFuelValues();
        refuelPanel.SetActive(false);
        mainScript.peace = false;
        Dictionary<string, string> LevelAchievedEvent = new Dictionary<string, string>();
        LevelAchievedEvent.Add("af_level", GetCurrentLevelNumb().ToString());
        AppsFlyer.sendEvent("CampRefuelByMoney", LevelAchievedEvent);
    }
    public void RefuelByAds()
    {
        Dictionary<string, string> LevelAchievedEvent = new Dictionary<string, string>();
        LevelAchievedEvent.Add("af_level", GetCurrentLevelNumb().ToString());
        AppsFlyer.sendEvent("CampRefuelByAds", LevelAchievedEvent);
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
        priceOfRearm = mainScript.milkCollection / 2;
        if (priceOfRearm < 10)
        {
            priceOfRearm = 10;
        }
        rearmText.text = priceOfRearm.ToString();
        if (priceOfRearm > mainScript.allMilk)
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
        Dictionary<string, string> LevelAchievedEvent = new Dictionary<string, string>();
        LevelAchievedEvent.Add("af_level", GetCurrentLevelNumb().ToString());
        AppsFlyer.sendEvent("CampRearmByMoney", LevelAchievedEvent);
        mainScript.SetMilk(-priceOfRearm);
        fss.currentHP = fss.maxHP;
        fss.SetHPValue();
        rearmPanel.SetActive(false);
        mainScript.peace = false;
    }
    public void RearmByAds()
    {
        Dictionary<string, string> LevelAchievedEvent = new Dictionary<string, string>();
        LevelAchievedEvent.Add("af_level", GetCurrentLevelNumb().ToString());
        AppsFlyer.sendEvent("CampRearmByAds", LevelAchievedEvent);
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
        mainScript.ConsumeEnergy();
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
