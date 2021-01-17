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
    playerMoving player_moving;
    
    private void Start()
    {
        player_moving = GetComponentInParent<playerMoving>();
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

        if (collision.gameObject.layer == 17)
        {
            Debug.Log("FuelPoint");
            int onRayCount;
            GameObject obj = collision.gameObject;
            onRayCount = obj.GetComponent<OnRay>().count;
            mainScript.P_fuelLevel += ((float) onRayCount);
            player_moving.SetFuelValues();

            Instantiate(DestroyParticle, transform.position, Quaternion.identity);
            Destroy(obj);
        }
    }
}
