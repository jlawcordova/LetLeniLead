using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    public static LevelProgressBar Instance { get; private set; }
    public float Rate = 10f;
    public float Counter = 0f;
    public GameObject Fill;
    public int PreviousScore = 0; 
    public GameObject PreviousScoreGameObject;
    public int CurrentScore = 100; 
    public int CompletionScore = 1000;
    public GameObject CompletionScoreGameObject;
    public GameObject NextLevelGameObject;
    private Text PreviousScoreText;
    private Text CompletionScoreText;
    private Text NextLevelText;
    private const int Resolution = 20;
    private int Step = 0;
    private int UIStep = 56;
    private HashSet<int> ProgressUITracker = new HashSet<int>();
    public AudioClip ProgressSound;
    public AudioClip LevelupSound;
    private bool ProgressComplete = false;
    private int Incrementor = 1;

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        Step = CompletionScore / Resolution;

        var filled = PreviousScore / Step;
        for (int i = 0; i < filled; i++)
        {
            UpdateUI(i);
            ProgressUITracker.Add(i);
        }

        PreviousScoreText = PreviousScoreGameObject.GetComponent<Text>();
        CompletionScoreText = CompletionScoreGameObject.GetComponent<Text>();
        NextLevelText = NextLevelGameObject.GetComponent<Text>();

        PreviousScoreText.text = PreviousScore.ToString();
        CompletionScoreText.text = CompletionScore.ToString();
    }

    void Update()
    {
        if (PreviousScore < CurrentScore)
        {
            if (Counter >= Rate)
            {
                PreviousScore = Mathf.Clamp(PreviousScore + Incrementor, 0, CurrentScore) ;
                Incrementor++;

                if (PreviousScore <= CompletionScore)
                {
                    PreviousScoreText.text = PreviousScore.ToString();
                    AudioManager.Play("Progress", ProgressSound, 1.2f, false, 0.45f);
                }

                if (PreviousScore >= CompletionScore && !ProgressComplete)
                {
                    PreviousScore = Mathf.Clamp(PreviousScore, 0, CompletionScore) ;
                    PreviousScoreText.text = PreviousScore.ToString();
                    AudioManager.Play("LevelUp", LevelupSound, 1.4f, false, 0.8f);
                    NextLevelText.text = "Naunlock mo na ang next location!";
                    ProgressComplete = true;
                    return;
                }

                var filled = PreviousScore / Step;
                UpdateUI(filled - 1);
                Counter = 0;
            }
            Counter++;
        }
    }

    private void UpdateUI(int filled)
    {
        // Do not exceed.
        if (filled > Resolution - 1 || filled < 0)
        {
            return;
        }

        if(!ProgressUITracker.Contains(filled))
        {
            var fill = Instantiate(Fill, gameObject.transform);
            fill.transform.localPosition = new Vector3(
                (UIStep * filled) - (UIStep * 10), 0, 0
            );
            ProgressUITracker.Add(filled);
        }
    }
}
