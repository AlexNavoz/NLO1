using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int bulletIndex = 0;
    public ParticleSystem smokeParticle;
    public GameObject emission;
    public float flyTime;
    FixedJoint2D joint;
    public float shootPower;
    public float damage;
    public float massScale = 1;
    Rigidbody2D rb;
    ForceShieldScript fs;
    Transform scale;
    Vector2 impulse_vector = new Vector2(0.0f,1.0f);

    public float impulse_angle;
    int i = 0;

    void Start()
    {
        scale = GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();
        rb.mass *= massScale;
        scale.localScale *= massScale;

        //impulse_vector.x = (float)System.Math.Sin(impulse_angle * System.Math.PI / 2.0f);
        //impulse_vector.y = (float)System.Math.Cos(impulse_angle * System.Math.PI / 2.0f);

        if (bulletIndex == 0)
        {
            rb.AddRelativeForce(impulse_vector * shootPower * massScale * MainScript.forceBatchingMultiplier, ForceMode2D.Impulse);
            Destroy(gameObject, 5.0f);
        }
        if (bulletIndex == 1)
        {
            Destroy(gameObject, 5.0f);
        }
        if (bulletIndex == 2)
        {
            rb.AddRelativeForce(impulse_vector * shootPower * massScale * MainScript.forceBatchingMultiplier, ForceMode2D.Impulse);
            joint = GetComponent<FixedJoint2D>();
        }
        
        GameObject fs_obj;
        if(fs_obj = GameObject.FindGameObjectWithTag("ForceShield"))
            fs = fs_obj.GetComponent<ForceShieldScript>();
        
    }

    private void Update()
    {
        if (bulletIndex == 1)
        {
            flyTime -= Time.deltaTime;
            if (flyTime > 0)
            {
                rb.AddRelativeForce(impulse_vector * shootPower * massScale * Time.deltaTime * MainScript.forceBatchingMultiplier, ForceMode2D.Force);
            }
            else { smokeParticle.Stop(); }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bulletIndex == 0)
        {
            if (collision.gameObject.layer == 13)
            {
                if (fs)
                    fs.TakingDamage(damage * massScale);
            }
            Destroy(gameObject, 2.0f);
        }
        if (bulletIndex == 1)
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
        if (bulletIndex == 2)
        {
            if (collision.gameObject.layer == 13)
            {
                if (fs&&i == 0)
                {
                    
                    fs.TakingDamage(damage * massScale);
                    FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                    joint.breakForce = 100;
                    joint.connectedBody = collision.rigidbody;
                    i++;
                }
            }
        }
    }
}
