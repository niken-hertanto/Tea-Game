using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour {
    public Rigidbody potExplosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            Rigidbody pieces;
            Destroy(gameObject,.1f);
            pieces = Instantiate(potExplosion, transform.position, transform.rotation) as Rigidbody;
            pieces.velocity = transform.TransformDirection(Vector3.forward * 10);
        }
    }
}
