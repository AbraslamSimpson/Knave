using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour

{
    public bool isYourTurn;
    public int yourTurn;
    public int opponentsTurn;

    public Text turnText;

    public int maxMana;
    public int currentMana;
    public Text manaText;

    public runSequence rSeq;

    // Start is called before the first frame update
    void Start()
    {
        isYourTurn = true;
        yourTurn = 1;
        opponentsTurn = 0;

        maxMana = 1;
        currentMana = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isYourTurn == true)
        {
            turnText.text = "Your Turn";
        }
        else { turnText.text = "Your Opponents Turn"; }

    }

    public void EndYourTurn()
    {
        isYourTurn = false;
        opponentsTurn += 1;

        rSeq.playSequence();
    }

    public void EndOpponentsTurn()
    {
        isYourTurn = true;
        yourTurn += 1;

        maxMana += 1;
        currentMana = maxMana;
    }
}
