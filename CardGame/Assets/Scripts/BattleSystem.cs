using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START,DRAWPHASE, ABILITYPHASE, SUMMONPHASE, BUFFPHASE, BATTLEPHASE, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;

    //private bool playerTurn = false;

    public bool enemyFirst = false;
    public bool playAbilityCard = false;
    public bool abilityPass = false;

    public bool playBuffCard = false;
    public bool buffPass = false;
    private void OnGUI()
    {
        //GUI.Box(new Rect(20, 20, 150, 25), "GameState: " + state.ToString());

        //GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), "Joey");

        if (state == BattleState.WON)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), state.ToString()))
            {

                state = BattleState.START;
            }
        }

        //if (playBuffCard)
        //{
        //    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
        //    {

        //    }
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        //DrawPhase();

        //AbilityPhase();

//        StartCoroutine(SetupBattle());
        SummonPhase();
        //Eventually this will grab all of the playerunit's attack on the field and add them together
        //ArenaManager.totalPlayerATK += playerUnit.ATK;
        BattlePhase();
    }
    IEnumerator SetupBattle()
    {
        GameObject PlayerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = PlayerGO.GetComponent<Unit>();

        GameObject EnemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = EnemyGO.GetComponent<Unit>();


        yield return new WaitForSeconds(2f);
    }
    void SummonPhase()
    {
        state = BattleState.SUMMONPHASE;
        Debug.Log("Select Available cards...");


        //When Card that has cost is selected, subtract appropriate points.
        //Confirm

        //Enemy AI plays all available cards.
        //Animation

        StartCoroutine(SetupBattle());

        //Total up ATK and HP
        Object[] allObjects = Object.FindObjectsOfType<Unit>();

        foreach(Unit unit in allObjects)
        {
            Debug.Log(unit + "is an active object " + unit.unitName);
        }

        //BuffPhase();
    }

    void BattlePhase()
    {
        state = BattleState.BATTLEPHASE;
        Debug.Log("Battle Phase...");
        if (enemyFirst)
        {
            //Enemy attacks First
            ArenaManager.totalPlayerHP = ArenaManager.totalPlayerHP - ArenaManager.totalEnemyATK;
            if(ArenaManager.totalPlayerHP < 0)
            {
                ArenaManager.totalPlayerHP = 0;
            }
        }
        else
        {
            //Player Attacks First
            ArenaManager.totalEnemyHP = ArenaManager.totalEnemyHP = ArenaManager.totalPlayerATK;
            if(ArenaManager.totalEnemyHP < 0)
            {
                ArenaManager.totalEnemyHP = 0;
            }
        }

        //Who Won?
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
            if(ArenaManager.player1stWin == false)
            {
                ArenaManager.player1stWin = true;
            }
            else
            {
                ArenaManager.player2ndWin = true;
            }
        }

    }

    void AbilityPhase()
    {
        state = BattleState.ABILITYPHASE;
        
        Debug.Log("Ability Phase");

        if (enemyFirst == true)
        {
            //EnemyTurn();
        }
        else
        {
           // PlayerTurn();
        }
    }

    void PlayerTurn()
    {
        state = BattleState.PLAYERTURN;
        Debug.Log("PlayerTurn");

        //Pop up asking if they want to play an ability card
        if(playAbilityCard == false)
        {
            if (enemyFirst == true)
            {
                SummonPhase();
            }
            else
            {
                ///EnemyTurn();
            }
        }
        else
        {
            //Play Ability Card
        }
    }

    void EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        Debug.Log("EnemyTurn");
        //Pop up asking if they want to play an ability card
        if (playAbilityCard == false)
        {
            if (enemyFirst == true)
            {
                PlayerTurn();
            }
            else
            {
                SummonPhase();
            }
        }
        else
        {
            //Play Ability Card
        }
    }

    void DrawPhase()
    {
        state = BattleState.DRAWPHASE;
        Debug.Log("Draw Cards...");
        //Animation
        Debug.Log("Count Up Points...");
        //Animation
    }



    void BuffPhase()
    {
        state = BattleState.BUFFPHASE;
        Debug.Log("Buff Phase...");

        if (playBuffCard == false)
        {
            if (enemyFirst == true)
            {
                StartCoroutine(SetupBattle());
            }
            else
            {
                EnemyTurn();
            }
        }
        else
        {
            //Play Buff Card
        }
    }


}
