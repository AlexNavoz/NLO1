using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float shootPower;
    public float damage;
    public float massScale = 1;
    Rigidbody2D rb;
    ForceShieldScript fs;
    Transform scale;

    public float impulse_angle;

    void Start()
    {
        scale = GetComponent<Transform>();
        fs = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
        rb = GetComponent<Rigidbody2D>();
        rb.mass *= massScale;
        scale.localScale *= massScale;
        Vector2 impulse_vector;

        impulse_vector.x = (float)System.Math.Sin(impulse_angle * System.Math.PI / 2.0f);
        impulse_vector.y = (float)System.Math.Cos(impulse_angle * System.Math.PI / 2.0f);
        rb.AddRelativeForce(impulse_vector * shootPower*massScale, ForceMode2D.Impulse);
        Destroy(gameObject, 7.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            fs.TakingDamage(damage*massScale);
        }
        Destroy(gameObject, 2.0f);
    }
}
