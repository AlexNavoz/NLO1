using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public bool rocketOrBullet = false;
    public ParticleSystem smokeParticle;
    public GameObject emission;
    public float flyTime;

    public float shootPower;
    public float damage;
    public float massScale = 1;
    Rigidbody2D rb;
    ForceShieldScript fs;
    Transform scale;
    Vector2 impulse_vector = new Vector2(0.0f,1.0f);

    public float impulse_angle;

    void Start()
    {
        scale = GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();
        rb.mass *= massScale;
        scale.localScale *= massScale;

        //impulse_vector.x = (float)System.Math.Sin(impulse_angle * System.Math.PI / 2.0f);
        //impulse_vector.y = (float)System.Math.Cos(impulse_angle * System.Math.PI / 2.0f);

        if (!rocketOrBullet)
        {
            rb.AddRelativeForce(impulse_vector * shootPower * massScale, ForceMode2D.Impulse);
        }
        GameObject fs_obj;
        if(fs_obj = GameObject.FindGameObjectWithTag("ForceShield"))
            fs = fs_obj.GetComponent<ForceShieldScript>();
        Destroy(gameObject, 7.0f);
    }

    private void Update()
    {
        if (rocketOrBullet)
        {
            flyTime -= Time.deltaTime;
            if (flyTime > 0)
            {
                rb.AddRelativeForce(impulse_vector * shootPower * massScale, ForceMode2D.Force);
            }
            else { smokeParticle.Stop(); }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!rocketOrBullet)
        {
            if (collision.gameObject.layer == 13)
            {
                if (fs)
                    fs.TakingDamage(damage * massScale);
            }
            Destroy(gameObject, 2.0f);
        }
        if (rocketOrBullet)
        {
            if (collision.gameObject.layer == 13)
            {
                if (fs)
                {
                    fs.TakingDamage(damage * massScale);
                    Instantiate(emission, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            else
            {
                Instantiate(emission, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
