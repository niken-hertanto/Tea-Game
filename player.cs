using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class player : MonoBehaviour
{
    public int conNumber;
    public bool hasCharacter = false;
    public bool inTeaRoom = false;

    float playerSpeed = 1f;
    Vector3 playerDir;
    Vector3 playerMoveAmount;
    public SpriteRenderer s_renderer;

    public int lives = 3;

    /*Character Data*/
    public string characterName = "Choose Character";
    public string faction = "Default";
    public int HP = 100;
    public List<Sprite> charSprite;
    /****************/

    int facing = 1;
   

    //Bomb stuff
    public bool grabbing = false;
    public bool isFiring = false;
    public bool ballTravelling = false;

    public float bombSpeed = 0.025f;
    Vector3 bombDelta;

    bool firePressed = false;

    private GameObject bomb;
    private Collider bombCollider;

    float t = 0;

    Vector3 origScale;

    float xAxisR = 0;
    float yAxisR = 0;

    public bool canTurnRed = false;
    float turnRedTime = 0;
    /**********/


    Rigidbody rb;

    /**********/   //Animation

    public Animator anim;
    public AnimatorController ac;

    // public List<Animator> charAnimation;
    /**********/



    // Use this for initialization
    void Start()
    {
        s_renderer = gameObject.GetComponent<SpriteRenderer>();
        s_renderer.sprite = charSprite[0];


        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleActions();
        HandleMovement();
        if (bomb != null)
        {
            if (bomb.GetComponent<Bomb>().exploding) resetBomb();
        }
        BombThing();
        anim.runtimeAnimatorController = ac;

        showHearts();
        turnRed();
    }

    void FixedUpdate()
    {
        BombFly();
    }

    void HandleActions()
    {
        //Debug.Log(Input.GetAxis("TriggersR_" + conNumber));
        if (Input.GetButtonDown("RB_" + conNumber) || Input.GetAxis("TriggersR_" + conNumber) > .5)
        {
            Debug.Log("Fire Pressed");
            firePressed = true;
        } else
        {
            firePressed = false;
        }
    }


    void HandleMovement()
    {

        float xAxis = 0;
        float yAxis = 0;

        if (conNumber == 5)
        {
            bool left = Input.GetKey("left") || Input.GetKey("a");
            bool right = Input.GetKey("right") || Input.GetKey("d");
            bool up = Input.GetKey("up") || Input.GetKey("w");
            bool down = Input.GetKey("down") || Input.GetKey("s");

            if (left || (left && right))
            {
                xAxis = -1;
                yAxis = 0;
            } else if (right)
            {
                xAxis = 1;
                yAxis = 0;
            }
            if (up || (up && down))
            {
                yAxis = 1;
                xAxis = 0;
            } else if (down)
            {
                yAxis = -1;
                xAxis = 0;
            }
        } else
        {
            xAxis = Input.GetAxis("L_XAxis_" + conNumber);
            yAxis = Input.GetAxis("L_YAxis_" + conNumber);
        }

        if(xAxis == 0 && yAxis == 0)
        {
            anim.SetInteger("State", 3);
        }

        if (Mathf.Abs(xAxis) > Mathf.Abs(yAxis))
        {
            yAxis = 0;

            if (xAxis < 0)
            {
                xAxis = -1;
                s_renderer.sprite = charSprite[2];
                s_renderer.flipX = false;

                facing = -1;
                anim.SetInteger("State", 2);
            }
            else
            {
                xAxis = 1;
                s_renderer.sprite = charSprite[2];
                s_renderer.flipX = true;

                facing = 1;
                anim.SetInteger("State", 2);
            }
        }
        else if (Mathf.Abs(xAxis) < Mathf.Abs(yAxis))
        {
            xAxis = 0;

            if (yAxis < 0)
            {
                yAxis = -1;
                s_renderer.sprite = charSprite[0];

                facing = -11;
                anim.SetInteger("State", 0);
            }
            else
            {
                yAxis = 1;
                s_renderer.sprite = charSprite[1];

                facing = 11;
                anim.SetInteger("State", 1);
            }
        }


        playerDir = new Vector3(xAxis, yAxis, 0);
        playerMoveAmount = playerDir * playerSpeed * Time.deltaTime;
        transform.position += playerMoveAmount;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Bomb")
        {
            if (collision.gameObject.GetComponent<Bomb>().exploding)
            {
                rb.velocity = Vector3.zero;
                return;
            }
            if (!grabbing && !isFiring)
            {
                Debug.Log("grabbing!!!");
                grabbing = true;
                bomb = collision.gameObject;
                origScale = bomb.transform.localScale;
                collision.gameObject.transform.parent = gameObject.transform;
                collision.transform.position = gameObject.transform.position;

                //if (s_renderer.sprite == sprites[2] && s_renderer.flipX == false)
                //{
                //    collision.transform.position = playerGameObject.transform.position + new Vector3(-1, 0, 0);
                //}
                //else if (s_renderer.sprite == sprites[2] && s_renderer.flipX == true)
                //{
                //    collision.transform.position = playerGameObject.transform.position + new Vector3(1, 0, 0);
                //}
                //else if (s_renderer.sprite == sprites[1])
                //{
                //    collision.transform.position = playerGameObject.transform.position + new Vector3(0, 1, 0);
                //}
                //else if (s_renderer.sprite == sprites[0])
                //{
                //    collision.transform.position = playerGameObject.transform.position + new Vector3(0, -1, 0);
                //}



            }



        }
        rb.velocity = Vector3.zero;
    }

    void resetBomb()
    {
        isFiring = false;
        grabbing = false;
        ballTravelling = false;
        bomb.transform.parent = null;
        bomb = null;
    }

    void BombThing()
    {
        if (grabbing)
        {
            if (!bomb) return;
            if (bomb.GetComponent<Bomb>().exploding) { resetBomb(); return; }
            bomb.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bomb.transform.position = gameObject.transform.position;
            //Debug.Log("grabbed!!!");
            if (firePressed)
            {
                bombDelta = playerMoveAmount;
                firePressed = false;
                isFiring = true;
                grabbing = false;
                ballTravelling = true;

                Debug.Log("Fired some shit!!!");

                xAxisR = Input.GetAxis("R_XAxis_" + conNumber);
                yAxisR = -Input.GetAxis("R_YAxis_" + conNumber);

                bombCollider = bomb.GetComponent<Collider>();

                bomb.GetComponent<Bomb>().fuselit = true;
                bomb.transform.position = gameObject.transform.position;
                bomb.transform.parent = null;

                //bombCollider.isTrigger = true;

                //Destroy(bomb);
            }

        }
    }

    void BombFly()
    {
        if (!bomb) return;
        if (ballTravelling)
        {



            //Debug.Log("x: " + xAxisR);
            //Debug.Log("y: " + yAxisR);
            //float angle;

            //Vector3 currentPosition = bomb.transform.position;
            //float step = 20.0f * Time.deltaTime;
            //
            //if (xAxisR != 0 || yAxisR != 0)
            //{
            //    //angle = Mathf.Atan2(yAxisR, xAxisR);
            //    currentPosition.x += 50.0f * xAxisR;
            //    currentPosition.y += 50.0f * yAxisR;
            //    currentPosition.z = 0;
            //}
            //else
            //{
            //    currentPosition.x += 50.00f;
            //    currentPosition.y += 0f;
            //    currentPosition.z = 0;
            //}




            t += Time.deltaTime;

            if (t < 1) {

                Vector3 newScale = bomb.transform.localScale;
                //Bomb is flying
                //Debug.Log(t);
                //if (xAxisR == 0 && yAxisR == 0)
                //{
                //    if (Mathf.Abs(facing) < 10)
                //    {
                //        bomb.transform.position += new Vector3(facing * bombSpeed, yAxisR * bombSpeed, 0);
                //    } else
                //    {
                //        facing = facing / Mathf.Abs(facing);
                //        Debug.Log(facing);
                //        bomb.transform.position += new Vector3(xAxisR * bombSpeed, facing * bombSpeed, 0);
                //    }
                //}
                //else
                //{
                    bomb.transform.position += new Vector3(xAxisR * bombSpeed + bombDelta.x/2, yAxisR * bombSpeed + bombDelta.y/2, 0);
                //}
                
                if (t < .5)
                {
                    newScale += new Vector3(0.005f, 0.005f, 0.005f);
                }
                else
                {
                    newScale -= new Vector3(0.005f, 0.005f, 0.005f);
                }
                if (newScale.x < origScale.x)
                {
                    newScale = origScale;
                }
                bomb.transform.localScale = newScale;
            }
            else
            {
                isFiring = false;
                if (t > 2.55)
                {
                    //bombCollider.isTrigger = false;
                
                    ballTravelling = false;
                    t = 0;
                    //Destroy(bomb);
                }
            }
        }
    }

    void showHearts()
    {
        if (inTeaRoom) return;
        if (lives == 3)
        {
            transform.Find("hearts_3").gameObject.SetActive(true);
            transform.Find("hearts_2").gameObject.SetActive(false);
            transform.Find("hearts_1").gameObject.SetActive(false);
        }
        else if (lives == 2)
        {
            transform.Find("hearts_3").gameObject.SetActive(false);
            transform.Find("hearts_2").gameObject.SetActive(true);
            transform.Find("hearts_1").gameObject.SetActive(false);
        }
        else if (lives == 1)
        {
            transform.Find("hearts_3").gameObject.SetActive(false);
            transform.Find("hearts_2").gameObject.SetActive(false);
            transform.Find("hearts_1").gameObject.SetActive(true);
        }
    }

    void turnRed()
    {
        if (canTurnRed)
        {
            //Debug.Log("can turn red");

            turnRedTime += Time.deltaTime;
            //Debug.Log("turnRedTime" + turnRedTime);
            if (turnRedTime < 2)
            {
                s_renderer.color = new Color(255, 0 ,0 );
            }
            else
            {
                s_renderer.color = new Color(255, 255, 255);
                canTurnRed = false;
                turnRedTime = 0;
            }
        }

    }



    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero; 
    }
}
