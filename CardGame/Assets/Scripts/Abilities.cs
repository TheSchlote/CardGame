using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
public class Abilities : MonoBehaviour
{
    public BattleSystem battleSystem;

    KeyValuePair<int, Card> currentCard;
    private int cardIndex;
    //Name of the coroutine is the name of the card

    public IEnumerator Foa_Joey()
    {
        for (int i = 0; i < battleSystem.playerBattleStation.childCount; i++)
        {
            if(battleSystem.playerBattleStation.GetChild(i).GetComponent<CardDisplay>().card == currentCard.Value)
            {
                if (battleSystem.playerBattleStation.GetChild(i).GetComponent<CardDisplay>().cardKey == currentCard.Key)
                {
                    cardIndex = i;
                    break;
                }

            }
        }
        int cardATK = Convert.ToInt32(battleSystem.playerBattleStation.GetChild(cardIndex).GetComponent<CardDisplay>().statsText.text.ToString().Split('/')[0]);
        int cardHP = Convert.ToInt32(battleSystem.playerBattleStation.GetChild(cardIndex).GetComponent<CardDisplay>().statsText.text.ToString().Split('/')[1]);

        battleSystem.playerBattleStation.GetChild(cardIndex).GetComponent<CardDisplay>().statsText.text = (cardATK + 10).ToString("D2") + "/" + cardHP.ToString("D2");
        ArenaManager.totalPlayerATK = ArenaManager.totalPlayerATK + 10;

        yield return new WaitForSeconds(.5f);
     
        //This will probably need to be in every ability. 
        battleSystem.BuffPhase();
    }

    public void SelectCardOnField(KeyValuePair<int, Card> card)
    {
        if (battleSystem.state.ToString() == "BUFFPHASE")
        {
            currentCard = card;
            battleSystem.StartCoroutine("ApplyBuff");
        }
            
    }

}
