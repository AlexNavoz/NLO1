using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    GameObject player;
    playerMoving playerMoving;
    Button leftButton;
    Button rightButton;

    int tutorialStage = 0;
    const int tutorialStagesCount = 10;
    // Start is called before the first frame update
    public GameObject[] tutorialObjects;
    void Start()
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
    /*

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
    */
    void Update()
    {
        /*
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
}
