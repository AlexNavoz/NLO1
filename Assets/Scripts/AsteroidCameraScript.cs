using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class AsteroidCameraScript : MonoBehaviour
{
    GameObject player;
    Camera cam;
    MainScript mainScript;

    public Vector3 offset;
    public float cameraSpeed;
    public float cameraAcceleration;
    public float leftEdge;
    public float rightEdge;

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
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
