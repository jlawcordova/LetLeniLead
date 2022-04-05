using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private const int LevelTypeCount = 1;
    public LevelType LevelType
    {
        get
        {
            return (LevelType)(Level%LevelTypeCount);
        }
    }

    public float Speed
    {
        get
        {
            return 0.1f * (1 + (Level * 0.075f));
        }
    }

    public int CompletionScore
    {
        get
        {
            return (75 * Level) - 25;
        }
    }

    public int Level = 1;
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
}

public enum LevelType
{
    Naga
}
