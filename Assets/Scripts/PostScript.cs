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
        StarsSwitch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && i == 0)
        {
            i++;
            Invoke("CanvasSetActive", 0.8f);
        }


    }
    void CanvasSetActive()
    {
        canvas.SetActive(true);
        Time.timeScale = 0.2f;
    }
    public void ExitCanvas()
    {
        i = 0;
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
        for(int i = 0; i<a_Stages.Length; i++)
        {
            Transform stars = a_Stages[i].transform.Find("Stars");
            GameObject star1 = stars.Find("Star1").gameObject;
            GameObject star2 = stars.Find("Star2").gameObject;
            GameObject star3 = stars.Find("Star3").gameObject;
            switch (mainScript.postLevels==null ? 0 : mainScript.postLevels[i]) {
                case 3:
                    star3.SetActive(true);
                    goto case 2;
                case 2:
                    star2.SetActive(true);
                    goto case 1;
                case 1:
                    star1.SetActive(true);
                    if ((i + 1) < a_Buttons.Length)
                        a_Buttons[i + 1].interactable = true;
                    break;
                default:
                    if((i + 1) < a_Buttons.Length)
                        a_Buttons[i + 1].interactable = false;
                    break;
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
