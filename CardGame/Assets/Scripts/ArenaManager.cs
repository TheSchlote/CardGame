using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaManager : MonoBehaviour
{
    public Text txttotalPlayerATK;
    public Text txttotalPlayerHP;
    public Text txttotalEnemyATK;
    public Text txttotalEnemyHP;

    public Text txttotalEnemyNickPoints;
    public Text txttotalEnemyJoeyPoints;
    public Text txttotalEnemyJordanPoints;
    public Text txttotalEnemyLoganPoints;
    public Text txttotalEnemyRobertPoints;
    public Text txttotalEnemyCardsInDeck;
    public Text txttotalEnemyCardsInHand;
    public Text txttotalEnemyCardsInDiscard;


    public Text txttotalNickPoints;
    public Text txttotalJoeyPoints;
    public Text txttotalJordanPoints;
    public Text txttotalLoganPoints;
    public Text txttotalRobertPoints;
    public Text txttotalCardsInDeck;
    public Text txttotalCardsInHand;
    public Text txttotalCardsInDiscard;

    public Toggle player1st;
    public Toggle player2nd;
    public Toggle enemy1st;
    public Toggle enemy2nd;

    public static int totalPlayerATK = 0;
    public static int totalPlayerHP = 1;
    public static int totalEnemyATK = 0;
    public static int totalEnemyHP = 0;

    public static bool player1stWin = false;
    public static bool player2ndWin = false;
    public static bool enemy1stWin = false;
    public static bool enemy2ndWin = false;

    public static int totalEnemyNickPoints;
    public static int totalEnemyJoeyPoints;
    public static int totalEnemyJordanPoints;
    public static int totalEnemyLoganPoints;
    public static int totalEnemyRobertPoints;
    public static int totalEnemyCardsInDeck;
    public static int totalEnemyCardsInHand;
    public static int totalEnemyCardsInDiscard;


    public static int totalNickPoints;
    public static int totalJoeyPoints;
    public static int totalJordanPoints;
    public static int totalLoganPoints;
    public static int totalRobertPoints;
    public static int totalCardsInDeck;
    public static int totalCardsInHand;
    public static int totalCardsInDiscard;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        txttotalPlayerATK.text = "ATK: " + totalPlayerATK.ToString();
        txttotalPlayerHP.text = "HP: " + totalPlayerHP.ToString();
        txttotalEnemyATK.text = "ATK: " + totalEnemyATK.ToString();
        txttotalEnemyHP.text = "HP: " + totalEnemyHP.ToString();

        player1st.isOn = player1stWin;
        player2nd.isOn = player2ndWin;
        enemy1st.isOn = enemy1stWin;
        enemy1st.isOn = enemy2ndWin;

        txttotalEnemyNickPoints.text = totalEnemyNickPoints.ToString("D2");
        txttotalEnemyJoeyPoints.text = totalEnemyJoeyPoints.ToString("D2");
        txttotalEnemyJordanPoints.text = totalEnemyJordanPoints.ToString("D2");
        txttotalEnemyLoganPoints.text = totalEnemyLoganPoints.ToString("D2");
        txttotalEnemyRobertPoints.text = totalEnemyRobertPoints.ToString("D2");
        txttotalEnemyCardsInDeck.text = totalEnemyCardsInDeck.ToString("D2");
        txttotalEnemyCardsInHand.text = totalEnemyCardsInHand.ToString("D2");
        txttotalEnemyCardsInDiscard.text = totalEnemyCardsInDiscard.ToString("D2");

        txttotalNickPoints.text = totalNickPoints.ToString("D2");
        txttotalJoeyPoints.text = totalJoeyPoints.ToString("D2");
        txttotalJordanPoints.text = totalJordanPoints.ToString("D2");
        txttotalLoganPoints.text = totalLoganPoints.ToString("D2");
        txttotalRobertPoints.text = totalRobertPoints.ToString("D2");
        txttotalCardsInDeck.text = totalCardsInDeck.ToString("D2");
        txttotalCardsInHand.text = totalCardsInHand.ToString("D2");
        txttotalCardsInDiscard.text = totalCardsInDiscard.ToString("D2");
    }

    public void ResetArena()
    {
        totalPlayerATK = 0;
        totalPlayerHP = 1;
        totalEnemyATK = 0;
        totalEnemyHP = 0;

        player1stWin = false;
        player2ndWin = false;
        enemy1stWin = false;
        enemy2ndWin = false;

        totalEnemyNickPoints = 0;
        totalEnemyJoeyPoints = 0;
        totalEnemyJordanPoints = 0;
        totalEnemyLoganPoints = 0;
        totalEnemyRobertPoints = 0;
        totalEnemyCardsInDeck = 0;
        totalEnemyCardsInHand = 0;
        totalEnemyCardsInDiscard = 0;

        totalNickPoints = 0;
        totalJoeyPoints = 0;
        totalJordanPoints = 0;
        totalLoganPoints = 0;
        totalRobertPoints = 0;
        totalCardsInDeck = 0;
        totalCardsInHand = 0;
        totalCardsInDiscard = 0;
    }
    public void ResetArenaPoints()
    {
        totalPlayerATK = 0;
        totalPlayerHP = 1;
        totalEnemyATK = 0;
        totalEnemyHP = 0;
    }

    public void AddPlayerPoints(string cardType, int points)
    {
        switch (cardType) 
        {
            case "Joey (CardType)":
                totalJoeyPoints += points;
                break;
            case "Nick (CardType)":
                totalNickPoints += points;
                break;
            case "Jordan (CardType)":
                totalJordanPoints += points;
                break;
            case "Logan (CardType)":
                totalLoganPoints += points;
                break;
            case "Robert (CardType)":
                totalRobertPoints += points;
                break;
            default:
                //No Points Added
                break;
        }
    }
    public void AddEnemyPoints(string cardType, int points)
    {
        switch (cardType)
        {
            case "Joey (CardType)":
                totalEnemyJoeyPoints += points;
                break;
            case "Nick (CardType)":
                totalEnemyNickPoints += points;
                break;
            case "Jordan (CardType)":
                totalEnemyJordanPoints += points;
                break;
            case "Logan (CardType)":
                totalEnemyLoganPoints += points;
                break;
            case "Robert (CardType)":
                totalEnemyRobertPoints += points;
                break;
            default:
                //No Points Added
                break;
        }
    }
}
