using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    Rigidbody2D playerRb;
    public ParticleSystem DestroyParticle;
    public int collection;
    MainScript mainScript;
    ParticleSystem givingParticle;
    
    private void Start()
    {
        givingParticle = GetComponentInChildren<ParticleSystem>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        playerRb = GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Rigidbody2D cowRb;
            int onRayCount;
            

            GameObject obj = collision.gameObject;

            onRayCount = obj.GetComponent<OnRay>().count;
            mainScript.collection += onRayCount;
            cowRb = obj.GetComponent<Rigidbody2D>();
            if (cowRb != null){
                playerRb.mass += cowRb.mass;
            }
            Instantiate(DestroyParticle, transform.position, Quaternion.identity);

            Destroy(obj);
        }

        if(collision.gameObject.layer == 14&& mainScript.collection!=0)
        {
            mainScript.SetMoney(mainScript.collection);
            givingParticle.Play();
            mainScript.collection = 0;
        }
    }
}
