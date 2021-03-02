using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIBEmissionScript : MonoBehaviour
{
    public GameObject emission2;
    ForceShieldScript fs;
    public float damage;


    private void Start()
    {
        GameObject fs_obj;
        if (fs_obj = GameObject.FindGameObjectWithTag("ForceShield"))
            fs = fs_obj.GetComponent<ForceShieldScript>();
        Invoke("MakeEmission",3.9f);
    }

    public void MakeEmission()
    {
        Instantiate(emission2, transform.position, Quaternion.identity);
    }
}
