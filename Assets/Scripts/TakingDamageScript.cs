using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamageScript : MonoBehaviour
{
    ForceShieldScript fs;
    public float damage;
    int i = 0;
    void Start()
    {
        GameObject fs_obj;
        if (fs_obj = GameObject.FindGameObjectWithTag("ForceShield"))
            fs = fs_obj.GetComponent<ForceShieldScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 13 && i == 0)
        {
            i++;
            fs.TakingDamage(damage);
        }
    }
}
