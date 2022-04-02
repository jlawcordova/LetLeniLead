using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float StartXPosition = 21.5f;
    public float EndXPosition = -21.5f;
    public GameObject[] RoadBackground;
    public GameObject[] LocationBackground;
    public GameObject[] LocationBackdropBackground;
    public GameObject[] CloudBackground;
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

        for(int i = 0; i < LocationBackdropBackground.Length; i++)
        {
            var initialXPosition = StartXPosition * i;

            var locationBackdropTransform = LocationBackdropBackground[i].transform;
            locationBackdropTransform.position = new Vector3(initialXPosition, locationBackdropTransform.position.y, locationBackdropTransform.position.z);
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

        foreach (var locationBackdropBackground in LocationBackdropBackground)
        {
            var locationBackdropTransform = locationBackdropBackground.transform;
            locationBackdropTransform.position -= new Vector3(Speed * 0.75f, 0, 0);

            if (locationBackdropTransform.position.x <= EndXPosition)
            {
                locationBackdropTransform.position = new Vector3(maxXPosition, locationBackdropTransform.position.y, locationBackdropTransform.position.z);
            }
        }

        foreach (var cloudBackground in CloudBackground)
        {
            var cloudTransform = cloudBackground.transform;
            cloudTransform.position -= new Vector3(Speed * 0.5f, 0, 0);

            if (cloudTransform.position.x <= EndXPosition)
            {
                cloudTransform.position = new Vector3(maxXPosition, cloudTransform.position.y, cloudTransform.position.z);
            }
        }
    }
}
