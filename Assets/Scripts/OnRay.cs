using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRay : MonoBehaviour
{
    public bool isInRay = false;
    Rigidbody2D rb;
    public int count;
    Transform player;

    float massOnStart;
    Vector3 scaleOnStart;

    public double massScale = 1.0f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        massOnStart = rb.mass;
        scaleOnStart = transform.localScale;

        InvokeRepeating("DistanceDetecting", 0, 1.0f);
    }

    private void FixedUpdate()
    {
        if (isInRay)
        {
            massScale -= (Time.deltaTime * 0.25f / massOnStart);// * MainScript.forceBatchingMultiplier;
            if (massScale < 0.1f)
                massScale = 0.1f;

            rb.mass = massOnStart * (float)massScale;

            double transformscale = System.Math.Pow(massScale, 0.7f);
            transform.localScale = new Vector2(scaleOnStart.x * (float)transformscale, scaleOnStart.y * (float)transformscale);
        }
        
    }

    void DistanceDetecting()
    {
        if (gameObject.tag != "Bad")
        {
            if ((player.position.x - transform.position.x) > 40 || (player.position.x - transform.position.x) < -40)
            {
                gameObject.SetActive(false);
            }
            else gameObject.SetActive(true);
        }
    }

}
