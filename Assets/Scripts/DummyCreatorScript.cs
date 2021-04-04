using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCreatorScript : MonoBehaviour
{
    public GameObject cowDummy;
    MainScript mainScript;
    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        mainScript.levelIndex = 0;
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
