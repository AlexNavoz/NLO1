using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceShieldScript : MonoBehaviour
{

    Animator anim;
    MainScript mainScript;
    string lname;


    //ForceShieldBar variables
    public float maxHP;
    public float currentHP;
    public ForceShieldBarScript fsb;
    Animator mainCameraAnim;

    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();

        mainCameraAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        SetHPValue();

        anim = GetComponent<Animator>();
        lname = SceneManager.GetActiveScene().name;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.layer == 9)
        {
            
            TakingDamage(3);
        }
        if (currentHP <= 0)
        {
            mainScript.shieldIsActive = false;
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
            currentHP -= dmg;
            fsb.SetValue(currentHP);
        }
    }
    public void SetHPValue()
    {
        maxHP = mainScript.P_forceShieldStrength;
        fsb.SetMaxHP(maxHP);
        currentHP = mainScript.P_forceShieldLevel;
        fsb.SetValue(currentHP);
    }
}
