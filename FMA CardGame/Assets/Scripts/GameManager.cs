using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<Card> deck = new List<Card>();
    public List<Card> enemyDeck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> hand = new List<Card>();
    public List<Card> enemyHand = new List<Card>();
    public List<ResourceCard> resourceDeck = new List<ResourceCard>();

    //public int attackDamage;

    public Transform[] boardSlots;
    public Transform[] cardSlots;
    public bool[] availableBoardSlots;
    public bool[] availableCardSlots;
    public Transform[] enemyBoardSlots;
    public Transform[] enemyCardSlots;
    public bool[] availableEnemyBSlots;
    public bool[] availableEnemyCSlots;

    public GameObject boardBack, boardFront;

    public Card newPlayedCard;

    public bool isHovering;
    public bool playerDefending;
    public int currentBSlot;
    public int slotHolder;

    public int enemiesLeft;

    public Turns turns;
    public enemyMainScript eMS;
    public PlayerScript pScript;

    public Text deconText;
    public Text youWinText;
    public Text youLoseText;

    public Animator anim;
    [SerializeField]
    public List<Card> playedCards = new List<Card>();
    public List<Card> EnemyPlayedCards = new List<Card>();



    //public Text deckSizeText;

    public void Start()
    {
        youLoseText.enabled = false;
        youWinText.enabled = false;

        for (int i = 0; i < 4; i++)
        { DrawCard(); enemyDraw(); };
       


        

    }

    public class DontDestroy : MonoBehaviour                                    //Dont destroy the Game Manger on loading new scene
    {
        void Awake()
        {
            GameObject[] GameMan = GameObject.FindGameObjectsWithTag("GameManager");                   //Find the GameManager game object and populate a list with that item

            if (GameMan.Length > 1)                                                                     //If the list GameMan has more than one object in it, delete it. (Saves unneccessary bloat if the game manager gets added multiple times).
            {
                Destroy(this.gameObject);                                                                   //Delete the last game object added
            }

            DontDestroyOnLoad(this.gameObject);                                                         //Dont destroy this GameManager on load.
        }
    }
    
  

    // Start is called before the first frame update
   public void DrawCard()
   {
        Random.seed = System.DateTime.Now.Millisecond;
        Card randCard = deck[Random.Range(0, deck.Count)];
        
            if (deck.Count >= 1)
            {
                
                for (int i = 0; i < availableCardSlots.Length; i++)
                {
                    if (availableCardSlots[i] == true)
                    {

                        Card copyCard = Instantiate(randCard);
                        deck.Remove(randCard);
                        copyCard.handIndex = i;
                        copyCard.transform.position = cardSlots[i].position;
                        copyCard.inHand = true;
                        copyCard.isPlayed = false;
                        availableCardSlots[i] = false;
                        break;
                    
                        
                    }
                
                }
            }
   }

    public void enemyDraw()
    {
        if (enemyDeck.Count >= 1)
        {
            Card enemyRandCard = enemyDeck[Random.Range(0, enemyDeck.Count)];

            for (int i = 0; i < availableEnemyCSlots.Length; i++)
            {
                if (availableEnemyCSlots[i] == true)
                {
                    eMS.handHiders[i].SetActive(true);
                    Card enemyCopyCard = Instantiate(enemyRandCard);
                                   
                    enemyCopyCard.enemyHandIndex = i;
                    enemyCopyCard.transform.position = enemyCardSlots[i].position;
                    //print("hiding " + enemyCopyCard.enemyHandIndex);
                    enemyCopyCard.inHand = true;
                    enemyCopyCard.isPlayed = false;
                    availableEnemyCSlots[i] = false;
                    enemyHand.Add(enemyCopyCard);
                  
                    return;
                }
            }
        }
    }


    public void DrawResource()
    {

        if (resourceDeck.Count >= 1)
        {
            ResourceCard resourceCard = resourceDeck[Random.Range(0, resourceDeck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] ==  true)
                {
                    
                    resourceCard.gameObject.SetActive(true);
                    resourceCard.handIndex = i;
                    resourceCard.transform.position = cardSlots[i].position;
                    resourceCard.inHand = true;
                    resourceCard.isPlayed = false;

                    availableCardSlots[i] = false;
                    resourceDeck.Remove(resourceCard);
                    return;
                    
                }
            }
        }
    }

   public void playCard(Card card)
    {

        if (turns.isYourTurn == true)
        {
            
            for (int i = 0; i < availableBoardSlots.Length; i++)
            {   
                if (availableBoardSlots[i] == true)
                {
                    
                    card.transform.position = boardSlots[i].position;
                    availableCardSlots[card.handIndex] = true;
                    card.isHovering = false;
                    card.isPlayed = true;
                    availableBoardSlots[i] = false;
                    card.slotHolder = i;
                    card.transform.SetParent(boardSlots[i].transform);
                    playedCards[i] = card;
                    card.transform.localScale = new Vector2(0.4f,0.4f);
                    

                   break;


                }
            }
        }
       

        
    }

    public void enemyPlayCard(Card eCard)
    {
         if (turns.isYourTurn == false)
        {
            for (int i = 0; i < availableEnemyBSlots.Length; i++)
            {
                if (availableEnemyBSlots[i] == true)
                {
                    newPlayedCard = Instantiate(eCard);
                    eMS.handHiders[newPlayedCard.enemyHandIndex].SetActive(false);
                    eCard.gameObject.SetActive(false);
                    enemyHand.Remove(eCard);
                    EnemyPlayedCards[i] = eCard;
                   
                   
                    print("Should be hiding : " + eCard.enemyHandIndex);
                    newPlayedCard.transform.position = enemyBoardSlots[i].position;
                    availableEnemyCSlots[newPlayedCard.handIndex] = true;
                    newPlayedCard.enemyHandIndex = 0;
                    newPlayedCard.isHovering = false;
                    newPlayedCard.isPlayed = true;
                    availableEnemyBSlots[i] = false;
                    
                     

                 
                    //if (newPlayedCard.type == "Attack")
                    //{
                    //    eMS.anim.SetBool("isAttacking", true);
                    //    StartCoroutine(waitRoutine());
                    //    eMS.enemyAttack();
                     

                    //}
                    //if (newPlayedCard.type == "Defend")
                    //{
                    //    eMS.enemyDefend();
                    //    StartCoroutine(waitRoutine());
                    //}
                    

                    return;


                }
            }
        }
    }

    public void returnCard(Card card)
    {
        
    }

    public void shuffle()
    {
        if (discardPile.Count >=1 )
        {
            foreach(Card card in discardPile)
            { deck.Add(card);
            }
            discardPile.Clear();
        }
    }

   


    public void Update()
    {
        //deckSizeText.text = deck.Count.ToString();

        if (enemiesLeft <= 0)
        {
            youWinText.enabled = true;
        }

        if(pScript.HP <= 0)
        {
           youLoseText.enabled = true;
            Time.timeScale = 0;
        }

    }

    public IEnumerator waitRoutine()
    {
        yield return new WaitForSeconds(2);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDefending", false);
        //turns.EndYourTurn();
        
    }
}
