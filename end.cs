using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour {

    public GameObject seatTrigger1;
    public GameObject seatTrigger2;

    public GameObject teaCup1;
    public GameObject teaCup2;
    public GameObject button1;
    public GameObject button2;
    public GameObject EndText;

    float alpha;

    bool playSound = false;
    public AudioSource endTwinkle;


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (seatTrigger1.GetComponent<sit>().isOccupied && seatTrigger2.GetComponent<sit>().isOccupied)
        {
            //Debug.Log("to end");
            StartCoroutine(showThings());
            
        }
    }

    void showCups()
    {
        teaCup1.SetActive(true);
        teaCup2.SetActive(true);

        
        showGradually(teaCup1);
        showGradually(teaCup2);
   

    }

    void showButtons()
    {
        button1.SetActive(true);
        button2.SetActive(true);

        showGradually(button1);
        showGradually(button2);
    }

    void showEndText()
    {
        if (playSound == false)
        {
            endTwinkle.Play();
            playSound = true;
        }
        EndText.SetActive(true);
        showGradually(EndText);
    }
    
    void showGradually(GameObject gb)
    {
        //Debug.Log(canShowText);
        alpha += Time.deltaTime / 2;
        //Debug.Log("alpha: " + alpha);
        if (alpha > 1)
            alpha = 1;
        gb.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);
    }

    IEnumerator showThings()
    {
        yield return new WaitForSeconds(1);
        showCups();
        //showButtons();
        showEndText();
    }
}
