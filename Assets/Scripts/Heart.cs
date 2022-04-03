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
    public HeartType Type = HeartType.Adder;

    public int Value = 1;

    void FixedUpdate()
    {
        HandleEdge();
    }

    private void HandleEdge()
    {
        transform.position -= new Vector3(GameManager.Instance.Speed, 0, 0);

        if (transform.position.x < -15f)
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
