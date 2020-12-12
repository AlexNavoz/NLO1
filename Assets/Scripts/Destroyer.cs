using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    Rigidbody2D playerRb;
    public ParticleSystem DestroyParticle;
    private void Start()
    {
        playerRb = GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            float cowMass;
            Rigidbody2D cowRb;


            GameObject obj = collision.gameObject;

            cowRb = obj.GetComponent<Rigidbody2D>();
            if (cowRb != null){
                playerRb.mass += cowRb.mass;
            }
            Instantiate(DestroyParticle, transform.position, Quaternion.identity);

            Destroy(obj);
        }
    }
}
