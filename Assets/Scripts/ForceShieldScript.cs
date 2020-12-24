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

    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();

        maxHP = mainScript.P_forceShieldStrength;
        currentHP = maxHP;
        fsb.SetHPValue(currentHP);

        anim = GetComponent<Animator>();
        lname = SceneManager.GetActiveScene().name;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.layer == 9)
        {
            anim.SetTrigger("TakeDMGAnim");
            TakingDamage(10);
        }
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakingDamage(float dmg)
    {
        if (lname !="Main menu")
        {
            currentHP -= dmg;
            fsb.SetValue(currentHP);
        }
    }
}
