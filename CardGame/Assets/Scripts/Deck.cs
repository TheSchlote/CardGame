using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public int maxCardsInDeck = 15;
    public int maxSameCardsInDeck = 4;
    //Players Deck
    public static Dictionary<int, Card> deck = new Dictionary<int, Card>();
    //A list of everyCard available
    public List<Card> cards = new List<Card>();

    public int  numberOfCardsinDeck = deck.Count;

    //This may need to be its own scrip. but for now this works.
    public static Dictionary<int, Card> enemyDeck = new Dictionary<int, Card>();


    public void AddCardToDeck(Card card)
    {
        numberOfCardsinDeck = deck.Count;
        //Does this number exist in our deck
        if (!deck.ContainsKey(numberOfCardsinDeck))
        {
            deck.Add(numberOfCardsinDeck, card);
        }
        else
        {
            //Debug.Log("Uh oh, I added 1");
            //We need to keep looping though this until we free up a number.
            while (deck.ContainsKey(numberOfCardsinDeck))
            {
                numberOfCardsinDeck++;
                //Debug.Log("Uh oh, I added 1 again");
            }
            deck.Add(numberOfCardsinDeck, card);
        }
    }

    public void RemoveCardFromDeck(int cardindex)
    {
        deck.Remove(cardindex);
    }
}
