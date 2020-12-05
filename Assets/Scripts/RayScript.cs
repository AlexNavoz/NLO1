using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public float rayPower;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            GameObject obj = collision.gameObject;
            InDecreaser(obj);
            Debug.Log("shit");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            GameObject obj = collision.gameObject;
            OutDecreaser(obj);
        }
    }
    void InDecreaser(GameObject obj)
    {
        OnRay onRay;
        onRay = obj.GetComponent<OnRay>();
        onRay.isInRay = true;
    }
    void OutDecreaser(GameObject obj)
    {
        OnRay onRay;
        onRay = obj.GetComponent<OnRay>();
        onRay.isInRay = false;
    }
}
