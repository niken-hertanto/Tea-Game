using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSRotate : MonoBehaviour {

    public GameObject RSCircle;

    float xAxisR = 0;
    float yAxisR = 0;
    player mother;
    SpriteRenderer circle_renderer;

    // Use this for initialization
    void Start () {
        mother = gameObject.GetComponentInParent<player>();
        circle_renderer = RSCircle.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        xAxisR = Input.GetAxis("R_XAxis_" + mother.conNumber);
        yAxisR = Input.GetAxis("R_YAxis_" + mother.conNumber);


        if (xAxisR==0 && yAxisR == 0)
        {
            circle_renderer.enabled = false;

        }
        else
        {
            circle_renderer.enabled = true;
            RSCircle.transform.eulerAngles = new Vector3(0 ,  0,  Mathf.Atan2(-xAxisR, -yAxisR) * Mathf.Rad2Deg);

        }
    }
}
