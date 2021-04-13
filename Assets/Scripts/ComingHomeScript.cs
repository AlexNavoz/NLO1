﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComingHomeScript : MonoBehaviour, AdsListener
{
    MainScript mainScript;
    Animator crossfade;
    Rigidbody2D playerRB;
    CampQuestScript campQuest;
    public Button x2Button;
    public GameObject canvas;
    public Text collectionText;
    public AudioSource clickSound;
    void Start()
    {
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        canvas.SetActive(false);
        if (mainScript.levelIndex != 1)
        {
            campQuest = GameObject.FindGameObjectWithTag("CampQuest").GetComponent<CampQuestScript>();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            switch (mainScript.levelIndex) {
                case 1:
                    canvas.SetActive(true);
                    playerRB.gravityScale = 0;
                    playerRB.drag = 10;
                    collectionText.text = mainScript.collection.ToString();
                    if (mainScript.collection > 1500)
                    {
                        x2Button.interactable = false;
                    }
                    break;
                case 3:
                    break;
                case 4:
                    break;
        }
        }
    }

    public void X2Button()
    {
        clickSound.Play();
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
        mainScript.collection *= 2;
        mainScript.SetMoney(mainScript.collection);
        mainScript.collection = 0;
        StartCoroutine(CrossFade(1));
    }
    public void AdsFailed()
    {
        Debug.Log("AdsFailed");
    }
    public void AdsSkipped()
    {
        Debug.Log("AdsSkipped");
    }

    public void OKButton()
    {
        clickSound.Play();
        Time.timeScale = 1;
        mainScript.SetMoney(mainScript.collection);
        mainScript.collection = 0;
        StartCoroutine(CrossFade(1));
    }
    public void Restart()
    {
        clickSound.Play();
        Time.timeScale = 1.0f;
        mainScript.collection = 0;
        StartCoroutine(CrossFade(SceneManager.GetActiveScene().buildIndex));
    }
    public void ChooseEarthStage(int stage)
    {
        StartCoroutine(CrossFade(stage));

    }

    IEnumerator CrossFade(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelIndex);
    }
}
