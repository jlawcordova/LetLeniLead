using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private const int LevelTypeCount = 3;
    public LevelType LevelType
    {
        get
        {
            return (LevelType)(Level%LevelTypeCount);
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
            return Mathf.Clamp(0.1f * (1 + (Level * 0.075f)), 0.1f, 0.35f);
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
    Naga,
    Pasig,
    Bohol
}
