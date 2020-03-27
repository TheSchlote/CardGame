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

    public Text txttotalNickPoints;
    public Text txttotalJoeyPoints;

    public Toggle player1st;
    public Toggle player2nd;
    public Toggle enemy1st;
    public Toggle enemy2nd;

    Card playerUnit;
    Card enemyUnit;

    public static int totalPlayerATK = 0;
    public static int totalPlayerHP = 1;
    public static int totalEnemyATK = 0;
    public static int totalEnemyHP = 0;

    public static bool player1stWin = false;
    public static bool player2ndWin = false;
    public static bool enemy1stWin = false;
    public static bool enemy2ndWin = false;

    public static int totalNickPoints = 0;
    public static int totalJoeyPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        //txttotalPlayerATK.text = "0";
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

        txttotalNickPoints.text = totalNickPoints.ToString("D2");
        txttotalJoeyPoints.text = totalJoeyPoints.ToString("D2");
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

        totalNickPoints = 0;
        totalJoeyPoints = 0;
    }
    public void ResetArenaPoints()
    {
        totalPlayerATK = 0;
        totalPlayerHP = 1;
        totalEnemyATK = 0;
        totalEnemyHP = 0;
    }
}
