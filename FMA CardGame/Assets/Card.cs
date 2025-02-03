using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Card : MonoBehaviour
{
    public bool inHand;
    public int handIndex;
    public int enemyHandIndex;
    public Vector3 priorSpace;
    public bool isHovering;
    public GameManager gm;
    public PlayerScript pScript;
    public Turns turns;
    public bool isPlayed;
    public string type;

    public int manaCost;
    public int slotHolder;
    public int blockValue;

    public SpriteRenderer sr;

    public VideoPlayer videoPlayer;
    public int speed;



    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    //private void OnMouseDown()
    //{
    //    if(inHand == true)
    //    {
    //        transform.position += Vector3.up * 5;
    //        inHand = true;
    //        gm.availableCardSlots[handIndex] = true;
    //        Invoke("PlayCard", 2f);
    //    }
    //}

    public void MoveToDiscardPile()
    {
        gm.discardPile.Add(this);
        this.gameObject.transform.position = new Vector3(100,100,100);
    }

    public void OnMouseDown()
    {
        
       
                 
        

        
        
       if (turns.isYourTurn == true && isPlayed == false)
       {
            gm.playCard(this);
            print("PlayingCard");
            //turns.currentMana -= manaCost;

            
       }

        else if (turns.isYourTurn == false)
        {
            turns.cantPlayText.enabled = true;

            StartCoroutine(waitRoutine());

            
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && isPlayed == true)
        {
            
            print("slot holder is: " + gm.slotHolder);
            MoveToDiscardPile();
            if(type == "Defend")
            {
                pScript.defendStacks -= 10;
            }
            print("Moving");
            gm.availableBoardSlots[this.slotHolder] = true;
            gm.playedCards[this.slotHolder] = null;
        }
    }

    private void Update()
    {
        if (isHovering == true && Input.GetKeyDown(KeyCode.R))
        {
            print("unhover");
            isHovering = false;
            transform.position = priorSpace;
            //transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);




        }



        if (isHovering == true && Input.GetKeyDown(KeyCode.B))
        {
            sr.enabled = false;
            videoPlayer.Play();
            turns.currentMana += this.manaCost;
            gm.deconText.enabled = true;
            gm.availableCardSlots[handIndex] = true;
            isHovering = false;
            StartCoroutine(deconstructEnum());
            print("decon");
           
            
        }
    }
    public void OnMouseExit()
    {
       
        
     
    }

    public IEnumerator waitRoutine()
    {
        yield return new WaitForSeconds(2);
        turns.cantPlayText.enabled = false;       
    }

    IEnumerator deconstructEnum()
    {
        yield return new WaitForSeconds(2);
        gm.deconText.enabled = false;
        MoveToDiscardPile();
    }
}
