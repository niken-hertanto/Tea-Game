using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBomb : MonoBehaviour {

    public GameObject bomb;

    public int maxNumber;
    public int currentNumber;

    bool[] exploding;
    GameObject[] newBombs;
    

	// Use this for initialization
	void Start () {
        currentNumber = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (currentNumber < maxNumber)
        {

            float x = Random.Range(-1.5f, 1.5f);
            float y = Random.Range(-1.3f, 0.65f);
            Vector3 pos = new Vector3(x, y, 0);
            Instantiate(bomb, pos, bomb.transform.rotation);
            //newBombs[currentNumber] = (GameObject)Instantiate(bomb, pos, bomb.transform.rotation);
           // exploding[currentNumber] = newBombs[currentNumber].GetComponent<Bomb>().exploding;

            currentNumber++;
         

        }

        //for (int i = 0; i < maxNumber; i++)
        //{
        //    if (exploding[i] == true)
        //    {
        //        Debug.Log("exploding!!!");
        //        currentNumber--;
        //    }
        //}
    }
}
