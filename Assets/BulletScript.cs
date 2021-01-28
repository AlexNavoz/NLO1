using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float shootPower;
    public float damage;
    Rigidbody2D rb;
    ForceShieldScript fs;
    void Start()
    {
        fs = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector3.left*shootPower,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            fs.TakingDamage(damage);
        }
        Destroy(gameObject, 2.0f);
    }
}
