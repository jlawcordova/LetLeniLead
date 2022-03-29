using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static GameState GameState { get; set; }
    public GameObject Canvas;
    public GameObject GameUIObject;
    public GameObject EndUIObject;
    public GameObject StartUIObject;

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            GameState = GameState.Start;
            Instance = this; 
        } 
    }

    public static void SetStart()
    {
        ScoreManager.ResetScore();
        SceneManager.LoadScene("Main");
    }

    public static void SetGame()
    {
        GameState = GameState.Game;
        ShowGameUI();
    }

    public static void SetEnd()
    {
        GameState = GameState.End;
        ShowEndUI();
    }

    private static void ShowStartUI()
    {
        Instantiate(Instance.StartUIObject, Instance.Canvas.transform);
    }

    private static void ShowGameUI()
    {
        StartUI.Destroy();
        Instantiate(Instance.GameUIObject, Instance.Canvas.transform);
    }

    private static void ShowEndUI()
    {
        Instantiate(Instance.EndUIObject, Instance.Canvas.transform);
    }
}

public enum GameState
{
    Start,
    Game,
    End
}
