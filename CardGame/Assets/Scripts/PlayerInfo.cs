using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static string playerName;
    public static Sprite playerIcon;
    public static int playerLevel;
    public static int playerXP;
    public static int playerXPToNextLevel;
    public static readonly int[] experienceNeededToLevelUp = new[] { 60, 100 };
    // Start is called before the first frame update
    void Start()
    {
        playerXPToNextLevel = experienceNeededToLevelUp[playerLevel];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddExperience(int amount)
    {
        playerXP += amount;
        if (playerXP >= playerXPToNextLevel)
        {
            if (playerLevel >= experienceNeededToLevelUp.Length - 1)
            {
                Debug.Log("Max Level Reached!");
            }
            else
            {
                //Enough XP to level up
                playerLevel++;
                playerXP -= playerXPToNextLevel;
                //Time for new level up requirements
                playerXPToNextLevel = experienceNeededToLevelUp[playerLevel];
            }
        }
        Debug.Log(playerXPToNextLevel);
    }
}
