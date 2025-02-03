using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardToHand1 : MonoBehaviour
{
    public GameObject Hand;
    public GameObject Card;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hand = GameObject.Find("Hand");
        Card.transform.SetParent(Hand.transform);
        Card.transform.localScale = Vector3.one;
        Card.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Card.transform.eulerAngles = new Vector3(25, 0, 0);
    }
}
