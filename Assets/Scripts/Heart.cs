using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int Score = 1;
    public float Speed = 20f;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        HandleEdge();
    }

    private void HandleEdge()
    {
        transform.position -= new Vector3(Speed, 0, 0);

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    public int Consume()
    {
        Destroy(gameObject);

        return Score;
    }
}
