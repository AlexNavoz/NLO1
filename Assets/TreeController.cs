using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    OnRay onRay;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        onRay = GetComponent<OnRay>();
    }
    void Update()
    {
        if (onRay.isInRay)
        {
            anim.SetBool("OnRay",true);
        }
        else { anim.SetBool("OnRay",false); }
    }
}
