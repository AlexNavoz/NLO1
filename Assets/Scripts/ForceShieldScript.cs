using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class ForceShieldScript : MonoBehaviour
{

    Animator anim;
    MainScript mainScript;
    playerMoving playermoving;
    string lname;
    int i = 0;


    //ForceShieldBar variables
    public float maxHP;
    public float currentHP;
    public ForceShieldBarScript fsb;
    Animator mainCameraAnim;

    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        playermoving = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMoving>();
        mainCameraAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        SetHPValue();

        anim = GetComponent<Animator>();
        lname = SceneManager.GetActiveScene().name;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.layer == 9)
        {
            i++;
            if ( playermoving.currentFuel <= 0 && !playermoving.alreadyRefueled && currentHP>3 && i ==1)
            {
                Invoke("OpenRefuelPanel", 1.0f);
            }
            TakingDamage(3);
        }
        if (currentHP <= 0)
        {
            mainScript.shieldIsActive = false;
            playermoving.canDie = true;
            gameObject.SetActive(false);
        }
    }

    public void TakingDamage(float dmg)
    {
        anim.SetTrigger("TakeDMGAnim");
        if (dmg >= 5)
        {
            mainCameraAnim.SetTrigger("Shake");
        }
        if (lname !="Main menu")
        {
            StringBuilder sb = new StringBuilder("-",10);
            sb.Append(((int)dmg).ToString());
            playermoving.showTextValue(gameObject, sb.ToString(), 0);
            currentHP -= dmg;
            fsb.SetValue(currentHP);
        }
    }
    public void SetHPValue()
    {
        if (mainScript.ShipIndex == 0)
        {
            maxHP = mainScript.P_forceShieldStrength;
            currentHP = mainScript.P_forceShieldLevel;
            if (currentHP < maxHP)
            {
                mainScript.P_forceShieldStrength = maxHP;
                mainScript.P_forceShieldLevel = currentHP;
                fsb.SetMaxHP(maxHP);

                fsb.SetValue(currentHP);

            }
            else
            {
                fsb.SetMaxHP(maxHP);
                currentHP = maxHP;
                fsb.SetValue(currentHP);

                mainScript.P_forceShieldStrength = maxHP;
                mainScript.P_forceShieldLevel = currentHP;
            }
        }
        if(mainScript.ShipIndex == 1)
        {
            maxHP = mainScript.WS_forceShieldStrength;
            currentHP = mainScript.WS_forceShieldLevel;
            if (currentHP < maxHP)
            {
                mainScript.WS_forceShieldStrength = maxHP;
                mainScript.WS_forceShieldLevel = currentHP;
                fsb.SetMaxHP(maxHP);

                fsb.SetValue(currentHP);

            }
            else
            {
                fsb.SetMaxHP(maxHP);
                currentHP = maxHP;
                fsb.SetValue(currentHP);

                mainScript.WS_forceShieldStrength = maxHP;
                mainScript.WS_forceShieldLevel = currentHP;
            }
        }
    }

    public void OpenRefuelPanel()
    {
        LooseScreenScript refuelCanvas = GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<LooseScreenScript>();
        refuelCanvas.RefuelCanvasOpen();
    }
}
