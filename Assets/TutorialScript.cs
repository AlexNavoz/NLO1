using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    GameObject player;
    playerMoving playerMoving;

    int tutorialStage = 0;
    // Start is called before the first frame update
    public GameObject[] tutorialObjects;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoving = player.GetComponent<playerMoving>();
        Invoke("TutorialUpdate", 2.0f);
    }

    void TutorialUpdate() {
        tutorialObjects[0].SetActive(true);
        tutorialStage = 1;
        Invoke("TutorialTimestop", 0.5f);
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
                Invoke("TutorialTimestop", 0.5f);
                break;
            case 2:
                tutorialObjects[1].SetActive(false);
                tutorialObjects[4].SetActive(false);
                Time.timeScale = 1;
                tutorialStage++;
                break;
            case 4:
                tutorialObjects[2].SetActive(false);
                tutorialObjects[5].SetActive(false);
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
    }
}
