using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreatorScript : MonoBehaviour
{
    GameObject player;
    public Vector3 offset;
    public float distanseX;
    public float AsteroidMakingSpeed = 0.2f;
    public float bonusCreationSpeed = 0.5f;
    public Transform StartGeneration;
    public Transform StopGeneration;

    public GameObject[] asteroids = new GameObject[] { };
    public GameObject[] bonuses = new GameObject[] { };
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("MakingAsteroids");
        StartCoroutine("MakingBonuses");
    }

    void Update()
    {
        if ((transform.position.y - player.transform.position.y)<distanseX) 
        {
            transform.position = player.transform.position + offset; 
        }
    }

    IEnumerator MakingAsteroids()
    {
        while (true)
        {
            if (player.transform.position.y > StartGeneration.position.y && player.transform.position.y < StopGeneration.position.y)
            {
                int i = Random.Range(0, 3);
                if (transform.rotation.z == 0)
                {
                    Instantiate(asteroids[i], new Vector3(Random.Range(-54, 54), transform.position.y, transform.position.z), gameObject.transform.rotation);
                }
                else
                {
                    Instantiate(asteroids[i], new Vector3(transform.position.x, transform.position.y + Random.Range(-30, 30), transform.position.z), gameObject.transform.rotation);
                }
                yield return new WaitForSeconds(AsteroidMakingSpeed);
            }
            else
            {
                yield return new WaitForSeconds(AsteroidMakingSpeed);
            }
        }
    }

    IEnumerator MakingBonuses()
    {
        while (true)
        {
            if (player.transform.position.y > StartGeneration.position.y && player.transform.position.y < StopGeneration.position.y)
            {
                int i = Random.Range(0, bonuses.Length);
                Instantiate(bonuses[i], new Vector3(Random.Range(-54, 54), transform.position.y, transform.position.z), Quaternion.identity);
                yield return new WaitForSeconds(bonusCreationSpeed);
            }
            else
            { yield return new WaitForSeconds(bonusCreationSpeed); }
        }
    }

}
