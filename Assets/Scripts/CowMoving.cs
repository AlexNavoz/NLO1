using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMoving : MonoBehaviour
{
    bool scared;
    OnRay onRay;
    Transform player;
    bool faceRight = false;
    Rigidbody2D rb;
    Animator anim;

    public float distanse;
    public float cowPower = 1.0f;
    float cowPowerindex = 1.0f;
    float startMass;

    public bool isGrounded;

    float waitTime;
    public float startWaitTime;
    public float walkSpeed = 1.0f;
    float randomSpot;

    int currentanimation = 0; // 1 - walk, 2 - fly, 3 - fear
    void Start()
    {
        waitTime = startWaitTime;
        SetNextSpotToMove();

        startMass = GetComponent<Rigidbody2D>().mass;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        onRay = GetComponent<OnRay>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        cowPowerindex = rb.mass / startMass;
        if (onRay.isInRay)
        {
            if (!scared)
            {
                scared = true;
                waitTime = 1;
            }
        }
        else if ((player.position.x - transform.position.x) > distanse || (player.position.x - transform.position.x) < -distanse)
        {
            scared = false;
        }

        if (scared)
        {
            InFear();
        }
        else
        {
            Chill();
        }
    }

    void SetNextSpotToMove()
    {
        randomSpot = transform.position.x + UnityEngine.Random.Range(-10, 10);

        float distanceToTarget = randomSpot - transform.position.x;

        Quaternion cowRotation = transform.rotation;
        if (distanceToTarget > 0)
        {
            RightRotate();
        }
        else
        {
            LeftRotate();
        }
    }

    void ChangeAnimation(int new_animation)
    {
        if (currentanimation == new_animation)
            return;
        currentanimation = new_animation;
        if (currentanimation == 1)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);
        if (currentanimation == 2)
            anim.SetBool("isFly", true);
        else
            anim.SetBool("isFly", false);
        if (currentanimation == 3)
            anim.SetBool("isInFear", true);
        else
            anim.SetBool("isInFear", false);
    }
    void Chill()
    {

        if (waitTime <= 0)
        {
            float distanceToTarget = randomSpot - transform.position.x;

            if (Mathf.Abs(distanceToTarget) >= 0.5f)
            {
                float movingVector = 1.0f;
                if (distanceToTarget < 0)
                {
                    movingVector = -1.0f;
                }
                movingVector *= (walkSpeed * Time.deltaTime);
                ChangeAnimation(1);
                rb.AddForce(new Vector3(movingVector, 0, 0) * walkSpeed*cowPowerindex);
            }
            else
            {
                ChangeAnimation(0);
                waitTime = UnityEngine.Random.Range(1, 5);
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
            ChangeAnimation(0);
            if (waitTime <= 0) {
                SetNextSpotToMove();
            }
        }

    }

    void InFear()
    {
        if (isGrounded)
        {
            ChangeAnimation(3);
            //anim.SetBool("isFly", false);
            //anim.SetBool("isInFear", true);
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
            ChangeAnimation(2);
            //anim.SetBool("isInFear", false);
            //anim.SetBool("isFly", true);
        }

    }
    void RightRotate()
    {
        Quaternion cowRotation = transform.rotation;
        if (!faceRight)
        {
            cowRotation.y = 180.0f;
            cowRotation.x = 0f;
            cowRotation.z = 0f;
            faceRight = true;
            transform.rotation = cowRotation;
        }
    }
    void RightRunning()
    {
        RightRotate();
        rb.AddForce(Vector3.right * cowPower * cowPowerindex * Time.deltaTime);
    }
    void LeftRotate()
    {
        Quaternion cowRotation = transform.rotation;
        if (faceRight)
        {
            cowRotation.y = 0f;
            cowRotation.x = 0f;
            cowRotation.z = 0f;
            faceRight = false;
            transform.rotation = cowRotation;
        }
    }
    void LeftRunning()
    {
        LeftRotate();
        rb.AddForce(Vector3.left * cowPower * cowPowerindex * Time.deltaTime);
    }
    void Flip()
    {


    }
}
