using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public int boxCount;
    public int boxHP;
    public GameObject boxParticle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 19)
        {
            boxHP -= 1;
            Instantiate(boxParticle, transform.position, Quaternion.identity);
            if (boxHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
