using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private const int LevelTypeCount = 7;
    public LevelType LevelType
    {
        get
        {
            return (LevelType)(Mathf.Clamp(Level, 0, LevelTypeCount));
        }
    }

    public int LevelTypeIndex
    {
        get
        {
            var index = (int)LevelType - 1;
            if (index < 0)
            {
                index = LevelTypeCount - 1;
            }

            return index;
        }
    }

    public float Speed
    {
        get
        {
            var level = 6;
            return Mathf.Clamp(0.1f * (1 + ((level) * 0.1f)), 0.1f, 0.35f);
        }
    }

    public int CompletionScore
    {
        get
        {
            return Mathf.FloorToInt((100f * (Level * Level)) - (150f * Level) + 100f);
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
    Naga,
    Pasig,
    Bohol,
    Davao,
    Samar,
    Cebu,
    Surigao
}
