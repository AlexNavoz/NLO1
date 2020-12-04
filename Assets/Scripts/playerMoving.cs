using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMoving : MonoBehaviour
{
    public float EnginePower = 30.0f;

    public PressedButton leftButton;
    public PressedButton rightButton;
    public GameObject leftEngine;
    Rigidbody2D rbLeft;
    public GameObject rightEngine;
    Rigidbody2D rbRight;
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;
    void Start()
    {
        rbLeft = leftEngine.GetComponent<Rigidbody2D>();
        rbRight = rightEngine.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)||leftButton.isPressed)
        {
            rbLeft.AddRelativeForce(Vector3.up * EnginePower);
            if (!leftParticle.isPlaying)
            {
                leftParticle.Play(true);
            }
        }
        //else leftParticle.Stop(true);
        if (Input.GetKey(KeyCode.D)||rightButton.isPressed)
        {
            rbRight.AddRelativeForce(Vector3.up * EnginePower);
            //if (!rightParticle.isPlaying)
            
                rightParticle.Play(true);
            
        }
        else rightParticle.Stop(true);

    }
}
