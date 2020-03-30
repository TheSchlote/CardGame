using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUp : MonoBehaviour
{
    private Button btnJoey;
    private Button btnNick;
    private Button btnLogan;
    private Button btnJordan;
    private Button btnRobert;
    private InputField infName;
    private TouchScreenKeyboard keyboard;

    public PlayerInfo player;

    // Start is called before the first frame update
    void Start()
    {
        btnJoey = GameObject.Find("btnJoey").GetComponent<Button>();
        btnNick = GameObject.Find("btnNick").GetComponent<Button>();
        btnLogan = GameObject.Find("btnLogan").GetComponent<Button>();
        btnJordan = GameObject.Find("btnJordan").GetComponent<Button>();
        btnRobert = GameObject.Find("btnRobert").GetComponent<Button>();
        infName = GameObject.Find("InputField").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
    public void JoeyClicked()
    {
        PlayerInfo.playerIcon = btnJoey.GetComponent<Image>().sprite;
    }
    public void NickClicked()
    {
        PlayerInfo.playerIcon = btnNick.GetComponent<Image>().sprite;
    }
    public void LoganClicked()
    {
        PlayerInfo.playerIcon = btnLogan.GetComponent<Image>().sprite;
    }
    public void JordanClicked()
    {
        PlayerInfo.playerIcon = btnJordan.GetComponent<Image>().sprite;
    }
    public void RobertClicked()
    {
        PlayerInfo.playerIcon = btnRobert.GetComponent<Image>().sprite;
    }


    public void StartGame()
    {
        player.AddStartingCards();
        PlayerInfo.playerName = infName.text;
        SceneManager.LoadScene("Main");
    }
}
