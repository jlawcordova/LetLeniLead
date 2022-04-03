using UnityEngine;

public static class ScoreManager
{
    public static int ScoreValue { get; set; }

    public static void AddScore(Heart heart)
    {
        switch (heart.Type)
        {
            case HeartType.Adder:
                ScoreValue += heart.Value;
                break;
            case HeartType.Multiplier:
                ScoreValue *= heart.Value;
                break;
            default:
                throw new System.Exception();
        }
    }
    public static void DeductScore(int deduction)
    {
        var temp = ScoreValue;
        ScoreValue = Mathf.Clamp(ScoreValue - deduction, 0, temp);
    }

    public static void ResetScore()
    {
        ScoreValue = 0;
    }
}
