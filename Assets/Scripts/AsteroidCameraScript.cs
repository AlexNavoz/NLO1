using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class AsteroidCameraScript : MonoBehaviour
{
    GameObject player;
    GameObject finish;
    public GameObject mainCameraObject;
    Rigidbody2D playerRb;
    Camera cam;
    MainScript mainScript;
    playerMoving PlayerMoving;

    Vector3 camRotation;
    Quaternion startRotation;
    public int boxIndex;

    public Vector3 offset;
    public float cameraSpeed;
    public float cameraAcceleration;
    public float leftEdge;
    public float rightEdge;
    public Transform StartGeneration;
    public Transform StopGeneration;

    public float salaryRange = 100;
    public int salaryEveryRange = 100;

    float lastSalaryYPosition = 0;

    private void Awake()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.StartOnPosition();
        mainScript.levelIndex = 2;
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerMoving = player.GetComponent<playerMoving>();
        PlayerMoving.boxes[boxIndex].SetActive(true);
    }
    void Start()
    {
        Time.fixedDeltaTime = 0.007f;
        MainScript.UpdateStupidTimeMultiplyingConstant();

        playerRb = player.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        finish = GameObject.FindGameObjectWithTag("Finish");

        lastSalaryYPosition = 0;
    }
    void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position + offset;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, playerPosition, 2.5f*Time.fixedDeltaTime);
        transform.position = SmoothedPosition;

        transform.position = new Vector3( player.transform.position.x, transform.position.y, transform.position.z);
        if (transform.position.x < leftEdge)
        {
            Vector3 LeftEdge = new Vector3(leftEdge, transform.position.y, transform.position.z);
            transform.position = LeftEdge;
        }
        if (transform.position.x > rightEdge)
        {
            Vector3 RightEdge = new Vector3(rightEdge, transform.position.y, transform.position.z);
            transform.position = RightEdge;
        }
        if (transform.position.y < 0)
        {
            Vector3 botEdge = new Vector3(transform.position.x,0,transform.position.z);
            transform.position = botEdge;
        }
        if (transform.position.y > finish.transform.position.y)
        {
            Vector3 finishEdge = new Vector3(transform.position.x, finish.transform.position.y, transform.position.z);
            transform.position = finishEdge;
        }
    }

    private void Update()
    {
        if (player.transform.position.y > StopGeneration.transform.position.y)
        {
            lastSalaryYPosition = 99999999999999999; // a lot!!!
            playerRb.gravityScale = -1;
            startRotation = mainCameraObject.transform.rotation;
            camRotation = new Vector3(mainCameraObject.transform.rotation.x, mainCameraObject.transform.rotation.y, mainCameraObject.transform.rotation.z + 180.0f);
            mainCameraObject.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(camRotation), 0.01f);
        }
        else
        {
            if (player.transform.position.y > (lastSalaryYPosition + salaryRange)) {
                lastSalaryYPosition += salaryRange;
                mainScript.collection += salaryEveryRange;
                {
                    StringBuilder sb = new StringBuilder("+", 10);
                    sb.Append((salaryEveryRange).ToString());
                    PlayerMoving.showTextValue(gameObject, sb.ToString(), 1);
                }
            }
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
