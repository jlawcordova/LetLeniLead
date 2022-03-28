using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    public static EndUI Instance { get; private set; }
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

    public static void Destroy()
    {
        Destroy(Instance.gameObject);
    }
}
