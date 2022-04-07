using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBackgroundTile : MonoBehaviour
{
    public float Speed = 0.01f;

    // Update is called once per frame
    void Update()
    {
        var y = Mathf.MoveTowards(transform.position.y, 9, Speed);

        if (y >= 9f)
        {
            y = -9;
        }

        transform.position = new Vector3(transform.position.x, y, 0);
    }
}
