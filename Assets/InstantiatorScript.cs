using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorScript : MonoBehaviour
{
    public GameObject[] rightObjects;
    public GameObject[] leftObjects;
    Vector3 offsetRight;
    Vector3 offsetLeft;
    private void Start()
    {
        offsetRight = new Vector3(transform.position.x + 45, -9, transform.position.z);
        offsetLeft = new Vector3(transform.position.x - 45, -9, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            if (rightObjects != null && rightObjects.Length != 0) 
            {
                for (int i = 0; i < Random.Range(0, rightObjects.Length); i++)
                {
                    offsetRight.x += Random.Range(-5,5);
                    Instantiate(rightObjects[Random.Range(0, rightObjects.Length)], offsetRight, Quaternion.identity);
                } 
            }
            if (leftObjects != null && leftObjects.Length != 0)
            {
                for (int i = 0; i < Random.Range(0, leftObjects.Length); i++)
                {
                    offsetLeft.x += Random.Range(-5, 5);
                    Instantiate(leftObjects[Random.Range(0, leftObjects.Length)], offsetLeft, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }

    }
}
