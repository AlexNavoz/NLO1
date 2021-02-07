using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMoving : MonoBehaviour
{
    MainScript mainScript;
    int ShipIndex;
    public GameObject forceShield;
    int i = 0;
    Canvas canvas;
    Camera mainCamera;

    public float EnginePower;

    // Plate Variables!!!
    [SerializeField] GameObject Plate_Variables_______;
    public PressedButton leftButton;
    public PressedButton rightButton;
    // WS Variables!!!
    [SerializeField] GameObject WS_Variables_______;
    public Slider leftSlider;
    public Slider rightSlider;
    public GameObject gun;


    GameObject leftEngine;
    Rigidbody2D rbLeft;
    GameObject rightEngine;
    Rigidbody2D rbRight;
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;
    bool isDead;
    public bool canDie;

    //fuel bar variables
    public float maxFuel;
    public float consumption = 0.01f;
    public float currentFuel;
    public FuelBarScript fuelBar;
    string lname;

    //refuel
    LooseScreenScript refuelCanvas;
    public bool alreadyRefueled = false;

    //salary
    public GameObject salary;


    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.shieldIsActive = true;
        ShipIndex = PlayerPrefs.GetInt("ShipIndex", 0);
        if (ShipIndex == 0)                                            
        {
            mainScript.LoadPlatePrefs();
            mainScript.LoadShortPlatePrefs();
        }
        if (ShipIndex == 1)                                                    //After MainScript
        {
            mainScript.LoadPlatePrefs();
            mainScript.LoadShortPlatePrefs();
        }

    }
    void Start()
    {
        Time.timeScale = 1;

        if (ShipIndex == 0)
        {
            EnginePower = mainScript.P_enginePower;
        }
        if (ShipIndex == 1)                                                    //Change After MainScript
        {
            EnginePower = mainScript.P_enginePower;
            leftSlider = GameObject.FindGameObjectWithTag("LeftSlider").GetComponent<Slider>();
            rightSlider = GameObject.FindGameObjectWithTag("RightSlider").GetComponent<Slider>();
        }

        SetFuelValues();
        leftEngine = GameObject.FindGameObjectWithTag("LeftEngine");
        rightEngine = GameObject.FindGameObjectWithTag("RightEngine");
        rbLeft = leftEngine.GetComponent<Rigidbody2D>();
        rbRight = rightEngine.GetComponent<Rigidbody2D>();
        lname = SceneManager.GetActiveScene().name;


    }

    public void showTextValue(GameObject obj, string text, int type) {
        GameObject textobj = Instantiate(salary);
        textobj.transform.SetParent(canvas.transform);
        SalaryShowing sh = textobj.GetComponent<SalaryShowing>();
        sh.setTextAndPosition(canvas,mainCamera,obj,text, type);
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            if (currentFuel > 0)
            {
                if (ShipIndex == 0)
                {
                    if (Input.GetKey(KeyCode.A) || leftButton.isPressed)
                    {
                        rbLeft.AddRelativeForce(Vector3.up * EnginePower);
                        if (lname != "Main menu")
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
                if (ShipIndex == 1)                                                    //After MainScript
                {
                    if (leftSlider.value != 0)
                    {
                        if (lname != "Main menu")
                        {
                            FuelConsampsion(consumption);
                        }
                        rbLeft.AddRelativeForce(Vector3.up * EnginePower * leftSlider.value);
                        leftParticle.Play();
                    }
                    else { leftParticle.Stop(); }
                    if (rightSlider.value != 0)
                    {
                        if (lname != "Main menu")
                        {
                            FuelConsampsion(consumption);
                        }
                        rbRight.AddRelativeForce(Vector3.up * EnginePower * rightSlider.value);;
                        rightParticle.Play();
                    }

                    else { rightParticle.Stop(); }
                }
            }
            else
            {
                leftParticle.Stop();
                rightParticle.Stop();
            }
        }
        if (isDead)
        {
            leftParticle.Stop();
            rightParticle.Stop();
        }

        if(mainScript.levelIndex == 2)
        {
            if(currentFuel<=0 && !alreadyRefueled)
            {
                alreadyRefueled = true;
                Invoke("OpenRefuelPanel", 1.0f);
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 11|| collision.gameObject.layer == 19)
        {
            if (canDie)
            { i++;
                if (i == 1)
                {
                    isDead = true;
                    Invoke("OpenLosePanel", 2.0f);
                }
            }
        }
        if (collision.gameObject.layer == 9)
        {
            if (alreadyRefueled && currentFuel <= 0 && !isDead)
            {
                i++;
                    if (i == 1) 
                {
                    isDead = true;
                    Invoke("OpenLosePanel", 2.0f); 
                }
            }
            if (!alreadyRefueled && currentFuel <= 0)
            {
                Invoke("OpenRefuelPanel", 1.0f);
            }
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
        if (ShipIndex == 0)
        {
            maxFuel = mainScript.P_maxFuel;
            currentFuel = mainScript.P_fuelLevel;
            if (currentFuel < maxFuel)
            {
                mainScript.P_maxFuel = maxFuel;
                mainScript.P_fuelLevel = currentFuel;
                fuelBar.SetMaxTank(maxFuel);

                fuelBar.SetValue(currentFuel);

            }
            else
            {
                fuelBar.SetMaxTank(maxFuel);
                currentFuel = maxFuel;
                fuelBar.SetValue(currentFuel);

                mainScript.P_maxFuel = maxFuel;
                mainScript.P_fuelLevel = currentFuel;
            }
        }
        if (ShipIndex == 1)
        {
            maxFuel = mainScript.P_maxFuel;
            currentFuel = mainScript.P_fuelLevel;
            if (currentFuel < maxFuel)
            {
                mainScript.P_maxFuel = maxFuel;
                mainScript.P_fuelLevel = currentFuel;
                fuelBar.SetMaxTank(maxFuel);

                fuelBar.SetValue(currentFuel);

            }
            else
            {
                fuelBar.SetMaxTank(maxFuel);
                currentFuel = maxFuel;
                fuelBar.SetValue(currentFuel);

                mainScript.P_maxFuel = maxFuel;
                mainScript.P_fuelLevel = currentFuel;
            }
        }
    }

    public void ReloadPlatePrefs()
    {
        mainScript.LoadPlatePrefs();
        EnginePower = mainScript.P_enginePower;
        maxFuel = mainScript.P_maxFuel;
    }

    public void OpenLosePanel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            LooseScreenScript losePanel = GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<LooseScreenScript>();
            losePanel.Activation();
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Application.Quit();
        }
    }
    public void OpenRefuelPanel()
    {
        refuelCanvas = GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<LooseScreenScript>();
        refuelCanvas.RefuelCanvasOpen();
    }
}
