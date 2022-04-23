using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position -= new Vector3(GameManager.Instance.Speed, 0, 0);

        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
