using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    GameObject player;
    Camera cam;
    MainScript mainScript;

    public float smoothSpeed = 1.0f;
    public Vector3 offset;
    public float maxSize;
    public float minSize;
    public float maxHeight;
    public float minHeight;
    public float leftEdge;

    private void Awake()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.StartOnPosition();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
    }
    void LateUpdate()
    {
        Vector3 playerPosition = player.transform.position + offset;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, playerPosition, smoothSpeed);
        transform.position = SmoothedPosition;
        cam.orthographicSize = player.transform.position.y*1.5f;
        if (cam.orthographicSize > maxSize)
        {
            cam.orthographicSize = maxSize;
        }
        if (cam.orthographicSize < minSize)
        {
            cam.orthographicSize = minSize;
        }
        if (transform.position.y < minHeight)
        {
            Vector3 MinHeight = new Vector3(transform.position.x, minHeight, transform.position.z);
            transform.position = MinHeight;
        }
        if (transform.position.y > maxHeight)
        {
            Vector3 MaxHeight = new Vector3(transform.position.x, maxHeight, transform.position.z);
            transform.position = MaxHeight;
        }
        if (transform.position.x < leftEdge)
        {
            Vector3 LeftEdge = new Vector3(leftEdge, transform.position.y, transform.position.z);
            transform.position = LeftEdge;
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
