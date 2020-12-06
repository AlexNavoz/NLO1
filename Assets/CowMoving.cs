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

    public float distanse;
    public float cowPower = 10.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        onRay = GetComponent<OnRay>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
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
            Chill();
        }
    }

    void Chill()
    {
        Debug.Log("fuf");
    }

    void InFear()
    {
        if (transform.position.x > player.position.x)
        {
            RightRunning();
        }
        if (transform.position.x < player.position.x)
        {
            LeftRunning();
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
        rb.AddForce(Vector3.right * cowPower * Time.deltaTime);
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
        rb.AddForce(Vector3.left * cowPower * Time.deltaTime);
    }
    void Flip()
    {
        
        
    }
}
