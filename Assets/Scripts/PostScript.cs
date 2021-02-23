using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostScript : MonoBehaviour
{
    MainScript mainScript;
    playerMoving player;
    ForceShieldScript fs;
    public GameObject canvas;
    int i = 0;
    public GameObject[] a_Stages;
    public Button[] a_Buttons;

    Animator crossfade;

    private void Awake()
    {
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }
    private void Start()
    {
        mainScript.PostLevels();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && i == 0)
        {
            i++;
            Invoke("CanvasSetActive", 0.8f);
            mainScript.SafeShortPlatePrefs();
            mainScript.SafeShortWSPrefs();

        }


    }
    void CanvasSetActive()
    {
        canvas.SetActive(true);
        Time.timeScale = 0.2f;
    }
    public void ExitCanvas()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);

    }

    public void ChooseAsteroidStage(string stageName)
    {
        Time.timeScale = 1;
        StartCoroutine(CrossFade(stageName));

    }

    void StarsSwitch()
    {
        for(int i = 0; i<a_Stages.Length-1; i++)
        {
            GameObject star1 = a_Stages[i].transform.Find("Star1").gameObject;
            GameObject star2 = a_Stages[i].transform.Find("Star2").gameObject;
            GameObject star3 = a_Stages[i].transform.Find("Star3").gameObject;
            if (mainScript.postLevels[i] == 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
            }
            if (mainScript.postLevels[i] == 2)
            {
                star1.SetActive(true);
                star2.SetActive(true);
            }
            if (mainScript.postLevels[i] == 1)
            {
                star1.SetActive(true);
            }
            if (mainScript.postLevels[i] == 0)
            {
                a_Buttons[i + 1].interactable = false;
            }
        }
    }

    IEnumerator CrossFade(string levelName)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        i = 0;
        SceneManager.LoadScene(levelName);
    }
}
