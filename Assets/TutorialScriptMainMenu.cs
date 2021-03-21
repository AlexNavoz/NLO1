using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScriptMainMenu : MonoBehaviour
{
    GameObject player;
    Rigidbody2D playerRb;
    playerMoving playerMoving;
    GameObject leftButton;
    GameObject rightButton;
    public GameObject nextButton;
    //public GameObject backButton;
    public GameObject cows;

    Destroyer destroyer;
    int tutorialStage = 0;
    public GameObject[] tutorialObjects;

    public GameObject collector;
    public GameObject garage;
    public GameObject post;
    public GameObject refuel;

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

        playerMoving.isDead = true;
        playerRb.gravityScale = 0;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        nextButton.SetActive(true);
        //backButton.SetActive(true);
    }
    private void Update()
    {
        if (playerMoving.currentFuel < 10.0f)
        {
            playerMoving.currentFuel = 30.0f;
            playerMoving.SetFuelValues();
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
                player.transform.position = collector.transform.position;
                break;
            case 2:
                player.transform.position = garage.transform.position;
                HintController(2);
                break;
            case 3:
                HintController(3);
                player.transform.position = refuel.transform.position;
                break;
            case 4:
                HintController(4);
                player.transform.position = post.transform.position;
                break;
            case 5:
                HintController(5);
                break;
            case 6:
                HintController(6);
                break;
            case 7:
                PlayerPrefs.SetInt("TutorialCompleted", 1);
                SceneManager.LoadScene("Main menu");
                break;
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
        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            if (i == hintIndex)
            {
                tutorialObjects[i].SetActive(true);
            }
            else
            {
                tutorialObjects[i].SetActive(false);
            }
        }
    }
}
