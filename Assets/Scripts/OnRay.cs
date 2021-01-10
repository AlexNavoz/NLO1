﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRay : MonoBehaviour
{
    public bool isInRay = false;
    Rigidbody2D rb;
    public int count;

    float massOnStart;
    Vector3 scaleOnStart;

    double massScale = 1.0f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        massOnStart = rb.mass;
        scaleOnStart = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (isInRay)
        {
            massScale -= (0.005f / massOnStart);
            if (massScale < 0.1f)
                massScale = 0.1f;

            rb.mass = massOnStart * (float)massScale;

            double transformscale = System.Math.Pow(massScale, 0.7f);
            transform.localScale = new Vector2(scaleOnStart.x * (float)transformscale, scaleOnStart.y * (float)transformscale);
        }
        
    }

}