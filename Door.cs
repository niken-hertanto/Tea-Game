using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject doorOpen;
    public GameObject doorClose;
    

    public GameObject camera1;
    public GameObject camera2;

    int conNumber;

    public GameObject charRemain1;
    public GameObject charRemain2;

    public GameObject sign;

    public AudioSource teaRoomAmbient;
    public AudioSource fightingLevelBg;
    public AudioSource bombFuse;
    public AudioSource bombExplosion;

    CharacterSelector cs;

	// Use this for initialization
	void Start () {
        doorOpen.SetActive(false);
        doorClose.SetActive(true);
        cs = GameObject.FindGameObjectWithTag("UI").GetComponent<CharacterSelector>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag =="Player")
        {

            doorOpen.SetActive(true);
            doorClose.SetActive(false);

            sign.SetActive(true);
        }
    }


    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //playerInDoor = col.GetComponent<Character>();
            //Debug.Log(playerInDoor.charSprite);
            conNumber = col.GetComponent<player>().conNumber;
            doorOpen.SetActive(true);
            doorClose.SetActive(false);

            sign.SetActive(true);
            if (Input.GetButtonDown("A_" + conNumber))
            {
                //col.transform.position = new Vector3(-0.414f - 0.01594381f, -0.581f + 3.61f, -0.03f);
                //Debug.Log("player: " + cs.players[0].characterName);
                //Debug.Log("character: " + cs.ninja[1].characterName);
                //Debug.Log(cs.players[0].transform.childCount);
                for (int i = cs.players[0].transform.childCount; i > 0 ; i--)
                {
                    cs.players[0].inTeaRoom = true;
                    Destroy(cs.players[0].transform.GetChild(i - 1).gameObject);
                    //Debug.Log("killed one child!");     
                }

                for (int i = cs.players[1].transform.childCount; i > 0; i--)
                {
                    cs.players[1].inTeaRoom = true;
                    Destroy(cs.players[1].transform.GetChild(i - 1).gameObject);
                }
                //while (cs.players[0].transform.GetChild(0) != null)
                //{
                //    Destroy(cs.players[0].transform.GetChild(0).gameObject);
                //}
                //while (cs.players[1].transform.GetChild(0) != null)
                //{
                //    Destroy(cs.players[1].transform.GetChild(0).gameObject);
                //}
                //cs.players[1].transform.DetachChildren();
                cs.players[0].transform.position = new Vector3(-0.414f - 0.01594381f, -0.581f + 3.61f, -0.03f);
                cs.players[1].transform.position = new Vector3(0.414f - 0.01594381f, -0.581f + 3.61f, -0.03f);

                //Audio
                bombFuse.Stop();
                bombExplosion.Stop();
                fightingLevelBg.Stop();
                teaRoomAmbient.Play();

                if (cs.ninja[0].characterName != cs.players[0].characterName && cs.ninja[0].characterName != cs.players[1].characterName)
                {
                    charRemain1.GetComponent<SpriteRenderer>().sprite = cs.ninja[0].charSprite[2];
                    if (cs.ninja[1].characterName != cs.players[0].characterName && cs.ninja[1].characterName != cs.players[1].characterName)
                    {
                        charRemain2.GetComponent<SpriteRenderer>().sprite = cs.ninja[1].charSprite[0];
                    }
                    else if (cs.samurai[0].characterName != cs.players[0].characterName && cs.samurai[0].characterName != cs.players[1].characterName)
                    {
                        charRemain2.GetComponent<SpriteRenderer>().sprite = cs.samurai[0].charSprite[0];
                    }
                    else
                    {
                        charRemain2.GetComponent<SpriteRenderer>().sprite = cs.samurai[1].charSprite[0];
                    }
                }
                else if(cs.ninja[1].characterName != cs.players[0].characterName && cs.ninja[1].characterName != cs.players[1].characterName)
                {
                    charRemain1.GetComponent<SpriteRenderer>().sprite = cs.ninja[1].charSprite[2];
                    if (cs.samurai[0].characterName != cs.players[0].characterName && cs.samurai[0].characterName != cs.players[1].characterName)
                    {
                        charRemain2.GetComponent<SpriteRenderer>().sprite = cs.samurai[0].charSprite[0];
                    }
                    else
                    {
                        charRemain2.GetComponent<SpriteRenderer>().sprite = cs.samurai[1].charSprite[0];
                    }
                }
                else
                {
                    charRemain1.GetComponent<SpriteRenderer>().sprite = cs.samurai[0].charSprite[2];
                    charRemain2.GetComponent<SpriteRenderer>().sprite = cs.samurai[1].charSprite[0];
                }

                    camera1.SetActive(false);
                camera2.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            doorOpen.SetActive(false);
            doorClose.SetActive(true);

            sign.SetActive(false);
        }
    }
}


