using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(0)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static GameState GameState { get; set; }

    public int HeartStreakCounter = 0;
    public int HeartStreakDuration = 20;
    public int HeartStreakTimer = 0;


    #region Scoring

    public int TotalLevelScore = 0;
    public int TotalGlobalScore = 0;
    public int TotalRosas = 0;

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

    #region Freeze
    public bool Frozen = false;
    private float CurrentSpeed = 0;
    #endregion 
    private bool EndUIShown = false;
    public Animator TransitionAnimator;
    public GameObject GameUI;
    public GameObject EndTransitionInstance;
    private int EndDelay = 100;
    private int EndDelayCounter = 0;
    public int Volunteers = 0;
    public bool Times3Unlocked = false;
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

        if (Canvas != null)
        {
            Instantiate(MainTransition, Canvas.transform);
        }
    }

    public void Freeze()
    {
        CurrentSpeed = Speed;
        Speed = 0f;
        Frozen = true;
    }

    public void Unfreeze()
    {
        Speed = CurrentSpeed;
        Frozen = false;
    }

    public static void SetStart()
    {
        ScoreManager.ResetScore();
        GameManager.Instance.EndTransitionInstance = Instantiate(Instance.EndTransition, Instance.Canvas.transform);
        GameManager.Instance.TransitionAnimator = GameManager.Instance.EndTransitionInstance.GetComponent<Animator>();
        GameManager.Instance.TransitionAnimator.SetBool("IsTransitioning", true);
    }

    public static void IncreaseSpeed()
    {
        GameManager.Instance.Speed += 0.01f;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.Frozen)
        {
            return;
        }

        if (HeartStreakTimer > 0)
        {
            HeartStreakTimer--;
        }
        else
        {
            HeartStreakCounter = 0;
        }

        if (GameState == GameState.End && ScoreManager.ScoreValue <= 0 && EndDelayCounter < EndDelay)
        {
            EndDelayCounter++;
        }

        if (GameState == GameState.End && ScoreManager.ScoreValue <= 0 && !EndUIShown && EndDelayCounter >= EndDelay)
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

    public static void SetShop()
    {
        SceneManager.LoadScene("Shop");
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
        ScoreManager.ScoreValue = GameManager.Instance.Volunteers;
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
