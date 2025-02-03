using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject handLocation1, handLocation2, handLocation3, handLocation4, handLocation5;
    bool H1isFull, H2isFull, H3isFull, H5isFull = false;
    bool H4isFull = false;
    public GameObject[] DeckList;
    public List<GameObject> DeckList1;

    // Start is called before the first frame update
    void Start()
    {
        DeckList = GameObject.FindGameObjectsWithTag("Card");
       
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            if (H1isFull == false)
            {   
                H1isFull = true;
                var nextCard = DeckList[0];
                Instantiate(nextCard, handLocation1.transform.position, handLocation1.transform.rotation);
                DeckList1.RemoveAt(0);
                
                print("card Drop 1");
            }
            
            else if (H2isFull == false && H1isFull == true)
            {
                var nextCard = DeckList[0];
                Instantiate(nextCard, handLocation2.transform.position, handLocation2.transform.rotation);
                H2isFull = true;
                DeckList1.RemoveAt(0);
                print("card Drop 2");
            }

            else if (H3isFull == false && H1isFull && H2isFull == true)
            {
                var nextCard = DeckList[0];
                Instantiate(nextCard, handLocation3.transform.position, handLocation1.transform.rotation);
                DeckList1.RemoveAt(0);
                H3isFull = true;
                print("card Drop 3");
            }

            else if (H4isFull == false && (H1isFull && H2isFull && H3isFull == true) )
            {
                var nextCard = DeckList[0];
                Instantiate(nextCard, handLocation4.transform.position, handLocation2.transform.rotation);
                H4isFull = true;
                DeckList1.RemoveAt(0);
                print("card Drop 4");
            }
            else if (H5isFull == false && (H1isFull && H2isFull && H3isFull && H4isFull == true) )
            {
                var nextCard = DeckList[0];
                Instantiate(nextCard, handLocation5.transform.position, handLocation1.transform.rotation);
                DeckList1.RemoveAt(0);
                H5isFull = true;
                DeckList1.RemoveAt(0);
                print("card Drop 5");
            }

           else
            { print("No Hand Space Left"); }
        }
    }


    private void OnMouseDown()
    {
        
    }
}
