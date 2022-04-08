using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volunteers : MonoBehaviour
{
    public float EndXPosition = 23f;
    public int ScoreValue = 1;
    public float Speed = 0.01f;
    public bool Bottom = false;


    void Update()
    {
        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, EndXPosition, Speed),
            transform.position.y,
            transform.position.z
        );
    }
}
