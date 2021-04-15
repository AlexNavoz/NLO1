using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BoxScript : MonoBehaviour
{
    public int boxCount;
    public int boxHP;
    public GameObject boxParticle;
    public bool boxIsLost = false;
    Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if(!boxIsLost && (playerTransform.position.y-transform.position.y)>Mathf.Abs(20))
        {
            boxIsLost = true;
        }
    }
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
