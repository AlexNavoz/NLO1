using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class plateBulletScript : MonoBehaviour
{
    MainScript mainScript;
    playerMoving player_moving;
    GameObject player;
    Rigidbody2D rb;
    public float shootPower = 10;
    public float damage;
    public float massScale = 1;
    Vector2 startScale;
    Vector2 impulse_vector = new Vector2(0.0f, 1.0f);
    public GameObject HitParticle;

    public float impulse_angle;
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        player_moving = player.GetComponent<playerMoving>();
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(impulse_vector * shootPower * MainScript.forceBatchingMultiplier, ForceMode2D.Impulse);
        transform.localScale = new Vector2(transform.localScale.x * massScale , transform.localScale.y * massScale);
        Destroy(gameObject , 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 19)
        {
            Rigidbody2D cowRb;
            int onRayMilkCount;
            int onRayBrainsCount;
            OnRay onRayComponent;

            GameObject obj = collision.gameObject;

            onRayComponent = obj.GetComponent<OnRay>();

            if (onRayComponent == null)
            {
                return;
            }
            else
            {
                cowRb = obj.GetComponent<Rigidbody2D>();

                onRayComponent.massScale -= damage/cowRb.mass;
                onRayComponent.UpdateScale();

                
                onRayMilkCount = onRayComponent.milkCount;
                onRayBrainsCount = onRayComponent.brainCount;

                if (onRayComponent.massScale > 0.11f) {
                    return;
                }
                //if (cowRb.mass > damage)
                //{
                //    return;
                //}

                if (onRayComponent.questObjectIndex == mainScript.campaignQisetObjIndex)
                {
                    mainScript.campaignQuestObjCount++;
                }
                if (onRayMilkCount != 0)
                {
                    mainScript.milkCollection += onRayMilkCount;
                    {
                        StringBuilder sb = new StringBuilder("+", 10);
                        sb.Append(((int)onRayMilkCount).ToString());
                        player_moving.showTextValue(player, sb.ToString(), 1);
                    }
                }
                if (onRayBrainsCount != 0)
                {
                    mainScript.brainsCollection += onRayBrainsCount;
                    {
                        StringBuilder sb = new StringBuilder("+", 10);
                        sb.Append(((int)onRayBrainsCount).ToString());
                        player_moving.showTextValue(player, sb.ToString(), 2);
                    }
                }
                
                Destroy(obj);
            }
        }
        Instantiate(HitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
