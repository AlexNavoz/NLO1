using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKicker : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-4, 4), Random.Range(-4, 4)), ForceMode2D.Impulse);
    }
}
