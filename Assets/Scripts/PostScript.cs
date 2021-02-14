using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostScript : MonoBehaviour
{
    MainScript mainScript;
    playerMoving player;
    ForceShieldScript fs;
    public GameObject canvas;
    int i = 0;

    Animator crossfade;

    private void Awake()
    {
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
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

    public void ChooseAsteroidStage(int stage)
    {
        Time.timeScale = 1;
        StartCoroutine(CrossFade(stage));

    }

    IEnumerator CrossFade(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        i = 0;
        SceneManager.LoadScene(levelIndex);
    }
}
