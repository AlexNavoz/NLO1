using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    int i = 0;
    Object cowMoving;
    private void Start()
    {
        cowMoving = GetComponentInParent<CowMoving>();
        if (cowMoving == null)
        {
            cowMoving = GetComponentInParent<AttackerScript>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            i++;
            if (i !=0)
            {
                if (cowMoving.GetType() == typeof(CowMoving))
                {
                    ((CowMoving)cowMoving).isGrounded = true;
                }
                else if (cowMoving.GetType() == typeof(AttackerScript))
                {
                    ((AttackerScript)cowMoving).isGrounded = true;
                }
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
                if (cowMoving.GetType() == typeof(CowMoving))
                {
                    ((CowMoving)cowMoving).isGrounded = false;
                }
                else if (cowMoving.GetType() == typeof(AttackerScript))
                {
                    ((AttackerScript)cowMoving).isGrounded = false;
                }
            }
        }
    }

}
