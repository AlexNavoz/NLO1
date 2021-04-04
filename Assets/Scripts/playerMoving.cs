using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMoving : MonoBehaviour
{
    MainScript mainScript;
    int ShipIndex;
    Rigidbody2D rb;
    GameObject forceShield;
    ForceShieldScript fsScript;
    int i = 0;
    Canvas canvas;
    Camera mainCamera;
    public AudioSource jetSound1;
    public AudioSource jetSound2;
    public AudioSource deathSound;
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
    // Knippel Variables!!!
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    public Transform leftJet;
    public Transform rightJet;

    GameObject leftEngine;
    Rigidbody2D rbLeft;
    GameObject rightEngine;
    Rigidbody2D rbRight;
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;
    public bool isDead;
    public bool canDie;

    //fuel bar variables
    public float maxFuel;
    public float consumption = 5.0f;
    public float currentFuel;
    public FuelBarScript fuelBar;
    string lname;

    //refuel
    LooseScreenScript refuelCanvas;
    public bool alreadyRefueled = false;

    //salary
    public GameObject salary;

    //Boxes
    public GameObject[] boxes = new GameObject[] { };

    //Criminal Stars
    public GameObject crimePanel;
    public GameObject[] criminalStars;
    
    public int crimeIndex = 0;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.shieldIsActive = true;
        crimeIndex = 0;
        ShipIndex = PlayerPrefs.GetInt("ShipIndex", 0);
        if (ShipIndex == 0)                                            
        {
            mainScript.LoadPlatePrefs();
            mainScript.LoadShortPlatePrefs();
        }
        if (ShipIndex == 1)                                                    
        {
            mainScript.LoadWSPrefs();
            mainScript.LoadShortWSPrefs();
        }
        if (ShipIndex == 2)
        {
            mainScript.LoadKPrefs();
            mainScript.LoadShortKPrefs();
        }

    }
    void Start()
    {
        Time.timeScale = 1;

        if (ShipIndex == 0)
        {
            EnginePower = mainScript.P_enginePower;
        }
        if (ShipIndex == 1)                                         
        {
            EnginePower = mainScript.WS_enginePower;
            leftSlider = GameObject.FindGameObjectWithTag("LeftSlider").GetComponent<Slider>();
            rightSlider = GameObject.FindGameObjectWithTag("RightSlider").GetComponent<Slider>();
        }
        if (ShipIndex == 2)
        {
            EnginePower = mainScript.K_enginePower;
        }
        leftEngine = GameObject.FindGameObjectWithTag("LeftEngine");
        rightEngine = GameObject.FindGameObjectWithTag("RightEngine");
        rbLeft = leftEngine.GetComponent<Rigidbody2D>();
        rbRight = rightEngine.GetComponent<Rigidbody2D>();
        lname = SceneManager.GetActiveScene().name;
        forceShield = GameObject.FindGameObjectWithTag("ForceShield");
        fsScript = forceShield.GetComponent<ForceShieldScript>();
        SetFuelValues();
        rb = GetComponent<Rigidbody2D>();
        if (mainScript.levelIndex == 1)
        {
            crimePanel.SetActive(true);
        }
        else
        {
            crimePanel.SetActive(false);
        }
        
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
            if (transform.position.y < -0.7&& mainScript.levelIndex!=0&& mainScript.levelIndex != 2)
            {
                rb.gravityScale = 2.5f;
            }
            else
            {
                if (ShipIndex != 2)
                {
                    rb.gravityScale = 1;
                }
                else rb.gravityScale = 0;
            }
            if (currentFuel > 0)
            {
                if (ShipIndex == 0)
                {
                    if (Input.GetKey(KeyCode.A) || leftButton.isPressed)
                    {
                        rb.drag = 1;
                        rbLeft.AddRelativeForce(Vector3.up * EnginePower);
                        if(!jetSound1.isPlaying)
                        jetSound1.Play();
                        if (mainScript.levelIndex!=0)
                        {
                            FuelConsampsion(consumption);
                        }
                        leftParticle.Play();
                    }
                    else
                    {
                        rb.drag = 5;
                        leftParticle.Stop();
                        jetSound1.Stop();
                    }
                    if (Input.GetKey(KeyCode.D) || rightButton.isPressed)
                    {
                        rb.drag = 1;
                        rbRight.AddRelativeForce(Vector3.up * EnginePower);
                        if (!jetSound2.isPlaying)
                            jetSound2.Play();
                        if (mainScript.levelIndex != 0)
                        {
                            FuelConsampsion(consumption);
                        }
                        rightParticle.Play();
                    }

                    else 
                    {
                        rb.drag = 5;
                        rightParticle.Stop();
                        jetSound2.Stop();
                    }
                }
                if (ShipIndex == 1)                                                    //After MainScript
                {
                    if (leftSlider.value != 0)
                    {
                        rb.drag = 1;
                        if (mainScript.levelIndex != 0)
                        {
                            FuelConsampsion(consumption);
                        }
                        rbLeft.AddRelativeForce(Vector3.up * EnginePower * leftSlider.value);
                        if (!jetSound1.isPlaying)
                            jetSound1.Play();
                        leftParticle.Play();
                    }
                    else 
                    {
                        leftParticle.Stop();
                        jetSound1.Stop();
                    }
                    if (rightSlider.value != 0)
                    {
                        rb.drag = 1;
                        if (mainScript.levelIndex != 0)
                        {
                            FuelConsampsion(consumption);
                        }
                        rbRight.AddRelativeForce(Vector3.up * EnginePower * rightSlider.value);
                        if (!jetSound2.isPlaying)
                            jetSound2.Play();
                        rightParticle.Play();
                    }
                    else 
                    {
                        rightParticle.Stop();
                        jetSound2.Stop();
                    }
                    if(rightSlider.value == 0|| leftSlider.value == 0)
                    {
                        rb.drag = 5;
                    }
                }
                if (ShipIndex == 2)                                                    //KNIPPEL!!
                {
                    rb.gravityScale = 0;
                    if (leftJoystick.Horizontal != 0|| leftJoystick.Vertical != 0)
                    {
                        rb.drag = 2;
                        if (mainScript.levelIndex != 0)
                        {
                            FuelConsampsion(consumption);
                        }
                        float leftJetRotation = (float)System.Math.Atan2(leftJoystick.Horizontal, leftJoystick.Vertical);                        
                        leftJet.rotation = Quaternion.Euler(0, 0, -180.0f * leftJetRotation / (float)System.Math.PI);
                        rbLeft.AddForce(new Vector3(leftJoystick.Horizontal, leftJoystick.Vertical,0) * EnginePower);
                        if (!jetSound1.isPlaying)
                            jetSound1.Play();
                        leftParticle.Play();
                    }
                    else
                    {
                        leftParticle.Stop();
                        jetSound1.Stop();
                    }
                    if (rightJoystick.Horizontal != 0 || rightJoystick.Vertical != 0)
                    {
                        rb.drag = 2;
                        if (mainScript.levelIndex != 0)
                        {
                            FuelConsampsion(consumption);
                        }
                        float rightJetRotation = (float)System.Math.Atan2(rightJoystick.Horizontal, rightJoystick.Vertical);
                        rightJet.rotation = Quaternion.Euler(0, 0,-180* rightJetRotation / (float)System.Math.PI);
                        rbRight.AddForce(new Vector3(rightJoystick.Horizontal, rightJoystick.Vertical, 0) * EnginePower);
                        if (!jetSound2.isPlaying)
                            jetSound2.Play();
                        rightParticle.Play();
                    }

                    else
                    {
                        rightParticle.Stop();
                        jetSound2.Stop();
                    }
                    if (rightJoystick.Horizontal == 0 || rightJoystick.Vertical == 0|| leftJoystick.Horizontal == 0 || leftJoystick.Vertical == 0)
                    {
                        rb.drag = 20;
                    }
                }
            }
            else
            {
                rb.drag = 1;
                leftParticle.Stop();
                rightParticle.Stop();
                jetSound1.Stop();
                jetSound2.Stop();
                if(ShipIndex == 2)
                {
                    rb.gravityScale = 1;
                }
            }
        }
        if (isDead)
        {
            leftParticle.Stop();
            rightParticle.Stop();
            jetSound1.Stop();
            jetSound2.Stop();
            if (ShipIndex == 2)
            {
                rb.gravityScale = 1;
            }
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
    private void Update()
    {
        if (fsScript.currentHP <= 0)
        {
            mainScript.shieldIsActive = false;
            canDie = true;
            forceShield.SetActive(false);
        }
        else
        {
            forceShield.SetActive(true);
            mainScript.shieldIsActive = true;
            canDie = false;
        }

        if (mainScript.levelIndex == 1)
        {
            if (mainScript.collection > 200 && !criminalStars[0].activeSelf)
            {
                crimeIndex = 1;
                criminalStars[crimeIndex - 1].SetActive(true);
            }
            if (mainScript.collection > 700 && !criminalStars[1].activeSelf)
            {
                crimeIndex = 2;
                criminalStars[crimeIndex - 1].SetActive(true);
            }
            if (mainScript.collection > 1500 && !criminalStars[2].activeSelf)
            {
                crimeIndex = 3;
                criminalStars[crimeIndex - 1].SetActive(true);
            }
            if (mainScript.collection > 3000 && !criminalStars[3].activeSelf)
            {
                crimeIndex = 4;
                criminalStars[crimeIndex - 1].SetActive(true);
            }
            if (mainScript.collection >5000 && !criminalStars[4].activeSelf)
            {
                crimeIndex = 5;
                criminalStars[crimeIndex - 1].SetActive(true);
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
                    deathSound.Play();
                    OpenLosePanel();
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
                OpenRefuelPanel();
            }
        }
    }
    public void FuelConsampsion(float Consumption)
    {
        currentFuel -= Consumption * Time.deltaTime;
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
            maxFuel = mainScript.WS_maxFuel;
            currentFuel = mainScript.WS_fuelLevel;
            if (currentFuel < maxFuel)
            {
                mainScript.WS_maxFuel = maxFuel;
                mainScript.WS_fuelLevel = currentFuel;
                fuelBar.SetMaxTank(maxFuel);

                fuelBar.SetValue(currentFuel);

            }
            else
            {
                fuelBar.SetMaxTank(maxFuel);
                currentFuel = maxFuel;
                fuelBar.SetValue(currentFuel);

                mainScript.WS_maxFuel = maxFuel;
                mainScript.WS_fuelLevel = currentFuel;
            }
        }
        if (ShipIndex == 2)
        {
            maxFuel = mainScript.K_maxFuel;
            currentFuel = mainScript.K_fuelLevel;
            if (currentFuel < maxFuel)
            {
                mainScript.K_maxFuel = maxFuel;
                mainScript.K_fuelLevel = currentFuel;
                fuelBar.SetMaxTank(maxFuel);

                fuelBar.SetValue(currentFuel);

            }
            else
            {
                fuelBar.SetMaxTank(maxFuel);
                currentFuel = maxFuel;
                fuelBar.SetValue(currentFuel);

                mainScript.K_maxFuel = maxFuel;
                mainScript.K_fuelLevel = currentFuel;
            }
        }
    }

    public void ReloadPlatePrefs()
    {
        mainScript.LoadPlatePrefs();
        EnginePower = mainScript.P_enginePower;
        maxFuel = mainScript.P_maxFuel;
    }
    public void ReloadWSPrefs()
    {
        mainScript.LoadWSPrefs();
        EnginePower = mainScript.WS_enginePower;
        maxFuel = mainScript.WS_maxFuel;
    }
    public void ReloadKPrefs()
    {
        mainScript.LoadKPrefs();
        EnginePower = mainScript.K_enginePower;
        maxFuel = mainScript.K_maxFuel;
    }

    public void OpenLosePanel()
    {
        try
        {
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                LooseScreenScript losePanel = GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<LooseScreenScript>();
                losePanel.Activation();
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                ShortMenuScript shortMenu = GameObject.FindGameObjectWithTag("ShortMenu").GetComponent<ShortMenuScript>();
                shortMenu.StartShortMenu();
            }
        }
        catch
        {
            SceneManager.LoadScene("Main menu");
        }
    }
    public void OpenRefuelPanel()
    {
        refuelCanvas = GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<LooseScreenScript>();
        refuelCanvas.RefuelCanvasOpen();
    }
    
}
