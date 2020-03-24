using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState { START,DRAWPHASE, ABILITYPHASE, SUMMONPHASE, BUFFPHASE, BATTLEPHASE, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerHand;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;


    public bool enemyFirst = false;
    private void OnGUI()
    {
        if (state == BattleState.WON || state == BattleState.LOST)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), state.ToString()))
            {
                state = BattleState.START;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        //Who goes first?
        StartCoroutine(WaitForThisLong(2.0f));
        //Both Players Draw...
        StartCoroutine(DrawPhase());
    }
    IEnumerator DrawPhase()
    {
        state = BattleState.DRAWPHASE;

        //Im using a HashSet so I dont draw duplicate cards
        HashSet<int> cardsToDraw = new HashSet<int>();
        //Randomly Draw 5 card.
        for (int i = 0; cardsToDraw.Count < 5; i++)
        {
            //I cant draw cards from my deck if there are none.
            if (Deck.deck.Count < 0)
                break;
            //Add distinct list of card indexes to draw
            cardsToDraw.Add(Random.Range(0, Deck.deck.Count));
        }
        
        foreach(int index in cardsToDraw)
        {
            //Add Cards to hand
            Hand.hand.Add(index, Deck.deck[index]);

            //And Remove that card so we dont draw it again.
            Deck.deck.Remove(index);
        }

        //After Cards are drawn put them in your hand.
        GameObject[] gameObjects = new GameObject[Hand.hand.Count];


        //foreach (KeyValuePair<int, Card> card in Hand.hand)
        //{
        //    playerPrefab.GetComponent<CardDisplay>().card = card.Value;
        //    gameObjects[i] = Instantiate(playerPrefab, playerHand);
        //}

        //for (int i = 0; i < Hand.hand.Count; i++)
        //{
        //    playerPrefab.GetComponent<CardDisplay>().card = Hand.hand.;
        //    gameObjects[i] = Instantiate(playerPrefab, playerHand);
        //}

        


        //StartCoroutine(WaitForThisLong(2.0f));
        //After drawing total up friend points
        foreach (KeyValuePair<int, Card> cardinHand in Hand.hand)
        {
            ArenaManager.totalJoeyPoints += cardinHand.Value.points;
        }

        yield return new WaitForSeconds(2.0f);

        SummonPhase();
    }


    void SummonPhase()
    {
        state = BattleState.SUMMONPHASE;
        Debug.Log("Select Available cards...");


        //When Card that has cost is selected, subtract appropriate points.
        //Confirm

        //Enemy AI plays all available cards.
        //Animation

        SummonCards();

        //Total up ATK and HP
        for (int i = 0; i < playerBattleStation.childCount; i++)
        {
             ArenaManager.totalPlayerATK += playerBattleStation.GetChild(i).GetComponent<CardDisplay>().card.ATK;
             ArenaManager.totalPlayerHP += playerBattleStation.GetChild(i).GetComponent<CardDisplay>().card.HP;
            //this part should really happen during draw phase but this is fine.
        }

        for (int i = 0; i < enemyBattleStation.childCount; i++)
        {
            ArenaManager.totalEnemyATK += enemyBattleStation.GetChild(i).GetComponent<CardDisplay>().card.ATK;
            ArenaManager.totalEnemyHP += enemyBattleStation.GetChild(i).GetComponent<CardDisplay>().card.HP;
            ArenaManager.totalNickPoints += enemyBattleStation.GetChild(i).GetComponent<CardDisplay>().card.points;
        }

        //BuffPhase should be next. But for now lets just attack.
        StartCoroutine(WaitForThisLong(2.0f));
        BattlePhase();
    }
    void SummonCards()
    {
        //I'll need to pass through a list of chose cards here.

        


        //For now play all cards in hand. Eventually will be player chosen.
        
        foreach (KeyValuePair<int, Card> card in Hand.hand)
        {
            for (int i = 0; i < Hand.hand.Count; i++)
            {
                Hand.cardsToPlay.Add(i, card.Value);
                Hand.hand.Remove(i);
            }
        }

        GameObject[] gameObjects = new GameObject[Hand.cardsToPlay.Count];

        foreach (KeyValuePair<int, Card> card in Hand.cardsToPlay)
        {
            int i = 0;
            playerPrefab.GetComponent<CardDisplay>().card = card.Value;
            gameObjects[i] = Instantiate(playerPrefab, playerBattleStation);
            i++;
        }
            

        GameObject EnemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        
        Debug.Log("Battle is set up.");

        //WaitForSeconds(2.0f);
    }

    void BattlePhase()
    {
        state = BattleState.BATTLEPHASE;
        Debug.Log("Battle Phase...");
        if (enemyFirst)
        {
            //Enemy attacks First
            EnemyAttack();
            //Wait
            StartCoroutine(WaitForThisLong(2.0f));
            PlayerAttack();
        }
        else
        {
            //Player Attacks First
            PlayerAttack();
            //Wait
            StartCoroutine(WaitForThisLong(2.0f));
            EnemyAttack();    
        }

        //End of the battlephase
        StartCoroutine(WaitForThisLong(2.0f));
        WhoWon();
    }

    void EnemyAttack()
    {
        ArenaManager.totalPlayerHP = ArenaManager.totalPlayerHP - ArenaManager.totalEnemyATK;
        if (ArenaManager.totalPlayerHP < 0)
        {
            ArenaManager.totalPlayerHP = 0;
        }
    }

    void PlayerAttack()
    {
        ArenaManager.totalEnemyHP = ArenaManager.totalEnemyHP - ArenaManager.totalPlayerATK;
        if (ArenaManager.totalEnemyHP < 0)
        {
            ArenaManager.totalEnemyHP = 0;
        }
    }

    void WhoWon()
    {
        if (ArenaManager.totalPlayerHP <= ArenaManager.totalEnemyHP)
        {
            state = BattleState.LOST;
            if (ArenaManager.enemy1stWin == false)
            {
                ArenaManager.enemy1stWin = true;
            }
            else
            {
                ArenaManager.enemy2ndWin = true;
            }
        }
        else
        {
            state = BattleState.WON;
            if (ArenaManager.player1stWin == false)
            {
                ArenaManager.player1stWin = true;
            }
            else
            {
                ArenaManager.player2ndWin = true;
            }
        }
    }

    IEnumerator WaitForThisLong(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
