using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clawController : MonoBehaviour
{
    public Rigidbody2D leftClaw;
    public Rigidbody2D rightClaw;
    public Image buttonSprite;
    public Sprite openedSprite;
    public Sprite closedSprite;
    bool isClosen = false;
    CampQuestScript campQuest;
    MainScript mainScript;
    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        campQuest = GameObject.FindGameObjectWithTag("CampQuest").GetComponent<CampQuestScript>();
        buttonSprite.sprite = closedSprite;
    }

    private void FixedUpdate()
    {
        if (isClosen)
        {
            leftClaw.AddTorque(-40);
            rightClaw.AddTorque(40);
        }
        else
        {
            leftClaw.AddTorque(40);
            rightClaw.AddTorque(-40);
        }
    }

    public void ClawButton()
    {
        if (isClosen)
        {
            isClosen = false;
            buttonSprite.sprite = closedSprite;
        }
        else
        {
            isClosen = true;
            buttonSprite.sprite =  openedSprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            campQuest.objectIsInClaw = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            campQuest.objectIsInClaw = false;
        }
    }
}
