using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPostScript : MonoBehaviour
{
    OnRay onRay;
    public GameObject lampLight;
    Animator anim;
   // FixedJoint2D jointGround;


    private void Start()
    {
        anim = GetComponent<Animator>();
        onRay = GetComponent<OnRay>();
        //jointGround = GetComponent<FixedJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15)
        {
            anim.SetBool("LampFear", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15)
        {
            anim.SetBool("LampFear", false);
        }
    }
    private void OnJointBreak2D(Joint2D joint)
    {
        lampLight.SetActive(false);
    }
}
