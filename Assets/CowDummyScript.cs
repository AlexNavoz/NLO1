using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowDummyScript : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector3.left * Random.Range(5.0f, 10.0f), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-2, 2), ForceMode2D.Impulse);
    }
}
