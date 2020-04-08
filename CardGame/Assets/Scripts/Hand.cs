using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public void SelectCard(KeyValuePair<int, Card> selectedcard)
    {
        switch (battleSystem.state.ToString())
        {
            case "ABILITYPHASE":
                if (selectedcard.Value.AbilityCard == true)
                {
                    DoStuff(selectedcard);
                }
                break;

            case "SUMMONPHASE":
                if(selectedcard.Value.AbilityCard == false && selectedcard.Value.BuffCard == false)
                {
                    DoStuff(selectedcard);
                }
                break;

            case "BUFFPHASE":
                if (selectedcard.Value.BuffCard == true && cardsToPlay.Count !=1)
                {
                    DoStuff(selectedcard);
                }
                break;
        }

    }

    private void DoStuff(KeyValuePair<int, Card> selectedcard)
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
            battleSystem.MyHandWindow.transform.GetChild(0).transform.GetChild(cardIndex).GetComponent<Image>().color = Color.white;
        }
        else
        {
            cardsToPlay.Add(selectedcard.Key, selectedcard.Value);
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

        battleSystem.MyHandWindow.SetActive(false);

        switch (battleSystem.state.ToString()) 
        {
            case "ABILITYPHASE":
                break;

            case "SUMMONPHASE":
                //Summon them!
                battleSystem.StartCoroutine("SummonCards");
                break;

            case "BUFFPHASE":
                //Wait for the player to choose a card
                break;
        }

    }
}
