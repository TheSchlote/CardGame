using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public enum BattleState { START,DRAWPHASE, ABILITYPHASE, SUMMONPHASE, BUFFPHASE, BATTLEPHASE, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public ArenaManager MyArena;
    public Hand MyHand;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject MyHandWindow;
    
    public Transform playerHand;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public PlayerInfo player;

    //We need a new copy of our deck to manipulate
    static Dictionary<int, Card> deck = new Dictionary<int, Card>();

    public BattleState state;

    public bool enemyFirst = false;

    
    private void OnGUI()
    {
        if (ArenaManager.player2ndWin == true || ArenaManager.enemy2ndWin == true)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), state.ToString()))
            {
                ResetRound();
                MyArena.ResetArena();
                player.AddExperience(60);
                SceneManager.LoadScene("Main");
            }
        }
    }
    private void Awake()
    {
        MyHandWindow.SetActive(false);
        deck = new Dictionary<int, Card>(Deck.deck);
    }
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        //Who goes first?
        
        //Both Players Draw...
        StartCoroutine(DrawPhase());
    }

    void NewRound()
    {
        state = BattleState.START;
        //Who goes first?

        //Both Players Draw...
        StartCoroutine(DrawPhase());
    }

    IEnumerator DrawPhase()
    {
        yield return new WaitForSeconds(0.5f);
        state = BattleState.DRAWPHASE;

        //during the DrawPhase we always want to draw 5
        StartCoroutine(DrawCardsFromDeck(5));

    }


    void SummonPhase()
    {
        state = BattleState.SUMMONPHASE;
        Debug.Log("Select Available cards...");

        MyHandWindow.SetActive(true);

        //When Card that has cost is selected, subtract appropriate points.
        //Confirm

        //Enemy AI plays all available cards.
        //Animation

        //BuffPhase should be next. But for now lets just attack.

    }
    IEnumerator SummonCards()
    {


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
        
        //Discard whatevers in your hand at the end of the round.
        Hand.cardsToPlay.Clear();

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
                ResetRound();
                NewRound();
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
                ResetRound();
                NewRound();
            }
            else
            {
                ArenaManager.player2ndWin = true;
            }
        }
    }

    IEnumerator DrawCardsFromDeck(int drawAmount)
    {
        //Randomly Draw 5 card.
        for (int i = 0; Hand.hand.Count < drawAmount; i++)
        {
            //I cant draw cards from my deck if there are none.
            if (deck.Keys.Count > 0)
            {
                int cardindex = Random.Range(deck.Keys.Min(), deck.Keys.Max());
                //Add distinct list of card indexes to draw
                if (deck.ContainsKey(cardindex))
                {
                    //Add that card to our hand
                    Hand.hand.Add(cardindex, deck[cardindex]);

                    //And Remove that card so we dont draw it again.
                    deck.Remove(cardindex);
                }
            }
            else
            {
                break;
            }

        }
        MyHandWindow.SetActive(true);
        GameObject[] gameObjects = new GameObject[Hand.hand.Count];
        int joey = Hand.hand.Count - 1;
        foreach (KeyValuePair<int, Card> card in Hand.hand)
        {
            playerPrefab.GetComponent<CardDisplay>().card = card.Value;
            ArenaManager.totalJoeyPoints += card.Value.points;
            //I think this will work it will just fill the cards backwards?
            yield return new WaitForSeconds(1f);
            gameObjects[joey] = Instantiate(playerPrefab, playerHand);
            Debug.Log("Hand Slot " + joey + " filled");
            joey--;
        }
        MyHandWindow.SetActive(false);
        //After drawing total up friend points


        if(state == BattleState.DRAWPHASE)
        {
            SummonPhase();
        }

    }

    void ResetRound()
    {
        MyArena.ResetArenaPoints();
        MyHand.cardIndex = 0;
        Hand.hand.Clear();
        foreach( Transform child in playerBattleStation.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform card in enemyBattleStation.transform)
        {
            Destroy(card.gameObject);
        }
        foreach (Transform card in playerHand.transform)
        {
            Destroy(card.gameObject);
        }
    }
}
