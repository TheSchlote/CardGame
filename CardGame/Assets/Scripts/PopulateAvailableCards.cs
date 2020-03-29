using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PopulateAvailableCards : MonoBehaviour
{
    public GameObject prefab;
    public Transform AvailableGrid;
    public Transform DeckGrid;
    public Deck MyDeck;
    //int availableCardIndex = 0;
    //int deckCardIndex = 0;

    //This is a List of all possible cards
    public List<Card> cards = new List<Card>();
    public CardInfo cardInfo;

    public Text cardsInDeck;
    private Card currentCard;

    //Animator stuff
    //private bool playAnimationForDeck = false;

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
        cardsInDeck.text = "Deck: " + Deck.deck.Count + "/" + MyDeck.maxCardsInDeck;
    }

    void PopulateGrids()
    {
        GameObject newObj;

        foreach (Card card in cards)
        {
            newObj = (GameObject)Instantiate(prefab, AvailableGrid);
            newObj.GetComponent<CardDisplay>().card = card;
            newObj.GetComponent<CardDisplay>().statsText.text = card.ATK.ToString("D2") + "/" + card.HP.ToString("D2");
            newObj.GetComponent<CardDisplay>().card.artWork = card.artWork;
            newObj.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card); });
        }

        foreach (KeyValuePair<int, Card> card in Deck.deck)
        {
            newObj = (GameObject)Instantiate(prefab, DeckGrid);
            newObj.GetComponent<CardDisplay>().card = card.Value;
            newObj.GetComponent<CardDisplay>().statsText.text = card.Value.ATK.ToString("D2") + "/" + card.Value.HP.ToString("D2");
            newObj.GetComponent<Button>().image.sprite = card.Value.artWork;
            newObj.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card.Value); });
        }
    }

    void PopulateDeckGrid(Card card)
    {
        GameObject newObj;
        newObj = (GameObject)Instantiate(prefab, DeckGrid);
        newObj.GetComponent<CardDisplay>().card = card;
        newObj.GetComponent<CardDisplay>().statsText.text = card.ATK.ToString("D2") + "/" + card.HP.ToString("D2");
        newObj.GetComponent<Button>().image.sprite = card.artWork;
        newObj.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card); });
    }
    public void SelectCard(Card selectedcard)
    {
        currentCard = selectedcard;
        cardInfo.CardInfoPanelShow();
        cardInfo.ShowCardInfo(selectedcard);
    }
    public void AddCard()
    {
        //Dont add more than cards than are aloud.
        if (Deck.deck.Count < MyDeck.maxCardsInDeck)
        {
            if (Deck.deck.ContainsValue(currentCard))
            {

            }
            cardInfo.CardInfoPanelHide();

            MyDeck.AddCardToDeck(currentCard);
            Debug.Log(currentCard + " Added to Deck. There are now " + Deck.deck.Count + " Cards in My Deck");
            PopulateDeckGrid(currentCard);
        }
        else
        {
            showMessage = true;
        }
    }
    
    public void RemoveCard()
    {
        if (Deck.deck.ContainsValue(currentCard))
        {
            cardInfo.CardInfoPanelHide();
            
            foreach (KeyValuePair<int, Card> cardsInDeck in Deck.deck)
            {
                if (cardsInDeck.Value == currentCard)
                {
                    if (Deck.deck.ContainsKey(cardsInDeck.Key))
                    {
                        MyDeck.RemoveCardFromDeck(cardsInDeck.Key);
                        Debug.Log(currentCard + " Removed card from Deck. There are now " + Deck.deck.Count + " Cards in My Deck");
                        Debug.Log(cardsInDeck.Key);
                        RemoveCardFromDeckGrid();
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("This card isnt in your deck");
        }
    }
    
    public void RemoveCardFromDeckGrid()
    {
        //Destroy all cards
        foreach (Transform child in DeckGrid.transform)
        {
            Destroy(child.gameObject);
        }

        //Repopulate
        GameObject newObj;
        foreach (KeyValuePair<int, Card> card in Deck.deck)
        {
            newObj = (GameObject)Instantiate(prefab, DeckGrid);
            newObj.GetComponent<CardDisplay>().card = card.Value;
            newObj.GetComponent<CardDisplay>().statsText.text = card.Value.ATK.ToString("D2") + "/" + card.Value.HP.ToString("D2");
            newObj.GetComponent<Button>().image.sprite = card.Value.artWork;
            newObj.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card.Value); });
        }
    }

    //public void AvailableMinusCard()
    //{
    //    playAnimationForDeck = false;
    //    if (availableCardIndex > 0)
    //    {
    //        AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
    //        availableCardIndex--;
    //    }
    //    Debug.Log("Card Index is " + availableCardIndex);
    //}
    
    //public void AvailablePlusCard()
    //{
    //    playAnimationForDeck = false;
    //    if (availableCardIndex < AvailableGrid.childCount-1)
    //    {
    //        AvailableGrid.GetChild(availableCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
    //        availableCardIndex++;
    //    }
    //    Debug.Log("Card Index is " + availableCardIndex);
    //}

    //public void DeckMinusCard()
    //{
    //    playAnimationForDeck = true;
    //    if (deckCardIndex > 0)
    //    {
    //        DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
    //        deckCardIndex--;
    //    }
    //    Debug.Log("Card Index is " + deckCardIndex);
    //}

    //public void DeckPlusCard()
    //{
    //    playAnimationForDeck = true;
    //    if (deckCardIndex < DeckGrid.childCount - 1)
    //    {
    //        DeckGrid.GetChild(deckCardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
    //        deckCardIndex++;
    //    }
    //    Debug.Log("Card Index is " + deckCardIndex);
    //}


}
