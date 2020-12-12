using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    int i = 0;
    CowMoving cowMoving;
    private void Start()
    {
        cowMoving = GetComponentInParent<CowMoving>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            i++;
            if (i !=0)
            {
                cowMoving.isGrounded = true;
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            i--;
            if (i == 0)
            {
                cowMoving.isGrounded = false;
            }
        }
    }

}
