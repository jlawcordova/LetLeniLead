using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetroyedOnGameEnd : MonoBehaviour
{
    void FixedUpdate()
    {
        if (GameManager.GameState == GameState.End)
        {
            Destroy(gameObject);
        }
    }
}
