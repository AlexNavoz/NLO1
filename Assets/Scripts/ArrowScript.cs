using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public bool isFlaming = false;
    FixedJoint2D joint;
    public float shootPower;
    public float damage;
    public float massScale = 1;
    Rigidbody2D rb;
    ForceShieldScript fs;
    Transform scale;
    Vector2 impulse_vector = new Vector2(0.0f, 1.0f);

    public float impulse_angle;

    void Start()
    {
        scale = GetComponent<Transform>();
        joint = GetComponent<FixedJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.mass *= massScale;
        scale.localScale *= massScale;

        //impulse_vector.x = (float)System.Math.Sin(impulse_angle * System.Math.PI / 2.0f);
        //impulse_vector.y = (float)System.Math.Cos(impulse_angle * System.Math.PI / 2.0f);

        if (!isFlaming)
        {
            rb.AddRelativeForce(impulse_vector * shootPower * massScale * MainScript.forceBatchingMultiplier, ForceMode2D.Impulse);
        }

        GameObject fs_obj;
        if (fs_obj = GameObject.FindGameObjectWithTag("ForceShield"))
            fs = fs_obj.GetComponent<ForceShieldScript>();
    }

    private void Update()
    {
        if (isFlaming)
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFlaming)
        {
            if (collision.gameObject.layer == 13)
            {
                if (fs)
                {
                    fs.TakingDamage(damage * massScale);
                    joint.enabled = true;
                    joint.connectedBody = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<Rigidbody2D>();
                }
            }
        }
        if (isFlaming)
        {
            if (collision.gameObject.layer == 13)
            {
            }
            else
            {
               
            }
        }
    }
}
