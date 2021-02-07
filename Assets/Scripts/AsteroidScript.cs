using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    Rigidbody2D rb;
    ForceShieldScript fs = null;
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.down * Random.Range(200.0f, 400.0f), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-100, 100),ForceMode2D.Impulse);
        GameObject fs_obj;
        if ((fs_obj = GameObject.FindGameObjectWithTag("ForceShield")) != null)
            fs = fs_obj.GetComponent<ForceShieldScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (fs == null)
            return;
        if (collision.gameObject.layer == 13)
        {
            fs.TakingDamage(rb.mass / 2);
        }
    }

    private void Update()
    {
        if (transform.position.y - player.position.y < -50)
        {
            Destroy(gameObject);
        }
    }

}
