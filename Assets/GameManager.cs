using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static GameState GameState { get; set; }

    #region Scoring

    public int TotalGlobalScore = 0;
    public int GlobalScore = 0;

    #endregion

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
        ShowVolunteerGenerator(); 
        ShowConfetti();
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
