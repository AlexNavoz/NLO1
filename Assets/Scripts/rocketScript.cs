using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketScript : MonoBehaviour
{
    Rigidbody2D rb;
    public  float speed;
    public Renderer rend;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    
    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.right * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != 10)
        {
            rend.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            Destroy(gameObject,3.0f);
        }
    }
    
}
