using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    public static int ScoreValue { get; set; }

    public static void AddScore(int score)
    {
        ScoreValue += score;
    }

    public static void ResetScore()
    {
        ScoreValue = 0;
    }
}
