using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float StartXPosition = 21.5f;
    public float EndXPosition = -21.5f;
    public GameObject[] RoadBackground;
    public float Speed = 0.1f;

    void FixedUpdate()
    {
        foreach (var roadBackground in RoadBackground)
        {
            var roadTransform = roadBackground.transform;
            roadTransform.position -= new Vector3(Speed, 0, 0);

            if (roadTransform.position.x <= EndXPosition)
            {
                roadTransform.position = new Vector2(StartXPosition, roadTransform.position.y);
            }
        }
    }
}
