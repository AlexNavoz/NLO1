using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvacuationScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            return;
        }
        else
        {
            rb.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            return;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}
