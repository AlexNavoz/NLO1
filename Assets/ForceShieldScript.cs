using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceShieldScript : MonoBehaviour
{

    Animator anim;

    //ForceShieldBar variables
    public float maxHP;
    float currentHP;
    public ForceShieldBarScript fsb;

    private void Start()
    {
        maxHP = PlayerPrefs.GetFloat("MaxHPPlate", 100.0f);
        currentHP = maxHP;
        fsb.SetMaxHP(maxHP);

        anim = GetComponent<Animator>();
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
        if (!SceneManager.sceneCount.Equals(1))
        {
            currentHP -= dmg;
            fsb.SetValue(currentHP);
        }
    }
}
