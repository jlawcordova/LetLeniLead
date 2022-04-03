using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    void FixedUpdate()
    {
        if (GameManager.GameState == GameState.End)
        {
            return;
        }

        Move();
        HandleEdge();
    }

    private void Move()
    {
        transform.position -= new Vector3(GameManager.Instance.Speed, 0, 0);
    }

    private void HandleEdge()
    {
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

}
