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
    static Dictionary<int, Card> enemyDeck = new Dictionary<int, Card>();

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
                //Only Winners get XP
                if(state == BattleState.WON)
                {
                    player.AddExperience(60);
                }
                SceneManager.LoadScene("Main");
            }
        }
    }
    private void Awake()
    {
        MyHandWindow.SetActive(false);
        deck = new Dictionary<int, Card>(Deck.deck);
        ArenaManager.totalCardsInDeck = deck.Count;
        //For now the bad guy mirrors your deck.
        enemyDeck = new Dictionary<int, Card>(Deck.deck);
        ArenaManager.totalEnemyCardsInDeck = enemyDeck.Count;
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

        //Enemy Randomly Draw 4 card. so he lives in 3rd round
        for (int i = 0; Hand.enemyHand.Count < 4; i++)
        {
            //I cant draw cards from my deck if there are none.
            if (enemyDeck.Keys.Count > 0)
            {
                int cardindex = Random.Range(enemyDeck.Keys.Min(), enemyDeck.Keys.Max());
                //Add distinct list of card indexes to draw
                if (enemyDeck.ContainsKey(cardindex))
                {
                    //Add that card to our hand
                    Hand.enemyHand.Add(cardindex, enemyDeck[cardindex]);
                    ArenaManager.totalEnemyCardsInHand = Hand.enemyHand.Count;
                    MyArena.AddEnemyPoints(enemyDeck[cardindex].type.ToString(), enemyDeck[cardindex].points);
                    //And Remove that card so we dont draw it again.
                    enemyDeck.Remove(cardindex);
                    ArenaManager.totalEnemyCardsInDeck = enemyDeck.Count;
                }
            }
            else
            {
                break;
            }
        }

        //After drawing total up friend points


        //during the DrawPhase Player always want to draw 5
        StartCoroutine(DrawCardsFromDeck(5));

    }


    void SummonPhase()
    {
        state = BattleState.SUMMONPHASE;
        Debug.Log("Select Available cards...");

        //Enemy AI plays all available cards.
        foreach (KeyValuePair<int, Card> card in Hand.enemyHand)
        {
            Hand.enemyCardsToPlay.Add(card.Key, card.Value);
        }
        Hand.enemyHand.Clear();
        //When AI gets smarter they might not play all cards. Keep in mind.
        ArenaManager.totalEnemyCardsInHand = Hand.enemyHand.Count;
        MyHandWindow.SetActive(true);

        //When Card that has cost is selected, subtract appropriate points.
        //Confirm

        
        //Animation

        //BuffPhase should be next. But for now lets just attack.

    }
    IEnumerator SummonCards()
    {
        //Put the cards on the fields.

        GameObject[] gameObjects = new GameObject[Hand.cardsToPlay.Count];
        int cardSlot = Hand.cardsToPlay.Count - 1;
        foreach (KeyValuePair<int, Card> card in Hand.cardsToPlay)
        {
            playerPrefab.GetComponent<CardDisplay>().card = card.Value;
            //I think this will work it will just fill the cards backwards?
            yield return new WaitForSeconds(1f);
            gameObjects[cardSlot] = Instantiate(playerPrefab, playerBattleStation);
            ArenaManager.totalCardsInHand--;
            Debug.Log("Card Slot " + cardSlot + " filled");
            cardSlot--;
        }
        //Just want to make sure this is accurate
        ArenaManager.totalCardsInHand = Hand.hand.Count;
        //Discard whatevers in your hand at the end of the round.
        Hand.cardsToPlay.Clear();


        //Enemy plays his cards
        yield return new WaitForSeconds(1f);

        GameObject[] enemyGameObjects = new GameObject[Hand.enemyCardsToPlay.Count];
        int badGuySlot = Hand.enemyCardsToPlay.Count - 1;
        foreach (KeyValuePair<int, Card> card in Hand.enemyCardsToPlay)
        {
            enemyPrefab.GetComponent<CardDisplay>().card = card.Value;
            //I think this will work it will just fill the cards backwards?
            yield return new WaitForSeconds(1f);
            enemyGameObjects[badGuySlot] = Instantiate(enemyPrefab, enemyBattleStation);
            ArenaManager.totalEnemyCardsInHand--;
            badGuySlot--;

        }
        ArenaManager.totalEnemyCardsInHand = Hand.enemyHand.Count();
        Hand.enemyCardsToPlay.Clear();

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
        int handSlot = Hand.hand.Count - 1;
        foreach (KeyValuePair<int, Card> card in Hand.hand)
        {
            playerPrefab.GetComponent<CardDisplay>().card = card.Value;
            yield return new WaitForSeconds(1f);
            MyArena.AddPlayerPoints(card.Value.type.ToString(), card.Value.points);
            ArenaManager.totalCardsInDeck--;
            ArenaManager.totalCardsInHand++;
            gameObjects[handSlot] = Instantiate(playerPrefab, playerHand);
            
            Debug.Log("Hand Slot " + handSlot + " filled");
            handSlot--;
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
        ArenaManager.totalCardsInDiscard += Hand.hand.Count();
        Hand.hand.Clear();
        foreach( Transform child in playerBattleStation.transform)
        {
            Destroy(child.gameObject);
            ArenaManager.totalCardsInDiscard++;
        }
        foreach (Transform card in enemyBattleStation.transform)
        {
            Destroy(card.gameObject);
            ArenaManager.totalEnemyCardsInDiscard++;
        }
        foreach (Transform card in playerHand.transform)
        {
            Destroy(card.gameObject);
        }
    }
}
