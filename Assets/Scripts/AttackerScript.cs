using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerScript : MonoBehaviour
{
    public bool isAgressive = true;
    public GameObject muzzle;
    public GameObject bullet;
    bool scared;
    bool aiming = false;
    OnRay onRay;
    Transform player;
    bool faceRight = false;
    Rigidbody2D rb;
    Animator anim;
    float aimingtime;
    public float shootingtime = 3;

    public float distanse;
    public float aiming_distanse;
    public float cowPower = 1.0f;
    float cowPowerindex = 1.0f;
    float startMass;

    public bool isGrounded;

    float waitTime;
    public float startWaitTime;
    public float walkSpeed = 1.0f;
    float randomSpot;

    AudioSource scream;
    int s;
    public AudioSource aim;
    int a;

    int currentanimation = 0; // 1 - walk, 2 - fly, 3 - fear
    float current_animation_position = 0.0f;
    void Start()
    {
        waitTime = startWaitTime;
        SetNextSpotToMove();
        scream = GetComponent<AudioSource>();
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
        else if (System.Math.Abs(player.position.x - transform.position.x) > distanse && scared)
        {
            scared = false;
            aiming = false;
            aimingtime = 0;
        }
        else if (System.Math.Abs(player.position.x - transform.position.x) < aiming_distanse && !scared)
        {
            if (!aiming)
            {
                aiming = true;
                aimingtime = 0;
            }
        }
        else 
        {
            a = 0;
            aiming = false;
        }

        if (scared)
        {
            InFear();
        }
        else
        {
            if (aiming)
            {
                Aiming();
            }
            else
            {
                if (!isAgressive)
                {
                    Chill();
                }
                if (isAgressive)
                {
                    Attacking();
                }
            }
        }
    }

    void Aiming() {
        double distanceToUFO_x = player.position.x - transform.position.x;
        double distanceToUFO_y = player.position.y - transform.position.y;

        if(a == 0)
        {
            aim.pitch = Random.Range(0.8f, 1.1f);
            aim.Play();
            a++;
        }
        if (distanceToUFO_x > 0)
        {
            RightRotate();
        }
        else
        {
            LeftRotate();
        }

        float anim_position = (float)((System.Math.Atan2(System.Math.Abs(distanceToUFO_x), distanceToUFO_y) * 2.0) / System.Math.PI);

        if (anim_position > 1.0f)
            anim_position = 1.0f;
        if (anim_position < 0.0f)
            anim_position = 1.0f;

        aimingtime += Time.deltaTime;

        if (aimingtime > shootingtime) {
            if(distanceToUFO_x > 0)
                Shoot(anim_position);
            else
                Shoot(-anim_position);
            aimingtime = 0;
        }

        ChangeAnimation(4, anim_position);
    }
    
    void Shoot(float anim_position)
    {
        a = 0;                                                      //for aim sound
        GameObject newbullet = Instantiate(bullet, muzzle.transform.position, Quaternion.Euler(0, 0, -90.0f * anim_position));
        BulletScript newbulletscript = newbullet.GetComponent<BulletScript>();
        newbulletscript.impulse_angle = anim_position;
        newbulletscript.massScale = (float) onRay.massScale;  
    }

    void SetNextSpotToMove()
    {
        randomSpot = transform.position.x + UnityEngine.Random.Range(-10, 10);

        float distanceToTarget = randomSpot - transform.position.x;

        if (distanceToTarget > 0)
        {
            RightRotate();
        }
        else
        {
            LeftRotate();
        }
    }

    void ChangeAnimation(int new_animation, float position = 0.0f)
    {
        if (currentanimation == new_animation && current_animation_position == position)
            return;
        currentanimation = new_animation;
        current_animation_position = position;
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
        if (currentanimation == 4)
        {
            anim.speed = 0.0f;
            anim.Play("Hunter", 0, position);
        }
        else
        {
            anim.speed = 1.0f;
        }
    }
    void Chill()
    {
        s = 0;                                              //for audio screem
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
                movingVector *= (walkSpeed);
                ChangeAnimation(1);
                rb.AddForce(new Vector3(movingVector, 0, 0) * cowPowerindex * Time.deltaTime * MainScript.forceBatchingMultiplier);
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
            if (waitTime <= 0)
            {
                SetNextSpotToMove();
            }
        }

    }

    void InFear()
    {
        if(s == 0)
        {
            scream.pitch = Random.Range(0.8f, 1.1f);
            scream.Play();
            s++;
        }
        if (isGrounded)
        {
            ChangeAnimation(3);
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
        rb.AddForce(Vector3.right * cowPower * cowPowerindex * Time.deltaTime * MainScript.forceBatchingMultiplier);
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
        rb.AddForce(Vector3.left * cowPower * cowPowerindex * Time.deltaTime * MainScript.forceBatchingMultiplier);
    }
    void Attacking()
    {
        s = 0;                                              //for audio screem
        ChangeAnimation(3);
        if (transform.position.x < player.position.x)
        {
            RightRunning();
        }
        if (transform.position.x > player.position.x)
        {
            LeftRunning();
        }

    }
}
