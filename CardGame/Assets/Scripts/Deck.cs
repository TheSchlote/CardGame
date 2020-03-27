using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    //public static List<Card> deck = new List<Card>();
    public static Dictionary<int, Card> deck = new Dictionary<int, Card>();
    public List<Card> cards = new List<Card>();
    public int  numberOfCardsinDeck = deck.Count;


    // Start is called before the first frame update
    void Start()
    {
        //Lets add our cards to our deck
        //deck.Add(Card. );
    }

    // Update is called once per frame
    void Update()
    {
        //When we draw cards lets take them from here.
    }

    public void AddCardToDeck(Card card)
    {
        numberOfCardsinDeck = deck.Count;

        if (!deck.ContainsKey(numberOfCardsinDeck))
        {
            
            deck.Add(numberOfCardsinDeck, card);
        }
        else
        {
            Debug.Log("Uh oh, I added 1");
            while (deck.ContainsKey(numberOfCardsinDeck))
            {
                numberOfCardsinDeck++;
                Debug.Log("Uh oh, I added 1 again");
            }
            deck.Add(numberOfCardsinDeck, card);

        }
    }

    public void RemoveCardFromDeck(int cardindex)
    {
        deck.Remove(cardindex);
    }
}
