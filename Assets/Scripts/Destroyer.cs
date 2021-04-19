using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    Rigidbody2D playerRb;
    public ParticleSystem DestroyParticle;
    public int collection;
    MainScript mainScript;
    ParticleSystem givingParticle;
    playerMoving player_moving;
    ForceShieldScript fs;
    public int stolenCows;
    
    private void Start()
    {
        fs = GameObject.FindGameObjectWithTag("ForceShield").GetComponent<ForceShieldScript>();
        player_moving = GetComponentInParent<playerMoving>();
        givingParticle = GetComponentInChildren<ParticleSystem>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        playerRb = GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8|| collision.gameObject.layer == 20)
        {
            Rigidbody2D cowRb;
            int onRayCount;
            OnRay onRayComponent;

            GameObject obj = collision.gameObject;

            onRayComponent = obj.GetComponent<OnRay>();
           
            if (onRayComponent == null)
            {
                return;
            }
            else
            {
                if(onRayComponent.questObjectIndex == mainScript.questObjectIndex)
                {
                    mainScript.questObjectCount++;
                    mainScript.SaveQuestPrefs();
                }
                if (onRayComponent.questObjectIndex == mainScript.campaignQisetObjIndex)
                {
                    mainScript.campaignQuestObjCount++;
                }
                stolenCows++;
                onRayCount = onRayComponent.count;
                cowRb = obj.GetComponent<Rigidbody2D>();
                if (cowRb.mass >= 2.0f)
                {
                    return;
                }
                mainScript.collection += onRayCount;
                {
                    StringBuilder sb = new StringBuilder("+", 10);
                    sb.Append(((int)onRayCount).ToString());
                    player_moving.showTextValue(gameObject, sb.ToString(), 1);

                    //player_moving.showQuestText(mainScript.questButton, sb.ToString(), 1);
                }
               
                if (cowRb != null)
                {
                    playerRb.mass += cowRb.mass;
                }
                Instantiate(DestroyParticle, transform.position, Quaternion.identity);

                Destroy(obj);
            }
        }

        if(collision.gameObject.layer == 14 && mainScript.collection!=0)
        {
            mainScript.SetMoney(mainScript.collection);
            givingParticle.Play();
            mainScript.collection = 0;
        }

        if (collision.gameObject.layer == 17)
        {
            int onRayCount;
            GameObject obj = collision.gameObject;
            onRayCount = obj.GetComponent<OnRay>().count;
            player_moving.currentFuel += onRayCount;
            player_moving.fuelBar.SetValue(player_moving.currentFuel);

            Instantiate(DestroyParticle, transform.position, Quaternion.identity);
            Destroy(obj);
        }
        if (collision.gameObject.layer == 18)
        {
            int onRayCount;
            GameObject obj = collision.gameObject;
            onRayCount = obj.GetComponent<OnRay>().count;
            fs.currentHP +=onRayCount;
            fs.SetHPValue();

            Instantiate(DestroyParticle, transform.position, Quaternion.identity);
            Destroy(obj);
        }
        /*if (collision.gameObject.layer == 20)
        {
            int onRayCount;
            GameObject obj = collision.gameObject;
            onRayCount = obj.GetComponent<OnRay>().count;
            mainScript.collection += onRayCount;

            Instantiate(DestroyParticle, transform.position, Quaternion.identity);
            Destroy(obj);
        }*/
    }
}
