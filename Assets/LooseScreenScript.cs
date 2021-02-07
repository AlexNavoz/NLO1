using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LooseScreenScript : MonoBehaviour
{
    //Refuel cariables

    public GameObject refuelCanvas;
    public Button refuelByMoneyButton;
    public Text refuelText;
    Animation refuelAnim;
    float refuelPrice;

    //LooseScreen variables

    public GameObject evacuator;
    public GameObject canvas;
    public GameObject mainPanel;
    public GameObject choisePanel;
    Animation choiseAnim;
    public Text collectionText;
    public Text priceText;
    Animation mainAnim;

    MainScript mainScript;
    Animator crossfade;
    GameObject player;
    playerMoving playerMoving;
    Vector3 offset = new Vector3(0, -20, 0);
    void Start()
    {

        mainAnim = mainPanel.GetComponent<Animation>();
        refuelAnim = refuelCanvas.GetComponent<Animation>();
        crossfade = GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoving = player.GetComponent<playerMoving>();

        if (choisePanel != null)
        {
            choiseAnim = choisePanel.GetComponent<Animation>();
        }
    }

    public void CanvasOpen()
    {
        if (mainScript.levelIndex == 1)
        {
            choisePanel.SetActive(false);
            collectionText.text = mainScript.collection.ToString();
            priceText.text = (mainScript.collection / 2).ToString();
        }
        canvas.SetActive(true);
        mainAnim.Play("PanelStartAnim");
        Invoke("TimeStop", mainAnim.clip.length);

    }
    public void TimeStop()
    {
        Time.timeScale = 0;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        mainScript.SafeShortPlatePrefs();
        StartCoroutine(CrossFade(1));
    }

    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        StartCoroutine(CrossFade(SceneManager.GetActiveScene().buildIndex));
    }

    public void Evacuate()
    {
        Time.timeScale = 1.0f;
        canvas.SetActive(false);
        Instantiate(evacuator, player.transform.position + offset, Quaternion.identity);
    }

    public void LetThemPay()
    {
        Time.timeScale = 1;
        choisePanel.SetActive(true);
        choiseAnim.Play("PanelStartAnim");
        Invoke("TimeStop", choiseAnim.clip.length);
    }

    public void ExitPayPanel()
    {
        Time.timeScale = 1;
        choisePanel.SetActive(false);
    }

    public void BuyEvac()
    {
        mainScript.collection = mainScript.collection / 2;

        Evacuate();
    }

    public void BuyEvacByAdd()                                                //change after add
    {
        Evacuate();
    }

    public void Activation()
    {
        CanvasOpen();
    }

    IEnumerator CrossFade(int levelIndex)
    {
        crossfade.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(levelIndex);
    }

    // Refuel methods

    public void RefuelCanvasOpen()
    {
        refuelPrice = player.GetComponent<playerMoving>().maxFuel * 3;
        refuelText.text = refuelPrice.ToString();
        if (refuelPrice > mainScript.allMoney)
        {
            refuelText.color = new Color(255,0,0);
            refuelByMoneyButton.interactable = false;
        }
        refuelCanvas.SetActive(true);
        refuelAnim.Play("PanelStartAnim");
        Invoke("TimeStop", refuelAnim.clip.length);
    }
    public void RefuelByMoney()
    {
        //mainScript.allMoney -= (int)refuelPrice;
        mainScript.SetMoney(-(int)refuelPrice);
        player.GetComponent<playerMoving>().currentFuel = player.GetComponent<playerMoving>().maxFuel;
        mainScript.SafeShortPlatePrefs();
        player.GetComponent<playerMoving>().SetFuelValues();
        playerMoving.alreadyRefueled = true;
        ExitRefuelCanvas();
    }
                                                                                                                            // After Ads
    public void RefuelByAds()
    {
        player.GetComponent<playerMoving>().currentFuel = player.GetComponent<playerMoving>().maxFuel;
        mainScript.SafeShortPlatePrefs();
        player.GetComponent<playerMoving>().SetFuelValues();
        playerMoving.alreadyRefueled = true;
        ExitRefuelCanvas();
    }

    public void ExitRefuelCanvas()
    {
        Time.timeScale = 1;
        playerMoving.alreadyRefueled = true;
        //refuelAnim.Play("PanelExitAnim");
        //Invoke("ExitFromRefuelCanvas", refuelAnim.clip.length);
        refuelCanvas.SetActive(false);
    }

}
