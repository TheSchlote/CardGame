using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject AreyouSureWindow;
    public PlayerInfo player;
    public void StartOver()
    {
        AreyouSureWindow.SetActive(true);
    }

    public void PlayGame()
    {
        player.LoadGameData();
        SceneManager.LoadScene("Main");
    }

    public void Yes()
    {
        player.SaveNewGameData();
        SceneManager.LoadScene("StartupScene");
    }
    public void No()
    {
        AreyouSureWindow.SetActive(false);
    }

}
