using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class runSequence : MonoBehaviour
{
    public GameManager gm;
    private string cardType;
    public PlayerScript pScript;
    public enemyMainScript eMS;
    public Turns turns;
    

    public List<Card> cardTypeAttack;
    public List<Card> cardTypeDefend;
    public List<Card> cardTypeMove;
    public GameObject playerSprite;

    public GameObject[] particleZone;

    public bool playerForward = false;
    public bool enemyForward = false;

    bool hasRunStarted = false;
   // public List<string> cardTypeList;
    public List<int> activeZones;

    public Text blockText;
    public int totalBlockAmount;

    int speed = 1000;
    


    private void Start()
    {
     
        particleZone[0].SetActive(false); particleZone[1].SetActive(false); particleZone[2].SetActive(false); particleZone[3].SetActive(false); particleZone[4].SetActive(false);
      
    }

    public void sequenceDecider()
    {

        foreach (Card card in gm.playedCards)
        {
            if (!card)
            {
                continue;
            }
            if (card.type == "Attack")
            {
                int index = gm.playedCards.IndexOf(card);

                if (!cardTypeAttack.Contains(card))
                {
                    cardTypeAttack.Add(card);
                    

                }

                activeZones[index] = 1;
            }

            else if (card.type == "Defend")
            {
                int index = gm.playedCards.IndexOf(card);
                if (!cardTypeDefend.Contains(card))
                {
                    cardTypeDefend.Add(card);
                    totalBlockAmount = totalBlockAmount + card.blockValue;
                }
                activeZones[index] = 2;
            }

            else if (card.type == "Move")
            {
                int index = gm.playedCards.IndexOf(card);
                if (!cardTypeMove.Contains(card))
                {
                    cardTypeMove.Add(card);
                }
                activeZones[index] = 3;
            }

           

        }

        StartCoroutine(playSequence());
    }

    public IEnumerator playSequence()
    { 

        //foreach (Card card in cardTypeAttack)
        for (int i = 0; i < activeZones.Count; i++)
        {

                if (activeZones[i] != 0)
                {
                    //particleZone[i].SetActive(true);
                    //print("about to wait");
                    //StartCoroutine(waitParticle(i));
                }
                if (activeZones[i] == 1)
                {
                    pScript.anim.SetBool("isAttacking", true);
                    particleZone[i].SetActive(true);
                    print("Attacking");
                    if (eMS.enemyIsDefending == false)
                    {

                        eMS.takeDamage(pScript.attackDamage);

                    }
                    else if (eMS.enemyIsDefending == true)
                    {
                        eMS.takeDamage(pScript.attackDamage - 15);
                    }
                    particleZone[i].SetActive(true);

                    yield return new WaitForSeconds(2);

                    pScript.anim.SetBool("isAttacking", false);
                    particleZone[i].SetActive(false);

                }
                else if (activeZones[i] == 2)
                {
                    particleZone[i].SetActive(true);
                    print("Defending");
                    pScript.isDefending = true;
                    pScript.anim.SetBool("isDefending", true);
                    pScript.defendStacks = pScript.defendStacks + 10;
                    particleZone[i].SetActive(true);
                    print("Do I make it here");
                    yield return new WaitForSeconds(2);
                    particleZone[i].SetActive(false);
                    pScript.isDefending = false;
                    pScript.anim.SetBool("isDefending", false);
                }
                if (activeZones[i] == 3)
                {
                    //pScript.anim.SetBool("isMoving", true);
                    particleZone[i].SetActive(true);

                    if (playerForward == false)
                    {
                        
                        pScript.anim.SetBool("isMoving", true);
                        /* playerSprite.transform.position = gm.boardFront.transform.position;*/
                        playerForward = true;
                        
                    }
                    else if (playerForward == true)
                    {
                        pScript.anim.SetBool("isMovingBack", true);
                        //playerSprite.transform.position = gm.boardBack.transform.position;
                        playerForward = false;
                    }

                    //playerSprite.transform.position = Vector3.MoveTowards(playerSprite.transform.position, gm.boardFront.transform.position, speed * Time.deltaTime);
                    yield return new WaitForSeconds(2);
                    
                    pScript.anim.SetBool("isMoving", false);
                    particleZone[i].SetActive(false);
                    pScript.anim.SetBool("isIdle", true);
                    pScript.anim.SetBool("isMovingBack", false);
                }
                else
                {
                    print("Nothing in element" + activeZones[i]);
                }


                //StartCoroutine(waitTwo());
            
        }    
       
        //foreach (Card card in cardTypeDefend)
        //for (int i = 0; i < cardTypeDefend.Count; i++)
        //{
           
        //    if (gm.availableBoardSlots[i] == false)
        //    {
        //        particleZone[i].SetActive(true);
        //    }

        //}

        blockText.text = totalBlockAmount.ToString();
       
        StartCoroutine(waitRoutine());
        

    }


    public void attackSlotAssigner(int i)
    {
        activeZones[i] = 1;
        return;
    }

    public void defendSlotAssigner(int i)
    {
        activeZones[i] = 2;
        return;
    }

    public IEnumerator waitRoutine()
    {
        yield return new WaitForSeconds(1);
        //pScript.anim.SetBool("isAttacking", false);
        //pScript.anim.SetBool("isDefending", false);
        ////pScript.anim.SetBool("isMoving", false);
        //for (int i = 0; i < 5; i++)
        //{
        //    particleZone[i].SetActive(false);
        //}
        
        turns.OpponentsTurn();

    }

    public IEnumerator enemyWaitRoutine()
    {
        yield return new WaitForSeconds(1);
        //pScript.anim.SetBool("isAttacking", false);
        //pScript.anim.SetBool("isDefending", false);
        ////pScript.anim.SetBool("isMoving", false);
        //for (int i = 0; i < 5; i++)
        //{
        //    particleZone[i].SetActive(false);
        //}

        turns.EndOpponentsTurn();

    }

    public IEnumerator waitParticle(int i)
    {
        yield return new WaitForSeconds(1);
        particleZone[i].SetActive(false);
    }

    public IEnumerator waitTwo()
    {
        print("Waiting");
        yield return new WaitForSeconds(1);
        print("Wait Done");
    }


    public void enemySequenceDecider()
    {

        foreach (Card card in gm.playedCards)
        {
            if (!card)
            {
                continue;
            }
            if (card.type == "Attack")
            {
                int eIndex = gm.EnemyPlayedCards.IndexOf(card);

                if (!cardTypeAttack.Contains(card))
                {
                    cardTypeAttack.Add(card);

                }

                activeZones[eIndex] = 1;
            }

            else if (card.type == "Defend")
            {
                int eIndex = gm.EnemyPlayedCards.IndexOf(card);
                if (!cardTypeDefend.Contains(card))
                {
                    cardTypeDefend.Add(card);
                    totalBlockAmount = totalBlockAmount + card.blockValue;
                }
                activeZones[eIndex] = 2;
            }

            else if (card.type == "Move")
            {
                int eIndex = gm.EnemyPlayedCards.IndexOf(card);
                if (!cardTypeMove.Contains(card))
                {
                    cardTypeMove.Add(card);
                }
                activeZones[eIndex] = 3;
            }



        }

        StartCoroutine(playEnemySequence());
    }

    public IEnumerator playEnemySequence()
    {

        //foreach (Card card in cardTypeAttack)
        for (int i = 0; i < activeZones.Count; i++)
        {

            if (activeZones[i] != 0)
            {
                
            }
            if (activeZones[i] == 1)
            {
                eMS.enemyAttack();

            }
            else if (activeZones[i] == 2)
            {
                eMS.enemyDefend();
            }
            if (activeZones[i] == 3)
            {
               

                if (enemyForward == false)
                {

                    //pScript.anim.SetBool("isMoving", true);
                    
                    enemyForward = true;

                }
                else if (enemyForward == true)
                {
                    //pScript.anim.SetBool("isMovingBack", true);
                   
                    playerForward = false;
                }

               
                yield return new WaitForSeconds(2);

                /*pScript.anim.SetBool("isMoving", false);
                particleZone[i].SetActive(false);
                pScript.anim.SetBool("isIdle", true);
                pScript.anim.SetBool("isMovingBack", false);*/
            }
            else
            {
                print("Nothing in element" + activeZones[i]);
            }


            //StartCoroutine(waitTwo());

        }

        

        blockText.text = totalBlockAmount.ToString();

        StartCoroutine(enemyWaitRoutine());
        
    }
}
