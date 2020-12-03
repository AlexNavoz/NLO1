using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMoving : MonoBehaviour
{
    public float EnginePower = 30.0f;

    public Button leftButton;
    public Button rightButton;
    GameObject leftEngine;
    Rigidbody2D rbLeft;
    GameObject rightEngine;
    Rigidbody2D rbRight;
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;
    void Start()
    {
        leftEngine = GameObject.FindGameObjectWithTag("LeftEngine");
        rightEngine = GameObject.FindGameObjectWithTag("RightEngine");
        rbLeft = leftEngine.GetComponent<Rigidbody2D>();
        rbRight = rightEngine.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rbLeft.AddRelativeForce(Vector3.up * EnginePower);
            leftParticle.Play();
        }
        else { leftParticle.Stop(); }
        if (Input.GetKey(KeyCode.D))
        {
            rbRight.AddRelativeForce(Vector3.up * EnginePower);
            rightParticle.Play();
        }
        else { rightParticle.Stop(); }

    }
}
