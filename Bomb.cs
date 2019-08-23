using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float speed = 0.025f;
    public float fuse = 3;
    public bool fuselit = false;
    public bool exploding = false;
    bool damagedPlayer = false;

    public AudioSource bombExplosion;
    public AudioSource bombFuse;
    public AudioSource ouch;
    bool playSound = false;

    SpriteRenderer sp;
    public List<Sprite> explosion;

    public GenerateBomb bombGenerator;

    Rigidbody rb;
    Collider col;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        col = gameObject.GetComponent<Collider>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        
        bombGenerator = GameObject.FindGameObjectWithTag("BombGenerator").GetComponent<GenerateBomb>();
    }
	
	// Update is called once per frame
	void Update () {
		if (fuselit)
        {
            if (fuse > 0)
            {
                if (playSound == false)
                {
                    bombFuse.Play();
                    playSound = true;
                }
                fuse -= Time.deltaTime;
            } else
            {
                bombFuse.Stop();
                bombExplosion.Play();
                sp.sprite = explosion[0];
                exploding = true;
                fuselit = false;
                fuse = 1;
                col.isTrigger = true;
                playSound = false;
            }
        }
        if (exploding)
        {
            //Debug.Log("Exploded");
            //Debug.Log(fuse);
            if (fuse < 0)
            {
                bombGenerator.currentNumber--;
                Destroy(gameObject);
            }
            fuse -= Time.deltaTime;
        }
	}

    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {
        if (exploding && other.tag == "Player" && !damagedPlayer)
        {
            other.gameObject.GetComponent<player>().lives--;
            other.GetComponent<player>().canTurnRed = true;
            //StartCoroutine(other.GetComponent<player>().turnWhite());
            ouch.Play();
            damagedPlayer = true;
        }
    }

}
