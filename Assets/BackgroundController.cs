using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float StartXPosition = 21.5f;
    public float EndXPosition = -21.5f;
    public GameObject[] RoadBackground;
    public GameObject[] LocationBackground;
    public float Speed = 0.1f;
    private float maxXPosition;

    async void Start()
    {
        maxXPosition = (LocationBackground.Length - 1) * StartXPosition;

        for(int i = 0; i < LocationBackground.Length; i++)
        {
            var initialXPosition = StartXPosition * i;

            var locationTransform = LocationBackground[i].transform;
            locationTransform.position = new Vector3(initialXPosition, locationTransform.position.y, locationTransform.position.z);
        }
    }

    void FixedUpdate()
    {
        foreach (var roadBackground in RoadBackground)
        {
            var roadTransform = roadBackground.transform;
            roadTransform.position -= new Vector3(Speed, 0, 0);

            if (roadTransform.position.x <= EndXPosition)
            {
                roadTransform.position = new Vector3(StartXPosition, roadTransform.position.y, roadTransform.position.z);
            }
        }

        foreach (var locationBackground in LocationBackground)
        {
            var locationTransform = locationBackground.transform;
            locationTransform.position -= new Vector3(Speed, 0, 0);

            if (locationTransform.position.x <= EndXPosition)
            {
                locationTransform.position = new Vector3(maxXPosition, locationTransform.position.y, locationTransform.position.z);
            }
        }
    }
}
