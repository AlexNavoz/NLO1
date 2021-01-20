using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    Rigidbody2D rb;
    ForceShieldScript fs;
    void Start()
    {
        fs = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.down * Random.Range(50.0f, 200.0f), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-100, 100),ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            fs.TakingDamage(rb.mass / 2);
        }
    }

}
