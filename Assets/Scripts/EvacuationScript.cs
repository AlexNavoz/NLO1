using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvacuationScript : MonoBehaviour
{
    float massOnStart;
    float dragOnStart;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if(collision.gameObject.layer == 10)
        {
            if (rb == null)
            {
                return;
            }
            massOnStart = rb.mass;
            dragOnStart = rb.drag;
            rb.drag = 0;
            rb.mass = 10;
        }

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

        if (collision.gameObject.layer == 10)
        {
            if (rb == null)
            {
                return;
            }
            rb.mass = massOnStart;
            rb.drag = dragOnStart;
        }
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
