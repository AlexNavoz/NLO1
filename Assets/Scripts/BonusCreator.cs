using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCreator : MonoBehaviour
{
    public GameObject bonus;
    public int min;
    public int max;
    int count;
    Rigidbody2D rb;

    private void Start()
    {
        count = Random.Range(min, max);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.down * Random.Range(150.0f, 250.0f), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-100, 100), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 13 || collision.gameObject.layer == 10)
        {
            for(int i = 0; i<count; i++)
            {
                Instantiate(bonus,transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
