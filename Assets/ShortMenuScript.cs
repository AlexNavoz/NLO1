using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortMenuScript : MonoBehaviour
{
    public GameObject shortMenuObj;
    public GameObject garageCanvas;
    public GameObject postCanvas;
    public GameObject refuelCanvas;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartShortMenu()
    {
        shortMenuObj.SetActive(true);
    }
    public void GarageButton()
    {
        player.transform.position = garageCanvas.transform.position;
        shortMenuObj.SetActive(false);
    }
    public void PostButton()
    {
        player.transform.position = postCanvas.transform.position;
        shortMenuObj.SetActive(false);
    }
    public void RefuelButton()
    {
        player.transform.position = refuelCanvas.transform.position;
        shortMenuObj.SetActive(false);
    }
    public void ExitFromGameButton()
    {
        Application.Quit();
    }
    public void BackButton()
    {
        shortMenuObj.SetActive(false);
    }
}
