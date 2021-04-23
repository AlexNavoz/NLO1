using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvacuationScript : MonoBehaviour
{
    float massOnStart;
    float dragOnStart;
    MainScript mainScript;

    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.layer == 8)
        {
            collision.transform.position =new Vector3(collision.transform.position.x, collision.transform.position.y+ 10 *Time.deltaTime, collision.transform.position.z);

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if(collision.gameObject.tag == "Player")
        {
            if (rb == null)
            {
                return;
            }
            massOnStart = rb.mass;
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

        if (collision.gameObject.tag == "Player")
        {
            if (rb == null)
            {
                return;
            }
            rb.mass = massOnStart;
        }
        if (rb == null)
        {
            return;
        }
        else
        {
            if(mainScript.ShipIndex !=2)
            rb.gravityScale = 1;
        }
    }
}
