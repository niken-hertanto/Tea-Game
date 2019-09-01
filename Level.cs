using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public Transform playerPrefab;

    //Use this for initialization

    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerPrefab.transform.position = new Vector3(0, 0, 0);
        }
    }
}
