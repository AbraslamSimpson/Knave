using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turns : MonoBehaviour

{

    public GameManager gm;
    public bool isYourTurn;
    public int yourTurn;
    public int opponentsTurn;

    public Text turnText;

    public int maxMana;
    public int currentMana;
    public Text manaText;
    public Text cantPlayText;

    public enemyMainScript eMS;
    public runSequence rSeq;

    public int maxPlays = 2;
    public int playsLeft;

    public int lastNumber;

    // Start is called before the first frame update
    void Start()
    {
        isYourTurn = true;
        yourTurn = 1;
        opponentsTurn = 0;

        maxMana = 1;
        currentMana = 1;
        cantPlayText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isYourTurn == true)
        {
            turnText.text = "Your Turn";
        }
        else { turnText.text = "Your Opponents Turn"; }

        //manaText.text = currentMana + "/" + maxMana;

    }

    public void EndYourTurn()
    {
        if (isYourTurn == true)
        {   
            isYourTurn = false;
            opponentsTurn += 1;
            rSeq.sequenceDecider();
        
            
            //OpponentsTurn();
        }
        else if (isYourTurn == false)
        {
            isYourTurn = true;
            yourTurn += 1;

            
            currentMana = maxMana;
            gm.DrawCard();
            

        }
    }

    public void EndOpponentsTurn()
    {
        isYourTurn = true;
        yourTurn += 1;
        gm.DrawCard();
        gm.DrawCard();

    }

    public void OpponentsTurn()
    {
        gm.enemyDraw();
        gm.enemyDraw();

        for (int i = 0; i < 2; i++)
        {
            int randomNumber = Random.Range(0, gm.enemyHand.Count);
            //GetRandom(0, gm.enemyHand.Count);
            print("enemy random number is: " + lastNumber);
            //eMS.handHiders[randomNumber].SetActive(false);
            //print("your number is: " + randomNumber);
            Card randCard = gm.enemyHand[randomNumber];
            print("I want to play" + randCard);
            //randCard = Instantiate(randCard);
            gm.enemyPlayCard(randCard);
          
           
            
        }
        //print("reached end");,,.;,yp;./
        //StartCoroutine(waitForTurn());

        rSeq.enemySequenceDecider();

        
    }
    public IEnumerator waitForTurn()
    {
            yield return new WaitForSeconds(2);
            eMS.anim.SetBool("isDamaged", false);
            eMS.anim.SetBool("isAttacking", false);
            EndOpponentsTurn();
    }

    int GetRandom(int min, int max)
    {
        int rand = Random.Range(min, max);
        while (rand == lastNumber)
            rand = Random.Range(min, max);
        lastNumber = rand;
        return rand;
    }
}
