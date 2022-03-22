using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeartType
{
    Adder,
    Multiplier
}

public class Heart : MonoBehaviour
{
    public int Score = 1;
    public float Speed = 20f;

    public HeartType Type;

    public int Value;

    void Start()
    {
        // TODO: Handle heart value rarity.
        Type = (HeartType)Random.Range(0, 2) ;
        Value = Random.Range(1, 3) ;
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

    public Heart Consume()
    {
        Destroy(gameObject);

        return this;
    }
}
