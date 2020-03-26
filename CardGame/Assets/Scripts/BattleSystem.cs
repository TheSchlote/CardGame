using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                SceneManager.LoadScene("Main");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        //Who goes first?
        
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
        //GameObject[] gameObjects = new GameObject[Hand.hand.Count];

        //foreach (KeyValuePair<int, Card> card in Hand.hand)
        //{
        //    playerPrefab.GetComponent<CardDisplay>().card = card.Value;
        //    gameObjects[i] = Instantiate(playerPrefab, playerHand);
        //}

        
        //After drawing total up friend points
        foreach (KeyValuePair<int, Card> cardinHand in Hand.hand)
        {
            ArenaManager.totalJoeyPoints += cardinHand.Value.points;
            yield return new WaitForSeconds(0.5f);
        }

        

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

        StartCoroutine(SummonCards());

        
        

        //BuffPhase should be next. But for now lets just attack.
        
        
    }
    IEnumerator SummonCards()
    {
        //I'll need to pass through a list of chose cards here.

        


        //For now play all cards in hand. Eventually will be player chosen.
        foreach (KeyValuePair<int, Card> card in Hand.hand)
        {
            Hand.cardsToPlay.Add(card.Key, card.Value);
        }
        //Since we played all of our cards our hand is empty...
        Hand.hand.Clear();

        //Put the cards on the fields.

        GameObject[] gameObjects = new GameObject[Hand.cardsToPlay.Count];
        int joey = Hand.cardsToPlay.Count - 1;
        foreach (KeyValuePair<int, Card> card in Hand.cardsToPlay)
        {
            playerPrefab.GetComponent<CardDisplay>().card = card.Value;
            //I think this will work it will just fill the cards backwards?
            yield return new WaitForSeconds(1f);
            gameObjects[joey] = Instantiate(playerPrefab, playerBattleStation);
            Debug.Log("Card Slot " + joey + " filled");
            joey--;
        }

        yield return new WaitForSeconds(1f);
        GameObject EnemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        
        Debug.Log("Battle is set up.");

        StartCoroutine(AddPoints());
    }
    IEnumerator AddPoints()
    {
        //Total up ATK and HP
        for (int i = 0; i < playerBattleStation.childCount; i++)
        {
            ArenaManager.totalPlayerATK += playerBattleStation.GetChild(i).GetComponent<CardDisplay>().card.ATK;
            ArenaManager.totalPlayerHP += playerBattleStation.GetChild(i).GetComponent<CardDisplay>().card.HP;
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < enemyBattleStation.childCount; i++)
        {
            ArenaManager.totalEnemyATK += enemyBattleStation.GetChild(i).GetComponent<CardDisplay>().card.ATK;
            ArenaManager.totalEnemyHP += enemyBattleStation.GetChild(i).GetComponent<CardDisplay>().card.HP;
            yield return new WaitForSeconds(0.5f);

            // this part should really happen after enemy draw
            ArenaManager.totalNickPoints += enemyBattleStation.GetChild(i).GetComponent<CardDisplay>().card.points;
        }

        BattlePhase();
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
            
            PlayerAttack();
        }
        else
        {
            //Player Attacks First

            //Damage is dealt
            PlayerAttack();

            //Wait
            
            EnemyAttack();    
        }

        //End of the battlephase
        
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


}
