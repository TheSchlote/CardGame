using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    public Text playerNameDisplay;
    public Text playerLevelDisplay;
    public Text playerXPToNextLevelDisplay;
    public Button playerIconDisplay;
    public Slider playerXPBar;

    public PlayerInfo player;

    // Start is called before the first frame update
    void Start()
    {
        player.SaveGameData();
    }

    // Update is called once per frame
    void Update()
    {
        playerNameDisplay.text = PlayerInfo.playerName;
        playerLevelDisplay.text = "Level: " + PlayerInfo.playerLevel.ToString();
        if (PlayerInfo.playerLevel >= PlayerInfo.experienceNeededToLevelUp.Length - 1)
        {
            playerXPToNextLevelDisplay.text = "MAX LEVEL";
            //Give them the satisfaction of a full bar.
            playerXPBar.maxValue = 1;
            playerXPBar.value = 1;
        }
        else
        {
            playerXPToNextLevelDisplay.text = PlayerInfo.playerXP.ToString() + "/" + PlayerInfo.playerXPToNextLevel + " XP To Next Level";
            playerXPBar.maxValue = PlayerInfo.playerXPToNextLevel;
            playerXPBar.value = PlayerInfo.playerXP;
        }
        playerIconDisplay.GetComponent<Image>().sprite = PlayerInfo.playerIcon;
    }
}
