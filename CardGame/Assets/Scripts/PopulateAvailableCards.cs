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
    private bool playAnimationForDeck = false;

    private bool showMessage = false;
    private void OnGUI()
    {
        if (showMessage)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 300, 100), "You can't add anymore card to your deck."))
            {
                showMessage = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateGrids();
    }

    // Update is called once per frame
    void Update()
    {
        //I dont want both of these to run at the same time

        if (playAnimationForDeck)
        {
            AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
            if (Deck.deck.Count > 0)
            {
                DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", true);
            }
        }
        else
        {
            if (Deck.deck.Count > 0)
            {
                DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
            }
            AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", true);
        }
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
        //Dont add more than 30 Cards.
        if (Deck.deck.Count < 10)
        {
            cardInfo.CardInfoPanelHide();
            Card selectedcard = AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<CardDisplay>().card;

            MyDeck.AddCardToDeck(selectedcard);
            Debug.Log(selectedcard + " Added to Deck. There are now " + Deck.deck.Count + " Cards in My Deck");
            PopulateDeckGrid(selectedcard);
        }
        else
        {
            showMessage = true;
        }
    }

    public void RemoveCard()
    {
        cardInfo.CardInfoPanelHide();
        Card selectedcard = DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<CardDisplay>().card;
        MyDeck.RemoveCardFromDeck(deckCardIndex);
        Debug.Log(selectedcard + " Removed card from Deck. There are now " + Deck.deck.Count + " Cards in My Deck");
        RemoveCardFromDeckGrid();

    }

    public void SelectAvailableCard()
    {
        Card selectedcard = AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<CardDisplay>().card;
        cardInfo.CardInfoPanelShow();
        cardInfo.ShowCardInfo(selectedcard);
    }

    public void SelectDeckCard()
    {
        if (Deck.deck.Count > 0)
        {
            Card selectedcard = DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<CardDisplay>().card;
            cardInfo.CardInfoPanelShow();
            cardInfo.ShowCardInfo(selectedcard);
        }
    }

    public void RemoveCardFromDeckGrid()
    {
        Destroy(DeckGrid.transform.GetChild(deckCardIndex).gameObject);
    }

    public void AvailableMinusCard()
    {
        playAnimationForDeck = false;
        if (availableCardIndex > 0)
        {
            AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
            availableCardIndex--;
        }
        Debug.Log("Card Index is " + availableCardIndex);
    }
    
    public void AvailablePlusCard()
    {
        playAnimationForDeck = false;
        if (availableCardIndex < AvailableGrid.childCount-1)
        {
            AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
            availableCardIndex++;
        }
        Debug.Log("Card Index is " + availableCardIndex);
    }

    public void DeckMinusCard()
    {
        playAnimationForDeck = true;
        if (deckCardIndex > 0)
        {
            DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
            deckCardIndex--;
        }
        Debug.Log("Card Index is " + deckCardIndex);
    }

    public void DeckPlusCard()
    {
        playAnimationForDeck = true;
        if (deckCardIndex < DeckGrid.childCount - 1)
        {
            DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
            deckCardIndex++;
        }
        Debug.Log("Card Index is " + deckCardIndex);
    }


}
