using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    GameObject player;
    playerMoving playerMoving;
    Camera cam;
    MainScript mainScript;
    public int levelIndex = 0;
    public GameObject[] badGuys;

    float smoothSpeed = 3.0f;
    public Vector3 offset;
    public float maxSize;
    public float minSize;
    float maxHeight = 11f;
    public float minHeight;
    public Transform leftBlock;
    public Transform rightBlock;

    private void Awake()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.StartOnPosition();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            mainScript.levelIndex = 0;
        }
        else mainScript.levelIndex = levelIndex;

    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoving = player.GetComponent<playerMoving>();
        cam = Camera.main;
        StartCoroutine("SpawnEnemies");
        if(levelIndex == 1)
        {
            mainScript.questButton.SetActive(true);
        }
        else
        {
            mainScript.questButton.SetActive(false);
        }
        //if (levelIndex != 5)
        {
            Time.fixedDeltaTime = 0.02f;
            MainScript.UpdateStupidTimeMultiplyingConstant();
        }

    }
    void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position + offset;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, playerPosition, smoothSpeed*Time.fixedDeltaTime);
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
        if (transform.position.x < leftBlock.position.x)
        {
            Vector3 LeftEdge = new Vector3(leftBlock.position.x, transform.position.y, transform.position.z);
            transform.position = LeftEdge;
        }
        if (transform.position.x > rightBlock.position.x)
        {
            Vector3 RightEdge = new Vector3(rightBlock.position.x, transform.position.y, transform.position.z);
            transform.position = RightEdge;
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 badGuysPos = new Vector3(player.transform.position.x + 50.0f, 0, 0);
            Vector3 badGuysPos2 = new Vector3(player.transform.position.x - 50.0f, 0, 0);
            float leftOrRight = Random.Range(-1.0f, 1.0f);
            if (playerMoving.crimeIndex == 0)
            {
                yield return new WaitForSeconds(10.0f);
            }
            else
            {
                if (leftOrRight > 0)
                {
                    Instantiate(badGuys[playerMoving.crimeIndex - 1], badGuysPos, new Quaternion(0, 0, 0, 0));
                }
                else
                {
                    Instantiate(badGuys[playerMoving.crimeIndex - 1], badGuysPos2, new Quaternion(0, 0, 0, 0));
                }
            }
            yield return new WaitForSeconds(10.0f);
        }

    }
}
