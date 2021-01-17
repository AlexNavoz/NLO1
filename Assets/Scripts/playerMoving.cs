using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMoving : MonoBehaviour
{
    MainScript mainScript;

    public float EnginePower;

    public PressedButton leftButton;
    public PressedButton rightButton;
    GameObject leftEngine;
    Rigidbody2D rbLeft;
    GameObject rightEngine;
    Rigidbody2D rbRight;
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;

    //fuel bar variables
    public float maxFuel;
    public float consumption = 0.01f;
    public float currentFuel;
    public FuelBarScript fuelBar;
    string lname;




    private void Awake()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.shieldIsActive = true;
        mainScript.LoadPlatePrefs();
        mainScript.LoadShortPlatePrefs();
    }
    void Start()
    {
        

        EnginePower = mainScript.P_enginePower;

        
        SetFuelValues();


        leftEngine = GameObject.FindGameObjectWithTag("LeftEngine");
        rightEngine = GameObject.FindGameObjectWithTag("RightEngine");
        rbLeft = leftEngine.GetComponent<Rigidbody2D>();
        rbRight = rightEngine.GetComponent<Rigidbody2D>();
        lname = SceneManager.GetActiveScene().name;

    }

    void FixedUpdate()
    {
        if (currentFuel > 0)
        {

            if (Input.GetKey(KeyCode.A) || leftButton.isPressed)
            {
                rbLeft.AddRelativeForce(Vector3.up * EnginePower);
                if (lname!= "Main menu")
                {
                    FuelConsampsion(consumption);
                }
                leftParticle.Play();
            }
            else { leftParticle.Stop(); }
            if (Input.GetKey(KeyCode.D) || rightButton.isPressed)
            {
                rbRight.AddRelativeForce(Vector3.up * EnginePower);
                if (lname != "Main menu")
                {
                    FuelConsampsion(consumption);
                }
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
    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    public void SetFuelValues()
    {
        maxFuel = mainScript.P_maxFuel;
        currentFuel = mainScript.P_fuelLevel;
        if (currentFuel < maxFuel)
        {
            fuelBar.SetMaxTank(maxFuel);

            fuelBar.SetValue(currentFuel);
        }
        else
        {
            fuelBar.SetMaxTank(maxFuel);

            fuelBar.SetValue(maxFuel);
        }
    }

    public void ReloadPlatePrefs()
    {
        mainScript.LoadPlatePrefs();
        EnginePower = mainScript.P_enginePower;
        maxFuel = mainScript.P_maxFuel;
    }
}
