using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sit : MonoBehaviour
{

    int conNumber;
    public Vector3 target;
    public GameObject dialogue;
    public int spriteNumber;

    float speed = 1f;

    public bool isOccupied = false;
    bool canShowText = false;


    float alpha = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isOccupied)
        {
            canShowText = true;
        }

        if(canShowText)
        {
            //Debug.Log(canShowText);
            alpha += Time.deltaTime;
            //Debug.Log("alpha: " + alpha);
            if (alpha > 1)
                alpha = 1;
            dialogue.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);


        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("stage1");
        
            conNumber = other.GetComponent<player>().conNumber;
            if (Input.GetButtonDown("A_" + conNumber))
            {
                Debug.Log("become trigger!!");
                other.GetComponent<Collider>().isTrigger = true;
                isOccupied = true;

 
              

                SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
                sr.sortingOrder = -1;
                //Debug.Log(other.transform.localScale.x);
                if(sr.flipX)
                {
                    sr.flipX = false;
                }
                sr.gameObject.GetComponent<Animator>().SetInteger("State", spriteNumber);
               
                //sr.sprite = other.GetComponent<player>().charSprite[spriteNumber];


                dialogue.SetActive(true);

                other.GetComponent<player>().enabled = false;
                
            }
        }
        if(isOccupied)
        {
            DragPlayer(other.gameObject);
            if(other.gameObject.transform.position == target)
            {
                other.gameObject.GetComponent<Animator>().SetInteger("State", 3);
                gameObject.GetComponent<Collider>().enabled = false;
                //Destroy(gameObject);

                
            }
            
        }
 
    }



    void DragPlayer(GameObject player)
    {
       // while (player.transform.position != target)
       // {
    
            float step = speed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, step);
        // }
       
    }

 
}