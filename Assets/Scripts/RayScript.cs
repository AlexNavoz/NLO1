using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    public float rayPower;
    PointEffector2D rayLift;
    MainScript mainScript;
    private void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        rayLift = GetComponent<PointEffector2D>();
        //change after Garage menu
        if (mainScript.ShipIndex == 0)
        {
            rayLift.forceMagnitude = -mainScript.P_rayLiftPower;
        }
        if (mainScript.ShipIndex == 1)
        {
            rayLift.forceMagnitude = -mainScript.WS_rayLiftPower;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8|| collision.gameObject.layer == 19)
        {
            GameObject obj = collision.gameObject;
            InDecreaser(obj);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 19)
        {
            GameObject obj = collision.gameObject;
            OutDecreaser(obj);
        }
    }
    void InDecreaser(GameObject obj)
    {
        OnRay onRay;
        onRay = obj.GetComponent<OnRay>();
        if (onRay != null)
            onRay.isInRay = true;
    }
    void OutDecreaser(GameObject obj)
    {
        OnRay onRay;
        onRay = obj.GetComponent<OnRay>();
        if(onRay != null)
            onRay.isInRay = false;
    }
}
