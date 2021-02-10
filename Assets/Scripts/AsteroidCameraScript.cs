using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class AsteroidCameraScript : MonoBehaviour
{
    GameObject player;
    public GameObject mainCameraObject;
    Rigidbody2D playerRb;
    Camera cam;
    MainScript mainScript;

    Vector3 camRotation;
    Quaternion startRotation;

    public Vector3 offset;
    public float cameraSpeed;
    public float cameraAcceleration;
    public float leftEdge;
    public float rightEdge;
    public Transform StartGeneration;
    public Transform StopGeneration;

    private void Awake()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.StartOnPosition();
        mainScript.levelIndex = 2;
    }
    void Start()
    {
        mainScript.CanvasOrNotCanvas();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        cam = Camera.main;

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
    }

    private void Update()
    {
        if (player.transform.position.y > StopGeneration.transform.position.y)
        {
            playerRb.gravityScale = -1;
            startRotation = mainCameraObject.transform.rotation;
            camRotation = new Vector3(mainCameraObject.transform.rotation.x, mainCameraObject.transform.rotation.y, mainCameraObject.transform.rotation.z+180.0f);
            mainCameraObject.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(camRotation),0.01f);
        }
        //0, 0, Mathf.Lerp(transform.rotation.z, 180.0f, 1.0f)
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
