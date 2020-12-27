using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarShipMoving : MonoBehaviour
{
    public GameObject rocket;
    public Transform firePoint;
    
    
    
    public float EnginePower = 50.0f;
    public float backJetPower = 100.0f;

    public Slider leftSlider;
    public Slider rightSlider;
    GameObject leftEngine;
    Rigidbody2D rbLeft;
    GameObject rightEngine;
    Rigidbody2D rbRight;
    Rigidbody2D assJet;
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;
    public ParticleSystem backParticle;
    bool backButton;

    int i = 0;

    //fuel bar variables
    public float maxFuel = 100.0f;
    public float consumption = 0.01f;
    float currentFuel;
    public FuelBarScript fuelBar;

    void Start()
    {
        leftSlider = GameObject.FindGameObjectWithTag("LeftSlider").GetComponent<Slider>();
        rightSlider = GameObject.FindGameObjectWithTag("RightSlider").GetComponent<Slider>();

        currentFuel = maxFuel;
        fuelBar.SetMaxTank(maxFuel);

        leftEngine = GameObject.FindGameObjectWithTag("LeftEngine");
        rightEngine = GameObject.FindGameObjectWithTag("RightEngine");
        rbLeft = leftEngine.GetComponent<Rigidbody2D>();
        rbRight = rightEngine.GetComponent<Rigidbody2D>();
        assJet = GameObject.FindGameObjectWithTag("BackJet").GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (currentFuel > 0)
        {
            if (backButton)
            {
                assJet.AddRelativeForce(Vector3.right * backJetPower);
                backParticle.Play();
            }
            else backParticle.Stop();

            if (leftSlider.value!=0)
            {
                rbLeft.AddRelativeForce(Vector3.up * EnginePower*leftSlider.value);
                FuelConsampsion(consumption);
                leftParticle.Play();
            }
            else { leftParticle.Stop(); }
            if (rightSlider.value!=0)
            {
                rbRight.AddRelativeForce(Vector3.up * EnginePower*rightSlider.value);
                FuelConsampsion(consumption);
                rightParticle.Play();
            }

            else { rightParticle.Stop(); }
        }
        else
        {
            leftParticle.Stop();
            rightParticle.Stop();
        }

    }

    public void FuelConsampsion(float Consumption)
    {
        currentFuel -= Consumption;
        fuelBar.SetValue(currentFuel);
    }
    public void IsClickedButton()
    {
        i++;
        if (i%2 == 1)
        {
            backButton = true;
        }
        if (i%2 == 0)
        {
            backButton = false;
            i -= 2;
        }
    }
    public void Fire()
    {
        Instantiate(rocket, firePoint.position, transform.rotation);
    }
}
