using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxStrength;
    public GameObject cam;

    float lengthX, startposX, startposY;

    private void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (transform.position.x * (1 - parallaxStrength));
        float distX = (cam.transform.position.x * parallaxStrength);
        float distY = (cam.transform.position.y * parallaxStrength);

        transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

        if(temp>startposX + lengthX)
        {
            startposX+=lengthX;
        }
        else if(temp<startposX - lengthX)
        {
            startposX -= lengthX;
        }
    }
}
