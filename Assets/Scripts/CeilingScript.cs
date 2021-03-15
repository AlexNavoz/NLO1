using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingScript : MonoBehaviour
{
    float enterMass;
    public float massCoeff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enterMass = collision.gameObject.GetComponent<Rigidbody2D>().mass;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().mass += massCoeff;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().mass = enterMass;
        }
    }
}
