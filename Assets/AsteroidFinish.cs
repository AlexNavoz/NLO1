using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsteroidFinish : MonoBehaviour
{
    GameObject box;
    BoxScript boxScript;
    int boxStartHP;
    int boxFinishHP;
    int boxCount;
    int finishBoxCount;
    int percent;

    public Text percentText;
    public Text collectionText;
    public Text boxCountText;
    Animator crossfade;
    GameObject player;
    playerMoving playerMoving;
    MainScript mainScript;
    public GameObject canvas;

    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        box = GameObject.FindGameObjectWithTag("box");
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoving = player.GetComponent<playerMoving>();
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        boxScript = box.GetComponent<BoxScript>();
        boxStartHP = boxScript.boxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            Invoke("CanvasActivation", 2.0f);
            collectionText.text = "+" + mainScript.collection.ToString();
        }
        if(collision.gameObject.tag == "box")
        {
            boxFinishHP = boxScript.boxHP;
            percent = boxFinishHP * 100 / boxStartHP;
            percentText.text = percent.ToString()+"%";
            finishBoxCount = (percent * boxScript.boxCount) / 100;
            boxCountText.text = finishBoxCount.ToString();
        }
    }
    void CanvasActivation()
    {
        canvas.SetActive(true);
        //Time.timeScale = 1;
    }

    public void GoHome()
    {
        mainScript.collection += boxCount;
        mainScript.SetMoney(mainScript.collection);
        mainScript.collection = 0;
        Time.timeScale = 1.0f;
        mainScript.SafeShortPlatePrefs();
        mainScript.SafeShortWSPrefs();
        StartCoroutine(CrossFade(1));
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        mainScript.collection = 0;
        StartCoroutine(CrossFade(SceneManager.GetActiveScene().buildIndex));
    }
    IEnumerator CrossFade(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelIndex);
    }
}
