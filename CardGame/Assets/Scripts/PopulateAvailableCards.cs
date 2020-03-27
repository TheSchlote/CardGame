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
    int availableCardIndex = 0;
    int deckCardIndex = 0;
    public List<Card> cards = new List<Card>();
    public CardInfo cardInfo;

    //Animator stuff
    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        PopulateGrids();
    }

    // Update is called once per frame
    void Update()
    {
        //I propbably dont want both of these to run at the same time

        AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", true);
        //DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", true);
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
    
    public void AddCard()
    {
        cardInfo.CardInfoPanelHide();
        Card selectedcard = AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<CardDisplay>().card;

        MyDeck.AddCardToDeck(selectedcard);
        Debug.Log(selectedcard + " Added to Deck. There are now " + Deck.deck.Count + " Cards in My Deck");
        PopulateDeckGrid(selectedcard);
    }

    public void RemoveCard()
    {
        cardInfo.CardInfoPanelHide();
        Card selectedcard = DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<CardDisplay>().card;
        MyDeck.RemoveCardFromDeck(deckCardIndex);
        Debug.Log(selectedcard + " Removed card from Deck. There are now " + Deck.deck.Count + " Cards in My Deck");
        PopulateDeckGrid(selectedcard);

    }

    public void SelectAvailableCard()
    {
        Card selectedcard = AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<CardDisplay>().card;
        cardInfo.CardInfoPanelShow();
        cardInfo.ShowCardInfo(selectedcard);
    }

    public void SelectDeckCard()
    {
        Card selectedcard = DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<CardDisplay>().card;
        cardInfo.CardInfoPanelShow();
        cardInfo.ShowCardInfo(selectedcard);
    }

    public void RemoveCardFromDeckGrid()
    {
        Destroy(DeckGrid.transform.GetChild(deckCardIndex).gameObject);
    }

    public void AvailableMinusCard()
    {
        if (availableCardIndex > 0)
        {
            availableCardIndex--;
        }
        Debug.Log("Card Index is " + availableCardIndex);
    }

    public void AvailablePlusCard()
    {
        if (availableCardIndex < AvailableGrid.childCount-1)
        {
            availableCardIndex++;
        }
        Debug.Log("Card Index is " + availableCardIndex);
    }

    public void DeckMinusCard()
    {
        if (deckCardIndex > 0)
        {
            deckCardIndex--;
        }
        Debug.Log("Card Index is " + deckCardIndex);
    }

    public void DeckPlusCard()
    {
        if (deckCardIndex < DeckGrid.childCount - 1)
        {
            deckCardIndex++;
        }
        Debug.Log("Card Index is " + deckCardIndex);
    }


}
