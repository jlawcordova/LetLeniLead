using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static GameState GameState { get; set; }

    public int HeartStreakCounter = 0;
    public int HeartStreakDuration = 10;
    public int HeartStreakTimer = 0;


    #region Scoring

    public int TotalLevelScore = 0;
    public int TotalGlobalScore = 0;

    #endregion

    private int LevelScore = 0;
    public int LevelCompetionScore = 50;

    public GameObject Canvas;
    public GameObject GameUIObject;
    public GameObject Confetti;
    public GameObject EndUIObject;
    public GameObject StartUIObject;
    public GameObject VolunteerGenerator;
    public GameObject MainTransition;
    public GameObject EndTransition;
    public float Speed = -0.1f;
    private bool EndUIShown = false;
    public Animator TransitionAnimator;
    public GameObject GameUI;
    public GameObject EndTransitionInstance;

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

    void Start()
    {
        SaveSerial.LoadGame();

        Speed = LevelManager.Instance.Speed;
        LevelCompetionScore = LevelManager.Instance.CompletionScore;

        Instantiate(MainTransition, Canvas.transform);
    }

    public static void SetStart()
    {
        ScoreManager.ResetScore();
        GameManager.Instance.EndTransitionInstance = Instantiate(Instance.EndTransition, Instance.Canvas.transform);
        GameManager.Instance.TransitionAnimator = GameManager.Instance.EndTransitionInstance.GetComponent<Animator>();
        GameManager.Instance.TransitionAnimator.SetBool("IsTransitioning", true);
    }

    void FixedUpdate()
    {
        if (HeartStreakTimer > 0)
        {
            HeartStreakTimer--;
        }
        else
        {
            HeartStreakCounter = 0;
        }

        if (GameState == GameState.End && ScoreManager.ScoreValue <= 0 && !EndUIShown)
        {
            ShowEndUI();
            EndUIShown = true;
        }

        if(GameManager.Instance.TransitionAnimator != null && GameManager.Instance.TransitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            SceneManager.LoadScene("Loading");
        }
    }

    public static void SetGame()
    {
        GameState = GameState.Game;
        ShowGameUI();
    }

    public static void SetEnd()
    {
        GameState = GameState.End;
        Instance.Speed = 0f;
        GameManager.Instance.LevelScore = ScoreManager.ScoreValue;
        ShowVolunteerGenerator(); 
        ShowConfetti();
    }

    private static void SetProgressBar()
    {
        LevelProgressBar.Instance.PreviousScore = GameManager.Instance.TotalLevelScore;
        LevelProgressBar.Instance.CurrentScore = GameManager.Instance.TotalLevelScore + GameManager.Instance.LevelScore;
        LevelProgressBar.Instance.CompletionScore = GameManager.Instance.LevelCompetionScore;
    }

    private static void ShowConfetti()
    {
        Instantiate(Instance.Confetti);
    }

    private static void ShowVolunteerGenerator()
    {
        Instantiate(Instance.VolunteerGenerator);
    }

    private static void ShowStartUI()
    {
        Instantiate(Instance.StartUIObject, Instance.Canvas.transform);
    }

    private static void ShowGameUI()
    {
        StartUI.Destroy();
        GameManager.Instance.GameUI = Instantiate(Instance.GameUIObject, Instance.Canvas.transform);
    }

    private static void ShowEndUI()
    {
        Instantiate(Instance.EndUIObject, Instance.Canvas.transform);
        Destroy(GameManager.Instance.GameUI);
        SetProgressBar();

        SaveScore();
    }

    private static void SaveScore()
    {
        Instance.TotalGlobalScore += GameManager.Instance.LevelScore;
        Instance.TotalLevelScore += GameManager.Instance.LevelScore;

        if (Instance.TotalLevelScore >= Instance.LevelCompetionScore)
        {
            LevelManager.Instance.Level++;
            Instance.TotalLevelScore = 0;
        }

        SaveSerial.SaveGame();
    }
}

public enum GameState
{
    Start,
    Game,
    End
}
