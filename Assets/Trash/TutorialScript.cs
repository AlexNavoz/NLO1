using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{

    GameObject player;
    Rigidbody2D playerRb;
    playerMoving playerMoving;
    GameObject leftButton;
    GameObject rightButton;
    public GameObject nextButton;
   // public GameObject backButton;
    public GameObject cows;

    Destroyer destroyer;
    int a = 0;
    int tutorialStage = 0;
    public GameObject[] tutorialObjects;

    Transform startPosTransform;
    Vector3 startPos;

    private void Start()
    {
        destroyer = GameObject.FindGameObjectWithTag("Destroyer").GetComponent<Destroyer>();
        leftButton = GameObject.FindGameObjectWithTag("LeftSlider");
        rightButton = GameObject.FindGameObjectWithTag("RightSlider");
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoving = player.GetComponent<playerMoving>();
        playerRb = player.GetComponent<Rigidbody2D>();
        HintController(0);
        TutorialStagesController(tutorialStage);
        startPosTransform = GameObject.FindGameObjectWithTag("StartPosition").GetComponent<Transform>();
        startPos = new Vector3(startPosTransform.position.x, startPosTransform.position.y, startPosTransform.position.z);
    }
    private void Update()
    {
        if (playerMoving.currentFuel < 10.0f)
        {
            playerMoving.currentFuel = 30.0f;
            playerMoving.SetFuelValues();
        }
        if (a == 0 && destroyer.stolenCows>1)
        {
            NextTutorialStage();
            a++;
        }
        if (tutorialStage == 10&& player.transform.position.y > 23)
        {
            SceneManager.LoadScene("FirstStartMainMenu");
            return;
        }
        else if (tutorialStage<6)
        {
            if(player.transform.position.x>45|| player.transform.position.x < -40 || player.transform.position.y > 23)
            {
                player.transform.position = startPos;
            }
        }
        else
        {
            if (player.transform.position.y > 23)
            {
                player.transform.position = startPos;
            }
        }
    }

    void TutorialStagesController(int tutorialStageIndex)
    {
        switch (tutorialStageIndex)
        {
            case 0:
                HintController(0);
                break;
            case 1:
                HintController(1);
                break;
            case 2:
                HintController(2);
                break;
            case 3:
                HintController(3);
                break;
            case 4:
                HintController(4);
                break;
            case 5:
                tutorialObjects[4].SetActive(false);
                Invoke("NextTutorialStage", 30);
                break;
            case 6:
                HintController(5);
                break;
            case 7:
                HintController(6);
                break;
            case 8:
                tutorialObjects[6].SetActive(false);
                break;
            case 9:
                HintController(7);
                break;
            case 10:
                tutorialObjects[7].SetActive(false);
                break;

        }
        if(tutorialStageIndex == 5||tutorialStageIndex == 8|| tutorialStageIndex == 10)
        {
            if(tutorialStageIndex == 8)
            {
                cows.SetActive(true);
            }
            playerMoving.isDead = false;
            playerRb.gravityScale = 1;
            leftButton.SetActive(true);
            rightButton.SetActive(true);
            nextButton.SetActive(false);
            //backButton.SetActive(false);
        }
        else
        {
            playerMoving.isDead = true;
            playerRb.gravityScale = 0;
            leftButton.SetActive(false);
            rightButton.SetActive(false);
            nextButton.SetActive(true);
            //backButton.SetActive(true);
        }
    }
    public void NextTutorialStage()
    {
        tutorialStage++;
        TutorialStagesController(tutorialStage);
    }

    public void PreviousTutorialStage()
    {
        tutorialStage--;
        TutorialStagesController(tutorialStage);
    }
    void HintController(int hintIndex)
    {
        for(int i = 0; i < tutorialObjects.Length; i++)
        {
            if(i == hintIndex)
            {
                tutorialObjects[i].SetActive(true);
            }
            else
            {
                tutorialObjects[i].SetActive(false);
            }
        }
    }
    /*void Start()
    {
        leftButton = GameObject.FindGameObjectWithTag("LeftSlider").GetComponent<Button>();
        rightButton = GameObject.FindGameObjectWithTag("RightSlider").GetComponent<Button>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoving = player.GetComponent<playerMoving>();
        tutorialStage = 0;
        Invoke("TutorialUpdate", 1.0f);
    }

    void TutorialUpdate()
    {
        updateStage();
    }

    void updateStage()
    {
        tutorialObjects[0].SetActive(tutorialStage == 0);
        tutorialObjects[1].SetActive(tutorialStage == 1);
        tutorialObjects[2].SetActive(tutorialStage == 2);
        tutorialObjects[3].SetActive(tutorialStage == 3);
        tutorialObjects[4].SetActive(tutorialStage == 4);
        if (tutorialStage == 5)
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        } else
        {
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        tutorialObjects[5].SetActive(tutorialStage == 6);
        tutorialObjects[6].SetActive(tutorialStage == 7);
        tutorialObjects[7].SetActive(tutorialStage == 8);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (collision.gameObject.layer == 10)
        {
            nextClicked();
        }
    }

    void finishTutuorial() { 
        //something finalizing
    }

    public void nextClicked()
    {
        if ((tutorialStage + 1) == tutorialStagesCount) {
            finishTutuorial();
            return;
        }
        tutorialStage++;
        updateStage();
    }
    public void backClicked()
    {
        if (tutorialStage == 0) {
            finishTutuorial();
            return;
        }
        tutorialStage--;
        updateStage();
    }
    

    void TutorialTimestop()
    {
        Time.timeScale = 0;
        doubleClickPrevent = 1.1f;
    }

    public void TutorialOnClick() {
        switch (tutorialStage) {
            case 1:
                tutorialObjects[0].SetActive(false);
                Time.timeScale = 1;
                tutorialStage++;
                tutorialObjects[1].SetActive(true);
                tutorialObjects[4].SetActive(true);
                rightButton.interactable = false;
                Invoke("TutorialTimestop", 0.5f);
                break;
            case 2:
                tutorialObjects[1].SetActive(false);
                tutorialObjects[4].SetActive(false);
                rightButton.interactable = true;
                Time.timeScale = 1;
                tutorialStage++;
                break;
            case 4:
                tutorialObjects[2].SetActive(false);
                tutorialObjects[5].SetActive(false);
                leftButton.interactable = true;
                Time.timeScale = 1;
                tutorialStage++;
                break;
            case 6:
                tutorialObjects[3].SetActive(false);
                Time.timeScale = 1;
                tutorialStage++;
                break;
        }
    }
    // Update is called once per frame
    float doubleClickPrevent = 0;
    void TutorialDoubleClickPrevent() {
        doubleClickPrevent = 0;
    }
    *//*
    void Update()
    {
        
        if (doubleClickPrevent < 1.0f)
        {
            doubleClickPrevent += Time.deltaTime;
            return;
        }
        switch (tutorialStage)
        {
            case 1:
            case 2:
            case 4:
            case 6:
                bool touched = false;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch t = Input.GetTouch(i);
                    if (t.phase == TouchPhase.Began)
                    {
                        touched = true;
                        break;
                    }
                }
                if (Input.GetMouseButtonDown(0) || touched)
                {
                    TutorialOnClick();
                    TutorialDoubleClickPrevent();
                }
                break;
            case 3:
                if (player.transform.rotation.eulerAngles.z > 270.0f && player.transform.rotation.eulerAngles.z < 320.0f)
                {
                    //Time.timeScale = 1;
                    tutorialStage++;
                    tutorialObjects[2].SetActive(true);
                    tutorialObjects[5].SetActive(true);
                    leftButton.interactable = false;
                    Invoke("TutorialTimestop", 0.5f);
                    break;
                }
                break;
            case 5:
                if (player.transform.rotation.eulerAngles.z > 40.0f && player.transform.rotation.eulerAngles.z < 90.0f)
                {
                    //Time.timeScale = 1;
                    tutorialStage++;
                    tutorialObjects[3].SetActive(true);
                    Invoke("TutorialTimestop", 0.5f);
                    break;
                }
                break;
        }
        */
}

