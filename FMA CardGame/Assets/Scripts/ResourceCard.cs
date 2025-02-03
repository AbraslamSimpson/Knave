using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class ResourceCard : MonoBehaviour
{
    public ResourceCard resourceCard;
    public bool inHand;
    public int handIndex;
    public Vector3 priorSpace;
    public bool isHovering;
    public GameManager gm;
    public Turns turns;
    public bool isPlayed;

    public int manaValue;
    public VideoPlayer videoPlayer;

    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovering == true && Input.GetKeyDown(KeyCode.B))
        {
           sr.enabled = false;
            videoPlayer.Play();
            turns.currentMana += this.manaValue;
            gm.deconText.enabled = true;
             
            StartCoroutine(deconstructEnum());

            gm.availableCardSlots[handIndex] = true;
        }
    }

    public void OnMouseDown()
    {
        if (isHovering == false && isPlayed == false)
        {


            priorSpace = gm.cardSlots[handIndex].position;
            transform.position = new Vector3(0, 0, 0);
            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            isHovering = true;

        }
        else if (isHovering == true && turns.isYourTurn == false)
        {
            turns.cantPlayText.enabled = true;
           
            StartCoroutine(waitRoutine());


        }
    }

    public void MoveToDiscardPile()
    {
        Destroy(this);
        gameObject.SetActive(false);
    }


    IEnumerator waitRoutine()
    {
        yield return new WaitForSeconds(2);
        turns.cantPlayText.enabled = false;
    }

    IEnumerator deconstructEnum()
    {
        yield return new WaitForSeconds(2);
        gm.deconText.enabled = false;
        
    }
}
