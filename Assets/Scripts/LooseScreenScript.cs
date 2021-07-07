using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppsFlyerSDK;

public class LooseScreenScript : MonoBehaviour, AdsListener
{
    //Refuel cariables

    public GameObject refuelCanvas;
    public Button refuelByMoneyButton;
    public Text refuelText;
    Animation refuelAnim;
    int refuelPrice;

    //LooseScreen variables

    public GameObject evacuator;
    public GameObject canvas;
    public GameObject mainPanel;
    public GameObject choisePanel;
    Animation choiseAnim;
    public Text milkCollectionText;
    public Text brainsCollectionText;
    public Text priceText;
    Animation mainAnim;

    MainScript mainScript;
    Animator crossfade;
    GameObject player;
    playerMoving playerMoving;
    Vector3 offset = new Vector3(0, -20, 0);

    int adsdestination = 1;
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
        mainScript.peace = false;
    }

    public void CanvasOpen()
    {
        mainScript.peace = true;
        if (canvas.activeSelf)
            return;
        if (mainScript.levelIndex == 1)
        {
            choisePanel.SetActive(false);
            milkCollectionText.text = mainScript.milkCollection.ToString();
            brainsCollectionText.text = mainScript.brainsCollection.ToString();
            priceText.text =((int)(mainScript.milkCollection / 2)).ToString();
        }
        canvas.SetActive(true);
        mainAnim.Play("PanelStartAnim");

    }

    public void LoadMainMenu()
    {
        mainScript.peace = true;
        mainScript.milkCollection = 0;
        mainScript.brainsCollection = 0;
        StartCoroutine(CrossFade(1));
    }

    public void Restart()
    {
        if (mainScript.GetCurrentEnergy() < 1)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            mainScript.ConsumeEnergy();
            mainScript.peace = true;
            mainScript.milkCollection = 0;
            mainScript.brainsCollection = 0;
            StartCoroutine(CrossFade(SceneManager.GetActiveScene().buildIndex));
        }
    }

    public void Evacuate()
    {
        mainScript.peace = false;
        canvas.SetActive(false);
        Instantiate(evacuator, player.transform.position + offset, Quaternion.identity);
    }
    public void ExitMainCanvas()
    {
        mainScript.peace = false;
        canvas.SetActive(false);
    }

    public void LetThemPay()
    {
        Time.timeScale = 1;
        choisePanel.SetActive(true);
        choiseAnim.Play("PanelStartAnim");
        mainScript.checkIfAdsReady();
    }

    public void ExitPayPanel()
    {
        Time.timeScale = 1;
        choisePanel.SetActive(false);
        mainScript.peace = false;
    }

    public void BuyEvac()
    {
        AppsFlyer.sendEvent("GameBuyEvacByMoney", null);
        mainScript.milkCollection /=2;
        mainScript.peace = false;
        Evacuate();
    }

    public void BuyEvacByAdd()                                                //change after add
    {
        AppsFlyer.sendEvent("GameBuyEvacByAdd", null);
        if (mainScript.ShowRewardedVideo(this))
        {
            adsdestination = 2;
            Debug.Log("Showing ad");
        }
        else
        {
            Debug.Log("Showing ad failed");
        }
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
        if (refuelCanvas.activeSelf)
            return;
        refuelPrice = (int)(mainScript.milkCollection/2);
        if (refuelPrice < 10)
        {
            refuelPrice = 10;
        }
        refuelText.text = refuelPrice.ToString();
        if (refuelPrice > mainScript.allMilk)
        {
            refuelText.color = new Color(255,0,0);
            refuelByMoneyButton.interactable = false;
        }
        refuelCanvas.SetActive(true);
        refuelAnim.Play("PanelStartAnim");
        mainScript.checkIfAdsReady();
        mainScript.peace = true;
    }
    public void RefuelByMoney()
    {
        AppsFlyer.sendEvent("GameRefuelByMoney", null);
        //mainScript.allMoney -= (int)refuelPrice;
        mainScript.SetMilk(-(int)refuelPrice);
        player.GetComponent<playerMoving>().currentFuel = player.GetComponent<playerMoving>().maxFuel;
        player.GetComponent<playerMoving>().SetFuelValues();
        playerMoving.alreadyRefueled = true;
        mainScript.peace = false;
        ExitRefuelCanvas();
    }
                                                                                                                            // After Ads
    public void RefuelByAds()
    {
        AppsFlyer.sendEvent("GameRefuelByAds", null);
        if (mainScript.ShowRewardedVideo(this))
        {
            adsdestination = 1;
            Debug.Log("Showing ad");
        }
        else
        {
            Debug.Log("Showing ad failed");
        }
    }


    public void AdsShowed()
    {
        if (adsdestination == 1)
        {
            Debug.Log("AdsSkipped");
            player.GetComponent<playerMoving>().currentFuel = player.GetComponent<playerMoving>().maxFuel;
            player.GetComponent<playerMoving>().SetFuelValues();
            playerMoving.alreadyRefueled = true;
            mainScript.peace = false;
            ExitRefuelCanvas();
        }
        else if (adsdestination == 2)
        {
            mainScript.peace = false;
            Evacuate();
        }
    }

    public void AdsFailed()
    {
        Debug.Log("AdsFailed");
    }
    public void AdsSkipped()
    {
        Debug.Log("AdsSkipped");
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
