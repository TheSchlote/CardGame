using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState { START,DRAWPHASE, ABILITYPHASE, SUMMONPHASE, BUFFPHASE, BATTLEPHASE, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;

    public bool enemyFirst = false;

    private void OnGUI()
    {
        if (state == BattleState.WON)
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
        Debug.Log("Player card count: " + playerBattleStation.childCount);
        //Total up ATK and HP
        for (int i = 0; i < playerBattleStation.childCount; i++)
        {
             ArenaManager.totalPlayerATK += playerBattleStation.GetChild(i).GetComponent<CardDisplay>().card.ATK;
             ArenaManager.totalPlayerHP += playerBattleStation.GetChild(i).GetComponent<CardDisplay>().card.HP;
            //this part should really happen during draw phase but this is fine.
             ArenaManager.totalJoeyPoints += playerBattleStation.GetChild(i).GetComponent<CardDisplay>().card.points;
        }

        for (int i = 0; i < enemyBattleStation.childCount; i++)
        {
            ArenaManager.totalEnemyATK += enemyBattleStation.GetChild(i).GetComponent<CardDisplay>().card.ATK;
            ArenaManager.totalEnemyHP += enemyBattleStation.GetChild(i).GetComponent<CardDisplay>().card.HP;
            ArenaManager.totalNickPoints += enemyBattleStation.GetChild(i).GetComponent<CardDisplay>().card.points;
        }

        //BuffPhase should be next. But for now lets just attack.
        BattlePhase();
    }
    void SummonCards()
    {
        //I'll need to pass through a list of chose cards here.

        GameObject PlayerGO = Instantiate(playerPrefab, playerBattleStation);

        GameObject EnemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        
        Debug.Log("Battle is set up.");

        //WaitForSeconds(2f);
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
        ArenaManager.totalEnemyHP = ArenaManager.totalEnemyHP = ArenaManager.totalPlayerATK;
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
