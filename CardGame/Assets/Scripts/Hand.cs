﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Hand : MonoBehaviour
{
    public BattleSystem battleSystem;
    public static Dictionary<int, Card> hand = new Dictionary<int, Card>();
    public static Dictionary<int, Card> cardsToPlay = new Dictionary<int, Card>();
    public static Dictionary<int, Card> enemyHand = new Dictionary<int, Card>();
    public static Dictionary<int, Card> enemyCardsToPlay = new Dictionary<int, Card>();
    public Transform HandGrid;
    public int cardIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectCard(KeyValuePair<int, Card> selectedcard)
    {
        //The for loop below is how we know which card to highlight
        int cardIndex = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            if (battleSystem.MyHandWindow.transform.GetChild(0).transform.GetChild(i).GetComponent<CardDisplay>().card == selectedcard.Value)
            {
                if (hand.Keys.ToList()[i] == selectedcard.Key)
                {
                    cardIndex = i;
                    break;
                }
            }
        }

        if (cardsToPlay.ContainsKey(selectedcard.Key))
        { 
            cardsToPlay.Remove(selectedcard.Key);
            //Debug.Log(selectedcard + " Removed from CardstoPlay. There are now " + cardsToPlay.Count + " Cards ready to be played");
            battleSystem.MyHandWindow.transform.GetChild(0).transform.GetChild(cardIndex).GetComponent<Image>().color = Color.white;
        }
        else
        {
            cardsToPlay.Add(selectedcard.Key, selectedcard.Value);
            //Debug.Log(selectedcard + " Added to CardstoPlay. There are now " + cardsToPlay.Count + " Cards ready to be played");
            battleSystem.MyHandWindow.transform.GetChild(0).transform.GetChild(cardIndex).GetComponent<Image>().color = Color.yellow;
        }
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
