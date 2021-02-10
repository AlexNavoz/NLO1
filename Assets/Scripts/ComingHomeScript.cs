using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComingHomeScript : MonoBehaviour
{
    MainScript mainScript;
    Animator crossfade;
    void Start()
    {
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            mainScript.SafeShortPlatePrefs();
            mainScript.SafeShortWSPrefs();
            ChooseEarthStage(1);
        }
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
