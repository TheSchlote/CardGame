using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CI.QuickSave;

public class PlayerInfo : MonoBehaviour
{
    public static string playerName;
    public static Sprite playerIcon;
    public static int playerLevel;
    public static int playerXP;
    public static int playerXPToNextLevel;
    public static readonly int[] experienceNeededToLevelUp = new[] { 50, 100, 150, 200, 250, 300 };

    //This will need to be in its own scrip but it can live here for now.
    public Deck myDeck;
    public static Dictionary<Card, int> playerCardInventory = new Dictionary<Card, int>();

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
                //Debug.Log("Max Level Reached!");
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
        //Debug.Log(playerXPToNextLevel);
    }

    public void AddStartingCards()
    {
        //Testing I want to add five of each card.
        foreach (Card card in myDeck.cards)
        {
            playerCardInventory.Add(card, 5);
        }
    }

    public void SaveGameData()
    {
        //Since I can't save ScriptableObjects using this, I make fake data with strings instead of Cards
        Dictionary<string, int> playerCardInventoryFake = new Dictionary<string, int>();
        List<string> deckFakeList = new List<string>();

        //Then I fill the fake data
        foreach (KeyValuePair<Card, int> card in playerCardInventory)
        {
            playerCardInventoryFake.Add(card.Key.name, card.Value);
        }

        foreach (KeyValuePair<int, Card> card in Deck.deck)
        {
            deckFakeList.Add(card.Value.name);
        }

        //Save Everything
        QuickSaveWriter.Create("SaveEverything")
                           .Write("PlayerName", playerName)
                           .Write("PlayerIcon", playerIcon)
                           .Write("PlayerLevel", playerLevel)
                           .Write("PlayerXP", playerXP)
                           .Write("PlayerCardInventory", playerCardInventoryFake)
                           .Write("PlayerDeck", deckFakeList)
                           .Commit();
        //Debug.Log("Saved!");
    }

    public void SaveNewGameData()
    {
        Dictionary<string, int> playerCardInventoryFake = new Dictionary<string, int>();
        List<string> deckFakeList = new List<string>();

        //Save Everything as blank
        QuickSaveWriter.Create("SaveEverything")
                           .Write("PlayerName", "")
                           .Write("PlayerIcon", "")
                           .Write("PlayerLevel", 0)
                           .Write("PlayerXP", 0)
                           .Write("PlayerCardInventory", playerCardInventoryFake)
                           .Write("PlayerDeck", deckFakeList)
                           .Commit();
    }

    public void LoadGameData()
    {
        Dictionary<string, int> playerCardInventoryFake = new Dictionary<string, int>();
        List<string> deckFakeList = new List<string>();

        QuickSaveReader.Create("SaveEverything")
                       .Read<string>("PlayerName", (r) => { playerName = r; })
                       .Read<Sprite>("PlayerIcon", (r) => { playerIcon = r; })
                       .Read<int>("PlayerLevel", (r) => { playerLevel = r; })
                       .Read<int>("PlayerXP", (r) => { playerXP = r; })
                       .Read<Dictionary<string, int>>("PlayerCardInventory", (r) => { playerCardInventoryFake = r; })
                       .Read<List<string>>("PlayerDeck", (r) => { deckFakeList = r; });

        //Since I can't save ScriptableObjects using this, I made fake data with strings instead of Cards
        //So i fix that here
        foreach (KeyValuePair<string, int> card in playerCardInventoryFake)
        {

            playerCardInventory.Add(myDeck.cards.Find(x => x.name == card.Key), card.Value);
        }
        //Reassigning Deck index numbers just makes everything easier.
        int i = 0;
        foreach (string card in deckFakeList)
        {
            Deck.deck.Add(i, myDeck.cards.Find(x => x.name == card));
            i++;
        }
    }
}
