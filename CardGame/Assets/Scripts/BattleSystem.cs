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

    Unit playerUnit;
    Unit enemyUnit;

    public DisplayHUD playerHUD;
    public DisplayHUD enemyHUD;

    public BattleState state;

    public bool enemyFirst = false;
    public bool playAbilityCard = false;
    public bool abilityPass = false;

    public bool playBuffCard = false;
    public bool buffPass = false;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        DrawPhase();

        AbilityPhase();
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

    void SummonPhase()
    {
        state = BattleState.SUMMONPHASE;
        Debug.Log("Select Available cards...");
        //When Card that has cost is selected, subtract appropriate points.
        //Confirm

        //Enemy AI plays all available cards.
        //Animation

        //Total up ATK and HP
        BuffPhase();
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

    IEnumerator SetupBattle()
    {
        GameObject PlayerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = PlayerGO.GetComponent<Unit>();

        GameObject EnemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = EnemyGO.GetComponent<Unit>();

        playerHUD.SetHud(playerUnit);
        enemyHUD.SetHud(enemyUnit);

        yield return new WaitForSeconds(2f);
    }

    void BattlePhase()
    {
        state = BattleState.BATTLEPHASE;
        Debug.Log("Battle Phase...");
    }
}
