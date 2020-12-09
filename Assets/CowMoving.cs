using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMoving : MonoBehaviour
{
    bool scared;
    OnRay onRay;
    Transform player;
    float cowSpeed = 1.0f;
    bool faceRight;
    Rigidbody2D rb;
    Animator anim;

    public float distanse;
    public float cowPower = 1.0f;
    float cowPowerindex = 1.0f;
    float startMass;

    public bool isGrounded;
    void Start()
    {
        startMass = GetComponent<Rigidbody2D>().mass;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        onRay = GetComponent<OnRay>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        cowPowerindex = rb.mass / startMass;
        if (onRay.isInRay&& !scared)
        {
            scared = true;
        }

        if (scared)
        {
            InFear();
        }

        if ((player.position.x -transform.position.x)>distanse|| (player.position.x - transform.position.x) < -distanse)
        {
            scared = false;
            Chill();
        }
    }

    void Chill()
    {
        anim.SetBool("isInFear", false);
        anim.SetBool("isFly", false);
        Debug.Log("fuf");
    }

    void InFear()
    {
        if (isGrounded)
        {
            anim.SetBool("isFly", false);
            anim.SetBool("isInFear", true);
            if (transform.position.x > player.position.x)
            {
                RightRunning();
            }
            if (transform.position.x < player.position.x)
            {
                LeftRunning();
            }
        }
        if (!isGrounded)
        {

            anim.SetBool("isInFear", false);
            anim.SetBool("isFly", true);
        }
       
    }
    void RightRunning()
    {
        Quaternion cowRotation = transform.rotation;
        if (!faceRight)
        {
            cowRotation.y = 180.0f;
            faceRight = true;
            transform.rotation = cowRotation;
        }
        rb.AddForce(Vector3.right * cowPower *cowPowerindex* Time.deltaTime);
    }
    void LeftRunning()
    {
        Quaternion cowRotation = transform.rotation;
        if (faceRight)
        {
            cowRotation.y = 0f;
            faceRight = false;
            transform.rotation = cowRotation;
        }
        rb.AddForce(Vector3.left * cowPower * cowPowerindex * Time.deltaTime);
    }
    void Flip()
    {
        
        
    }
}
