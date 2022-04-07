using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBursts : MonoBehaviour
{
    private int Counter = 0;
    private int Duration = 100;

    void FixedUpdate()
    {
        if (Counter > Duration)
        {
            Destroy(gameObject);
        }

        Counter++;
    }
}
