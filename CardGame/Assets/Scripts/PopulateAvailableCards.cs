using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PopulateAvailableCards : MonoBehaviour
{
    public GameObject deckPrefab;
    public GameObject prefab;
    public Transform AvailableGrid;
    public Transform DeckGrid;
    public Deck MyDeck;
    
    
    //This is a List of all possible cards
    public List<Card> cards = new List<Card>();
    public CardInfo cardInfo;

    public Text cardsInDeck;
    private Card currentCard;

    private bool showMessage = false;
    private void OnGUI()
    {
        if (showMessage)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 300, 100), "You can't add this card to your deck."))
            {
                showMessage = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //For Testing only
        if(PlayerInfo.playerCardInventory.Count <= 0)
        {
            foreach (Card card in cards)
            {
                PlayerInfo.playerCardInventory.Add(card, 5);
            }
        }

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

        foreach (KeyValuePair<Card, int> card in PlayerInfo.playerCardInventory)
        {
            newObj = (GameObject)Instantiate(prefab, AvailableGrid);
            newObj.GetComponent<CardDisplay>().card = card.Key;
            newObj.GetComponent<CardDisplay>().statsText.text = card.Key.ATK.ToString("D2") + "/" + card.Key.HP.ToString("D2");//This text is disabled but we need to have a place to put it
            newObj.GetComponent<CardDisplay>().card.artWork = card.Key.artWork;
            newObj.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card.Key); });
            if (newObj.GetComponentInChildren<Text>().name == "txtCardsOwned") 
            { 
                newObj.GetComponentInChildren<Text>().text = " X " + card.Value.ToString(); 
            }
        }

        foreach (KeyValuePair<int, Card> card in Deck.deck)
        {
            newObj = (GameObject)Instantiate(deckPrefab, DeckGrid);
            newObj.GetComponent<CardDisplay>().card = card.Value;
            newObj.GetComponent<CardDisplay>().statsText.text = card.Value.ATK.ToString("D2") + "/" + card.Value.HP.ToString("D2");
            newObj.GetComponent<Button>().image.sprite = card.Value.artWork;
            newObj.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card.Value); });
        }
    }

    void PopulateDeckGrid(Card card)
    {
        GameObject newObj;
        newObj = (GameObject)Instantiate(deckPrefab, DeckGrid);
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
        if (CanWeAddThisCard() == true)
        {
            cardInfo.CardInfoPanelHide();

            MyDeck.AddCardToDeck(currentCard);
            PlayerInfo.playerCardInventory[currentCard]--;
            UpdateAvailableCardsGrid();
            PopulateDeckGrid(currentCard);   
        }
        else
        {
            showMessage = true;
        }
    }
    bool CanWeAddThisCard()
    {
        
        //Don't add more than cards than are aloud.
        if (Deck.deck.Count >= MyDeck.maxCardsInDeck)
        {
            return false;
        }

        int numberOfSameCards = 0;
        foreach (KeyValuePair<int, Card> card in Deck.deck)
        {
            if (currentCard.ToString() == Deck.deck[card.Key].ToString())
            {
                numberOfSameCards++;
                if (numberOfSameCards >= MyDeck.maxSameCardsInDeck)
                {
                    return false;
                }
            }
        }

        //Does the player own this card to add it
        if (PlayerInfo.playerCardInventory[currentCard] <= 0)
        {
            return false;
        }

        //None of the above conditions have been hit so we can add it
        return true;
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
                        PlayerInfo.playerCardInventory[currentCard]++;
                        UpdateAvailableCardsGrid();
                        //Debug.Log(currentCard + " Removed card from Deck. There are now " + Deck.deck.Count + " Cards in My Deck");
                        //Debug.Log(cardsInDeck.Key);
                        RemoveCardFromDeckGrid();
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("This card isn't in your deck");
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
            newObj = (GameObject)Instantiate(deckPrefab, DeckGrid);
            newObj.GetComponent<CardDisplay>().card = card.Value;
            newObj.GetComponent<CardDisplay>().statsText.text = card.Value.ATK.ToString("D2") + "/" + card.Value.HP.ToString("D2");
            newObj.GetComponent<Button>().image.sprite = card.Value.artWork;
            newObj.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card.Value); });
        }
    }

    public void UpdateAvailableCardsGrid()
    {
        //Destroy all cards
        foreach (Transform child in AvailableGrid.transform)
        {
            Destroy(child.gameObject);
        }

        //Repopulate
        GameObject newObj;
        foreach (KeyValuePair<Card, int> card in PlayerInfo.playerCardInventory)
        {
            newObj = (GameObject)Instantiate(prefab, AvailableGrid);
            newObj.GetComponent<CardDisplay>().card = card.Key;
            newObj.GetComponent<CardDisplay>().statsText.text = card.Key.ATK.ToString("D2") + "/" + card.Key.HP.ToString("D2");//This text is disabled but we need to have a place to put it
            newObj.GetComponent<CardDisplay>().card.artWork = card.Key.artWork;
            newObj.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card.Key); });
            if (newObj.GetComponentInChildren<Text>().name == "txtCardsOwned")
            {
                newObj.GetComponentInChildren<Text>().text = " X " + card.Value.ToString();
            }
        }
    }
}
