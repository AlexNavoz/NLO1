using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketScript : MonoBehaviour
{
    //public  float speed;
    public float flyTime = 0.5f;
    public Renderer rend;
    public float shootPower;
    public float damage;
    public float massScale = 1;
    Rigidbody2D rb;
    ForceShieldScript fs;
    Transform scale;

    public ParticleSystem smokeParticle;
    public GameObject emission;
    public float impulse_angle;
    Vector2 impulse_vector;

    void Start()
    {
        scale = GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();
        rb.mass *= massScale;
        scale.localScale *= massScale;
        

        impulse_vector.x = (float)System.Math.Sin(impulse_angle * System.Math.PI / 2.0f);
        impulse_vector.y = (float)System.Math.Cos(impulse_angle * System.Math.PI / 2.0f);
        //rb.AddRelativeForce(impulse_vector * shootPower * massScale, ForceMode2D.Impulse);
        GameObject fs_obj;
        if (fs_obj = GameObject.FindGameObjectWithTag("ForceShield"))
            fs = fs_obj.GetComponent<ForceShieldScript>();
        Destroy(gameObject, 7.0f);
    }
    private void Update()
    {
        flyTime -= Time.deltaTime;
        if (flyTime > 0)
        {
            rb.AddRelativeForce(impulse_vector * shootPower * massScale * MainScript.forceBatchingMultiplier, ForceMode2D.Force);
            smokeParticle.Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            if (fs)
                fs.TakingDamage(damage * massScale);
        }
        Destroy(gameObject, 2.0f);
    }
}
