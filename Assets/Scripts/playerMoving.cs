using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMoving : MonoBehaviour
{
    public float EnginePower = 30.0f;

    public PressedButton leftButton;
    public PressedButton rightButton;
    GameObject leftEngine;
    Rigidbody2D rbLeft;
    GameObject rightEngine;
    Rigidbody2D rbRight;
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;

    //fuel bar variables
    public float maxFuel = 100.0f;
    public float consumption = 0.01f;
    float currentFuel;
    public FuelBarScript fuelBar;
    void Start()
    {
        currentFuel = maxFuel;
        fuelBar.SetMaxTank(maxFuel);

        leftEngine = GameObject.FindGameObjectWithTag("LeftEngine");
        rightEngine = GameObject.FindGameObjectWithTag("RightEngine");
        rbLeft = leftEngine.GetComponent<Rigidbody2D>();
        rbRight = rightEngine.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (currentFuel > 0)
        {

            if (Input.GetKey(KeyCode.A) || leftButton.isPressed)
            {
                rbLeft.AddRelativeForce(Vector3.up * EnginePower);
                FuelConsampsion(consumption);
                leftParticle.Play();
            }
            else { leftParticle.Stop(); }
            if (Input.GetKey(KeyCode.D) || rightButton.isPressed)
            {
                rbRight.AddRelativeForce(Vector3.up * EnginePower);
                FuelConsampsion(consumption);
                rightParticle.Play();
            }

            else { rightParticle.Stop(); }
        }
        else 
        {   leftParticle.Stop();
            rightParticle.Stop();
        }

    }

    public void FuelConsampsion(float Consumption)
    {
        currentFuel -= Consumption;
        fuelBar.SetValue(currentFuel);
    }
}
