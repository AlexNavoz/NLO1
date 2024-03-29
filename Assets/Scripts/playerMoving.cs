﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    float gunPower;

    //LevelIndex variables!
    public GameObject ray;
    public GameObject destroyer;
    public GameObject gun;
    public GameObject gunMuzzle;
    public GameObject muzzle;
    public GameObject bullet;
    public GameObject claw;
    public GameObject clawController;
    public AudioSource powerUpSound;
    public ParticleSystem shootParticle;
    bool powerUp;
    // EasyShip Variables
    public Joystick joystick;
    // Plate Variables!!!
    [SerializeField] GameObject Plate_Variables_______;
    public PressedButton leftButton;
    public PressedButton rightButton;
    // WS Variables!!!
    [SerializeField] GameObject WS_Variables_______;
    public Slider leftSlider;
    public Slider rightSlider;
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
        ShipIndex = PlayerPrefs.GetInt("ShipIndex", -1);
        if (ShipIndex == -1)
        {
            mainScript.LoadEPrefs();
        }
        if (ShipIndex == 0)                                            
        {
            mainScript.LoadPlatePrefs();
        }
        if (ShipIndex == 1)                                                    
        {
            mainScript.LoadWSPrefs();
        }
        if (ShipIndex == 2)
        {
            mainScript.LoadKPrefs();
        }

    }
    void Start()
    {
        Time.timeScale = 1;
       
        if (ShipIndex == -1)
        {
            EnginePower = mainScript.E_enginePower;
            gunPower = mainScript.E_gunPower;
        }
        if (ShipIndex == 0)
        {
            EnginePower = mainScript.P_enginePower;
            gunPower = mainScript.P_gunPower;
        }
        if (ShipIndex == 1)
        {
            EnginePower = mainScript.WS_enginePower;
            gunPower = mainScript.WS_gunPower;
            leftSlider = GameObject.FindGameObjectWithTag("LeftSlider").GetComponent<Slider>();
            rightSlider = GameObject.FindGameObjectWithTag("RightSlider").GetComponent<Slider>();
        }
        if (ShipIndex == 2)
        {
            EnginePower = mainScript.K_enginePower;
            gunPower = mainScript.K_gunPower;
        }
        if (ShipIndex != -1)
        {
            leftEngine = GameObject.FindGameObjectWithTag("LeftEngine");
            rightEngine = GameObject.FindGameObjectWithTag("RightEngine");
            rbLeft = leftEngine.GetComponent<Rigidbody2D>();
            rbRight = rightEngine.GetComponent<Rigidbody2D>();
        }

        lname = SceneManager.GetActiveScene().name;
        forceShield = GameObject.FindGameObjectWithTag("ForceShield");
        fsScript = forceShield.GetComponent<ForceShieldScript>();
        SetFuelValues();
        rb = GetComponent<Rigidbody2D>();   
        crimePanel.SetActive(false);

        switch (mainScript.levelIndex){
            case 1:
                crimePanel.SetActive(true);
                ray.SetActive(true);
                destroyer.SetActive(true);
                if (mainScript.ShipIndex == 1)
                {
                    gun.SetActive(true);
                }
                else { gun.SetActive(false); };
                claw.SetActive(false);
                clawController.SetActive(false);
                break;
            case 2:
                ray.SetActive(true);
                destroyer.SetActive(true);
                if (mainScript.ShipIndex == 1)
                {
                    gun.SetActive(true);
                }
                else { gun.SetActive(false); }
                claw.SetActive(false);
                clawController.SetActive(false);
                break;
            case 3:
                ray.SetActive(true);
                destroyer.SetActive(true);
                if (mainScript.ShipIndex == 1)
                {
                    gun.SetActive(true);
                }
                else { gun.SetActive(false); }
                claw.SetActive(false);
                clawController.SetActive(false);
                break;
            case 4:
                ray.SetActive(false);
                destroyer.SetActive(false);
                gun.SetActive(true);
                claw.SetActive(false);
                clawController.SetActive(false);
                break;
            case 5:
                Time.fixedDeltaTime = 0.01f;
                MainScript.UpdateStupidTimeMultiplyingConstant();
                ray.SetActive(false);
                destroyer.SetActive(false);
                gun.SetActive(true);
                claw.SetActive(true);
                clawController.SetActive(true);
                break;
        }
        
    }

    public void showTextValue(GameObject obj, string text, int type) {
        GameObject textobj = Instantiate(salary);
        textobj.transform.SetParent(canvas.transform);
        SalaryShowing sh = textobj.GetComponent<SalaryShowing>();
        sh.setTextAndPosition(canvas,mainCamera,obj,text, type);
    }
    public void showQuestText(GameObject obj, string text, int type)
    {
        GameObject textobj = Instantiate(salary);
        textobj.transform.SetParent(canvas.transform);
        SalaryShowing sh = textobj.GetComponent<SalaryShowing>();
        sh.setTextAndPosition(canvas, null, obj, text, type);
    }

    void FixedUpdate()
    {
        if (!mainScript.peace)
        {
            if (!isDead)
            {
                if (transform.position.y < -0.8 && mainScript.levelIndex != 0 && mainScript.levelIndex != 2)
                {
                    rb.gravityScale = 1.4f;
                }
                else
                {
                    if (ShipIndex != 2|| ShipIndex !=-1)
                    {
                        rb.gravityScale = 1;
                    }
                    else rb.gravityScale = 0;
                }
                if (currentFuel > 0)
                {
                    if(ShipIndex == -1)
                    {
                        if(joystick.Horizontal != 0|| joystick.Vertical != 0)
                        {
                            rb.drag = 2;
                            rb.AddForce(new Vector3(joystick.Horizontal, joystick.Vertical, 0) * EnginePower);
                            if (!jetSound1.isPlaying)
                                jetSound1.Play();
                            FuelConsampsion(consumption);
                        }
                        else
                        {
                            rb.drag = 7;
                            jetSound1.Stop();
                        }
                    }
                    if (ShipIndex == 0)
                    {
                        if (Input.GetKey(KeyCode.A) || leftButton.isPressed)
                        {
                            rb.drag = 1;
                            rbLeft.AddRelativeForce(Vector3.up * EnginePower);
                            if (!jetSound1.isPlaying)
                                jetSound1.Play();
                            if (mainScript.levelIndex != 0)
                            {
                                FuelConsampsion(consumption);
                            }
                            leftParticle.Play();
                        }
                        else
                        {
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
                            rightParticle.Stop();
                            jetSound2.Stop();
                        }
                        if (!rightButton.isPressed || !leftButton.isPressed)
                        {
                            rb.drag = 5;
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
                        if (rightSlider.value == 0 || leftSlider.value == 0)
                        {
                            rb.drag = 5;
                        }
                    }
                    if (ShipIndex == 2)                                                    //KNIPPEL!!
                    {
                        rb.gravityScale = 0;
                        if (leftJoystick.Horizontal != 0 || leftJoystick.Vertical != 0)
                        {
                            rb.drag = 2;
                            if (mainScript.levelIndex != 0)
                            {
                                FuelConsampsion(consumption);
                            }
                            float leftJetRotation = (float)System.Math.Atan2(leftJoystick.Horizontal, leftJoystick.Vertical);
                            leftJet.rotation = Quaternion.Euler(0, 0, -180.0f * leftJetRotation / (float)System.Math.PI);
                            rbLeft.AddForce(new Vector3(leftJoystick.Horizontal, leftJoystick.Vertical, 0) * EnginePower);
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
                            rightJet.rotation = Quaternion.Euler(0, 0, -180 * rightJetRotation / (float)System.Math.PI);
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
                        if (rightJoystick.Horizontal == 0 || rightJoystick.Vertical == 0 || leftJoystick.Horizontal == 0 || leftJoystick.Vertical == 0)
                        {
                            rb.drag = 20;
                        }
                }
                    
                }
                else
                {
                    if(ShipIndex != -1)
                    {
                        leftParticle.Stop();
                        rightParticle.Stop();
                        jetSound2.Stop();
                    }
                    rb.drag = 1;
                    jetSound1.Stop();

                    if (ShipIndex == 2)
                    {
                        rb.gravityScale = 1;
                    }
                }
            }
            if (isDead)
            {
                if (ShipIndex != -1)
                {
                    leftParticle.Stop();
                    rightParticle.Stop();
                    jetSound2.Stop();
                }
                jetSound1.Stop();
                if (ShipIndex == 2)
                {
                    rb.gravityScale = 1;
                }
            }

            if (mainScript.levelIndex == 2)
            {
                if (currentFuel <= 0 && !alreadyRefueled)
                {
                    alreadyRefueled = true;
                    Invoke("OpenRefuelPanel", 1.0f);
                }
            }
        }
        else
        {
            rb.drag = 20;
            rb.gravityScale = 0;
            if (ShipIndex != -1)
            {
                leftParticle.Stop();
                rightParticle.Stop();
                jetSound2.Stop();
            }
            jetSound1.Stop();
        }
    }

    float shooting_angle = 0;
    void SetShootingAngle(float new_angle) {
        float rotation_angle = rb.transform.rotation.eulerAngles.z;
        float deltaAngle;

        if (!gun.activeSelf)
            return;

        rotation_angle += 180.0f;

        deltaAngle = (new_angle - rotation_angle + 720.0f) % 360.0f;

        const float limit = 45.0f;
        if (ShipIndex != 1) {
            if (deltaAngle > 180.0f && deltaAngle < (360.0f - limit))
                new_angle = (rotation_angle - limit);
            if (deltaAngle > limit && deltaAngle < 180.0f)
                new_angle = (rotation_angle + limit);
        }

        shooting_angle = new_angle;

        gunMuzzle.transform.rotation = Quaternion.Euler(0, 0, 180.0f + shooting_angle);
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
            if (mainScript.milkCollection > 4 && !criminalStars[0].activeSelf || mainScript.brainsCollection > 1 && !criminalStars[0].activeSelf)
            {
                crimeIndex = 1;
                criminalStars[crimeIndex - 1].SetActive(true);
            }
            if (mainScript.milkCollection > 8 && !criminalStars[1].activeSelf || mainScript.brainsCollection > 4 && !criminalStars[0].activeSelf)
            {
                crimeIndex = 2;
                criminalStars[crimeIndex - 1].SetActive(true);
            }
            if (mainScript.milkCollection > 15 && !criminalStars[2].activeSelf || mainScript.brainsCollection > 8 && !criminalStars[0].activeSelf)
            {
                crimeIndex = 3;
                criminalStars[crimeIndex - 1].SetActive(true);
            }
            if (mainScript.milkCollection > 30 && !criminalStars[3].activeSelf || mainScript.brainsCollection > 15 && !criminalStars[0].activeSelf)
            {
                crimeIndex = 4;
                criminalStars[crimeIndex - 1].SetActive(true);
            }
            if (mainScript.milkCollection >50 && !criminalStars[4].activeSelf || mainScript.brainsCollection > 30 && !criminalStars[0].activeSelf)
            {
                crimeIndex = 5;
                criminalStars[crimeIndex - 1].SetActive(true);
            }
        }
        //_____-Shooting
        #region
        if (fsScript.currentHP > 0)
        {
            if (gun.activeSelf)
            {
                bool touching = false;
                bool shooting = false;
                Vector2 touchPosition = new Vector2(0, 0);
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch t = Input.GetTouch(i);
                    switch (t.phase)
                    {
                        case TouchPhase.Began:
                            {
                                if (aiming_touchid == -1 && !EventSystem.current.IsPointerOverGameObject(t.fingerId))
                                {
                                    touchPosition = t.position;
                                    aiming_touchid = t.fingerId;
                                    aiming_touchtime = 0;
                                    touching = true;
                                    goto touching;
                                }
                            }
                            break;
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            {
                                if (aiming_touchid == t.fingerId)
                                {
                                    touchPosition = t.position;
                                    aiming_touchtime += t.deltaTime;
                                    touching = true;
                                    if (!powerUp)
                                    {
                                        powerUp = true;
                                        powerUpSound.Play();
                                    }
                                    goto touching;
                                }
                            }
                            break;
                        case TouchPhase.Ended:
                        case TouchPhase.Canceled:
                            {
                                if (aiming_touchid == t.fingerId)
                                {
                                    aiming_touchtime += t.deltaTime;
                                    touchPosition = t.position;
                                    touching = true;
                                    shooting = true;
                                    aiming_touchid = -1;
                                    goto touching;
                                }
                            }
                            break;
                    }
                }
                touching:
                if (touching)
                {
                    Vector2 ViewportPosition = mainCamera.WorldToScreenPoint(gameObject.transform.position);

                    double dx = touchPosition.x - ViewportPosition.x;
                    double dy = touchPosition.y - ViewportPosition.y;

                    float anim_position = (float)((System.Math.Atan2(dx, dy)) / System.Math.PI);

                    SetShootingAngle(-anim_position * 180);

                    //gunMuzzle.transform.rotation = Quaternion.Euler(0, 0, 180.0 + shooting_angle);

                    if (shooting)
                    {
                        powerUpSound.Stop();
                        shootParticle.Play();
                        fsScript.TakingDamage(1);
                        powerUp = false;
                        GameObject newbullet = Instantiate(bullet, muzzle.transform.position, Quaternion.Euler(0, 0, shooting_angle));
                        plateBulletScript newbulletscript = newbullet.GetComponent<plateBulletScript>();

                        //Vector3 kickBack = new Vector3(Mathf.Sin(180 + shooting_angle), Mathf.Cos(180 + shooting_angle), 0);
                        //rb.AddForce(kickBack* 100, ForceMode2D.Impulse);
                        //newbulletscript.impulse_angle = anim_position;
                        if (aiming_touchtime > 3)
                            aiming_touchtime = 3;
                        {
                            if (aiming_touchtime < 1)
                                newbulletscript.damage = gunPower*1.4f;
                            else newbulletscript.damage = gunPower * aiming_touchtime*1.4f;
                        }
                        newbulletscript.massScale *= aiming_touchtime / 2;
                        if (newbulletscript.massScale < 0.5f)
                        {
                            newbulletscript.massScale = 0.5f;
                        }

                    }
                }
            }
        }
        SetShootingAngle(shooting_angle);
        #endregion
    }

    int aiming_touchid = -1;
    float aiming_touchtime = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 11|| collision.gameObject.layer == 19)
        {
            if (canDie)
            { i++;
                if (i == 1)
                {
                    isDead = true;
                    if (mainScript.levelIndex == 1)
                    {
                        deathSound.Play();
                        OpenLosePanel();
                    }
                }
            }
        }
        if (collision.gameObject.layer == 9)
        {
            if (mainScript.levelIndex == 1)
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
    }
    public void FuelConsampsion(float Consumption)
    {
        currentFuel -= Consumption * Time.deltaTime;
        fuelBar.SetValue(currentFuel);
    }
    public void SetFuelValues()
    {
        if (ShipIndex == -1)
        {
            maxFuel = mainScript.E_maxFuel;
            currentFuel = mainScript.E_maxFuel;
            fuelBar.SetMaxTank(maxFuel);
            fuelBar.SetValue(currentFuel);
        }
        if (ShipIndex == 0)
        {
            maxFuel = mainScript.P_maxFuel;
            currentFuel = mainScript.P_maxFuel;
            fuelBar.SetMaxTank(maxFuel);
            fuelBar.SetValue(currentFuel);
        }
        if (ShipIndex == 1)
        {
            maxFuel = mainScript.WS_maxFuel;
            currentFuel = mainScript.WS_maxFuel;
            fuelBar.SetMaxTank(maxFuel);
            fuelBar.SetValue(currentFuel);
        }
        if (ShipIndex == 2)
        {
            maxFuel = mainScript.K_maxFuel;
            currentFuel = mainScript.K_maxFuel;
            fuelBar.SetMaxTank(maxFuel);
            fuelBar.SetValue(currentFuel);
        }
    }

    public void ReloadEPrefs()
    {
        mainScript.LoadEPrefs();
        EnginePower = mainScript.E_enginePower;
        maxFuel = mainScript.E_maxFuel;
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
        
            if (mainScript.levelIndex > 1)
            {
                CampQuestScript campQuset = GameObject.FindGameObjectWithTag("CampQuest").GetComponent<CampQuestScript>();
                campQuset.Pause();
            }
            if (SceneManager.GetActiveScene().buildIndex != 1 && mainScript.levelIndex == 1)
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
    public void OpenRefuelPanel()
    {
        refuelCanvas = GameObject.FindGameObjectWithTag("LoseScreen").GetComponent<LooseScreenScript>();
        refuelCanvas.RefuelCanvasOpen();
    }
    
}
