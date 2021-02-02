using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class AsteroidCameraScript : MonoBehaviour
{
    public GameObject[] asteroids = new GameObject[] { };
    public GameObject[] bonuses = new GameObject[] { };
    GameObject player;
    Camera cam;
    MainScript mainScript;
    public float AsteroidMakingSpeed = 0.2f;
    public float bonusCreationSpeed = 0.5f;

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
        StartCoroutine("MakingAsteroids");
        StartCoroutine("MakingBonuses");
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
    }

    

    IEnumerator MakingAsteroids()
    {
        while (true)
        {
            int i = Random.Range(0, 3);
            Instantiate(asteroids[i], new Vector3(Random.Range(-54, 54), transform.position.y + 50, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(AsteroidMakingSpeed);
        }
    }

    IEnumerator MakingBonuses()
    {
        while (true)
        {
            int i = Random.Range(0, bonuses.Length);
            Instantiate(bonuses[i], new Vector3(Random.Range(-54, 54), transform.position.y + 50, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(bonusCreationSpeed);
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}
