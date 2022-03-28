using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public float Speed = 0.1f;

    void FixedUpdate()
    {
        if (GameManager.GameState != GameState.Game)
        {
            return;
        }

        Move();
        HandleEdge();
    }

    private void Move()
    {
        transform.position -= new Vector3(Speed, 0, 0);
    }

    private void HandleEdge()
    {
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

}
