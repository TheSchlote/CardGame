﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMangaer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
