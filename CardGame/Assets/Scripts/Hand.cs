using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    public BattleSystem battleSystem;
    public static Dictionary<int, Card> hand = new Dictionary<int, Card>();
    public static Dictionary<int, Card> cardsToPlay = new Dictionary<int, Card>();
    public Transform HandGrid;
    private int cardIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCard()
    {
        Card selectedcard = HandGrid.GetChild(cardIndex).gameObject.GetComponent<CardDisplay>().card;
        cardsToPlay.Add(cardIndex, selectedcard);
        Debug.Log(selectedcard + " Added to CardstoPlay. There are now " + cardsToPlay.Count + " Cards ready to be played");
    }

    public void LeftCard()
    {
        if (cardIndex > 0)
        {
            cardIndex--;
        }
        Debug.Log("Card Index is " + cardIndex);
    }

    public void RightCard()
    {
        if (cardIndex < HandGrid.childCount - 1)
        {
            cardIndex++;
        }
        Debug.Log("Card Index is " + cardIndex);
    }

    public void Confirm()
    {
        //Now that we've confirmed remove those cards from our hand so we can't play them again
        foreach(KeyValuePair<int, Card>card in cardsToPlay)
        {
            hand.Remove(card.Key);
        }
        //Summon them!
        battleSystem.MyHandWindow.SetActive(false);
        battleSystem.StartCoroutine("SummonCards");
    }
}
