using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMangaer : MonoBehaviour
{
    public void StartBattle()
    {
        SceneManager.LoadScene("TestBattleScene");
    }

    public void StartDeckBuilder()
    {
        SceneManager.LoadScene("DeckBuilder");
    }

    public void StartMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void StartShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
