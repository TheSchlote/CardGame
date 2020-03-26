using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PopulateAvailableCards : MonoBehaviour
{
    public GameObject prefab;
    public Transform AvailableGrid;
    public Transform DeckGrid;
    public Deck MyDeck;
    int cardIndex = 0;
    public List<Card> cards = new List<Card>();
    //public int numberToCreate;

    // Start is called before the first frame update
    void Start()
    {
        PopulateGrids();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PopulateGrids()
    {
        GameObject newObj;

        foreach(Card card in cards)
        {
            newObj = (GameObject)Instantiate(prefab, AvailableGrid);
            newObj.GetComponent<CardDisplay>().card = card;
        }

        foreach (KeyValuePair<int, Card> card in Deck.deck)
        {
            newObj = (GameObject)Instantiate(prefab, DeckGrid);
            newObj.GetComponent<CardDisplay>().card = card.Value;
        }
    }

    void PopulateDeckGrid(Card card)
    {
        GameObject newObj;
        newObj = (GameObject)Instantiate(prefab, DeckGrid);
        newObj.GetComponent<CardDisplay>().card = card;
    }



    public void SelectCard()
    {
        Card selectedcard = AvailableGrid.GetChild(cardIndex).gameObject.GetComponent<CardDisplay>().card;
        MyDeck.AddCardToDeck(selectedcard);
        Debug.Log(selectedcard + " Added to Deck. There are now " + Deck.deck.Count + " Cards in My Deck");
        PopulateDeckGrid(selectedcard);
    }

    public void MinusCard()
    {
        if (cardIndex > 0)
        {
            cardIndex--;
        }
        Debug.Log("Card Index is " + cardIndex);
    }

    public void PlusCard()
    {
        if (cardIndex < AvailableGrid.childCount-1)
        {
            cardIndex++;
        }
        Debug.Log("Card Index is " + cardIndex);
    }
}
