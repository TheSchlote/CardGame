using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if(HandGrid.childCount >0)
        {
            //HandGrid.GetChild(cardIndex).gameObject.GetComponent<Animator>().SetBool("SelectCard", true);
        }
    }

    public void SelectCard(KeyValuePair<int, Card> selectedcard)
    {
        if (cardsToPlay.ContainsKey(selectedcard.Key))
        { 
            cardsToPlay.Remove(selectedcard.Key);
            Debug.Log(selectedcard + " Removed from CardstoPlay. There are now " + cardsToPlay.Count + " Cards ready to be played");
        }
        else
        {
            cardsToPlay.Add(selectedcard.Key, selectedcard.Value);
            Debug.Log(selectedcard + " Added to CardstoPlay. There are now " + cardsToPlay.Count + " Cards ready to be played");
        }
    }

    public void Confirm()
    {
        //Now that we've confirmed remove those cards from our hand so we can't play them again
        foreach(KeyValuePair<int, Card>card in cardsToPlay)
        {
            //HandGrid.GetChild(cardIndex).gameObject.GetComponent<Animator>().SetBool("SelectedCard", false);
            hand.Remove(card.Key);
            ArenaManager.totalCardsInHand = hand.Count;
        }
        //Summon them!
        battleSystem.MyHandWindow.SetActive(false);
        battleSystem.StartCoroutine("SummonCards");
    }
}
