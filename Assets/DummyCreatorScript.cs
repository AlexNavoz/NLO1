using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCreatorScript : MonoBehaviour
{
    public GameObject cowDummy;
    void Start()
    {
        StartCoroutine("DummyCorutine");
    }

    IEnumerator DummyCorutine()
    {
        while (true)
        {
            Instantiate(cowDummy, new Vector3(transform.position.x, Random.Range(-10, 10), transform.position.z),Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
