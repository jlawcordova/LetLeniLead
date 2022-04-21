using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class BackgroundController : MonoBehaviour
{
    public float StartXPosition = 21.5f;
    public float EndXPosition = -21.5f;

    public GameObject Canvas;
    public GameObject LocationBanner;

    #region Location Roller

    public int MaxLocationDuration = 4;
    private int LocationDurationCounter = 0;
    private int CurrentLocation = 0;

    #endregion

    public GameObject[] RoadBackground;
    public List<GameObject> RoadBackgroundInstances = new List<GameObject>();
    public Dictionary<GameObject, bool> RoadBackgroundReplaced = new Dictionary<GameObject, bool>();

    public GameObject TransitionLocationBackground;
    public GameObject[] LocationBackground;
    public List<GameObject> LocationBackgroundInstances = new List<GameObject>();
    public Dictionary<GameObject, bool> LocationBackgroundReplaced = new Dictionary<GameObject, bool>();

    public GameObject TransitionBackdropBackground;
    public GameObject[] BackdropBackground;
    public List<GameObject> BackdropBackgroundInstances = new List<GameObject>();
    public Dictionary<GameObject, bool> BackdropBackgroundReplaced = new Dictionary<GameObject, bool>();
    
    public GameObject[] CloudBackground;
    public List<GameObject> CloudBackgroundInstances = new List<GameObject>();
    public Dictionary<GameObject, bool> CloudBackgroundReplaced = new Dictionary<GameObject, bool>();

    void Start()
    {
        // Initialize road background.
        InstantiateRoad(0);
        InstantiateLocation(0);
        InstantiateBackdrop(0);
        InstantiateCloud(0);
    }

    void FixedUpdate()
    {
        for (int i = 0; i < RoadBackgroundInstances.Count; i++)
        {
            var roadBackgroundInstance = RoadBackgroundInstances[i];

            var roadTransform = roadBackgroundInstance.transform;
            roadTransform.position -= new Vector3(GameManager.Instance.Speed, 0, 0);

            if (roadTransform.position.x <= 0 && !RoadBackgroundReplaced[roadBackgroundInstance])
            {
                InstantiateRoad(StartXPosition);
                RoadBackgroundReplaced[roadBackgroundInstance] = true;
            }

            if (roadTransform.position.x <= EndXPosition)
            {
                RoadBackgroundInstances.Remove(roadBackgroundInstance);
                Destroy(roadBackgroundInstance);
            }
        }

        for (int i = 0; i < LocationBackgroundInstances.Count; i++)
        {
            var locationBackgroundInstance = LocationBackgroundInstances[i];

            var locationTransform = locationBackgroundInstance.transform;
            locationTransform.position -= new Vector3(GameManager.Instance.Speed, 0, 0);

            if (locationTransform.position.x <= 0 && !LocationBackgroundReplaced[locationBackgroundInstance])
            {
                InstantiateLocation(StartXPosition);
                LocationBackgroundReplaced[locationBackgroundInstance] = true;
            }

            if (locationTransform.position.x <= EndXPosition)
            {
                LocationBackgroundInstances.Remove(locationBackgroundInstance);
                Destroy(locationBackgroundInstance);
            }
        }

        for (int i = 0; i < BackdropBackgroundInstances.Count; i++)
        {
            var backdropBackgroundInstance = BackdropBackgroundInstances[i];

            var backdropTransform = backdropBackgroundInstance.transform;
            backdropTransform.position -= new Vector3(GameManager.Instance.Speed * 0.75f, 0, 0);

            if (backdropTransform.position.x <= 0 && !BackdropBackgroundReplaced[backdropBackgroundInstance])
            {
                InstantiateBackdrop(StartXPosition);
                BackdropBackgroundReplaced[backdropBackgroundInstance] = true;
            }

            if (backdropTransform.position.x <= EndXPosition)
            {
                BackdropBackgroundInstances.Remove(backdropBackgroundInstance);
                Destroy(backdropBackgroundInstance);
            }
        }

        for (int i = 0; i < CloudBackgroundInstances.Count; i++)
        {
            var cloudBackgroundInstance = CloudBackgroundInstances[i];

            var cloudTransform = cloudBackgroundInstance.transform;
            cloudTransform.position -= new Vector3(GameManager.Instance.Speed * 0.5f, 0, 0);

            if (cloudTransform.position.x <= 0 && !CloudBackgroundReplaced[cloudBackgroundInstance])
            {
                InstantiateCloud(StartXPosition);
                CloudBackgroundReplaced[cloudBackgroundInstance] = true;
            }

            if (cloudTransform.position.x <= EndXPosition)
            {
                CloudBackgroundInstances.Remove(cloudBackgroundInstance);
                Destroy(cloudBackgroundInstance);
            }
        }
    }

    private void InstantiateRoad(float initialXPosition)
    {
        var roadIndex = Random.Range(0, RoadBackground.Length);
        var roadBackgroundGameObject = RoadBackground[roadIndex];

        var roadBackgroundInstance = Instantiate(roadBackgroundGameObject, 
            new Vector3(
                initialXPosition, roadBackgroundGameObject.transform.position.y, roadBackgroundGameObject.transform.position.z),
            Quaternion.identity);
        RoadBackgroundInstances.Add(roadBackgroundInstance);
        RoadBackgroundReplaced.Add(roadBackgroundInstance, false);
    }

    private void InstantiateLocation(float initialXPosition)
    {
        // Set a transition if location is almost done.
        // Otherwise just show the location.
        GameObject locationBackgroundGameObject;
        if (LocationDurationCounter >= MaxLocationDuration - 1)
        {
            locationBackgroundGameObject = TransitionLocationBackground;
        }
        else
        {
            // Create the current location.
            var locationIndex = CurrentLocation;
            locationBackgroundGameObject = LocationBackground[locationIndex];
        }
        var locationBackgroundInstance = Instantiate(locationBackgroundGameObject, 
            new Vector3(
                initialXPosition, locationBackgroundGameObject.transform.position.y, locationBackgroundGameObject.transform.position.z),
            Quaternion.identity);
        LocationBackgroundInstances.Add(locationBackgroundInstance);
        LocationBackgroundReplaced.Add(locationBackgroundInstance, false);

        // Don't change location if the game hasn't started yet.
        if (GameManager.GameState == GameState.Start)
        {
            return;
        }

        if (LocationDurationCounter == 0)
        {
            ShowLocationBanner();
        }

        // Replace the location after a certain period.
        // While also increasing the difficulty.
        LocationDurationCounter++;
        if (LocationDurationCounter > MaxLocationDuration)
        {
            ReplaceLocation();
            GameManager.IncreaseSpeed();
        }
    }

    private void ShowLocationBanner()
    {
        var locationBanner = LocationBanner.GetComponent<LocationBanner>();
        locationBanner.LevelTypeIndex = CurrentLocation;
        Instantiate(LocationBanner, Canvas.transform);
    }

    private void ReplaceLocation()
    {
        // Look for a location that isn't the current one.
        // Set that as the new location.
        int tempCurrentLocation;
        do 
        {
            tempCurrentLocation = Random.Range(0, LevelManager.Instance.LevelTypeIndex);
        } while (CurrentLocation == tempCurrentLocation);

        CurrentLocation = tempCurrentLocation;
        LocationDurationCounter = 0;
    }

    private void InstantiateBackdrop(float initialXPosition)
    {
        // Set a transition if location is almost done.
        // Otherwise just show the location.
        GameObject backdropBackgroundGameObject;
        if (LocationDurationCounter >= MaxLocationDuration || LocationDurationCounter == 0)
        {
            backdropBackgroundGameObject = TransitionBackdropBackground;
        }
        else
        {
            // Create the current location.
            var backdropIndex = CurrentLocation;
            backdropBackgroundGameObject = BackdropBackground[backdropIndex];
        }

        var backdropBackgroundInstance = Instantiate(backdropBackgroundGameObject, 
            new Vector3(
                initialXPosition, backdropBackgroundGameObject.transform.position.y, backdropBackgroundGameObject.transform.position.z),
            Quaternion.identity);
        BackdropBackgroundInstances.Add(backdropBackgroundInstance);
        BackdropBackgroundReplaced.Add(backdropBackgroundInstance, false);
    }

    private void InstantiateCloud(float initialXPosition)
    {
        var cloudIndex = Random.Range(0, CloudBackground.Length);
        var cloudBackgroundGameObject = CloudBackground[cloudIndex];

        var cloudBackgroundInstance = Instantiate(cloudBackgroundGameObject, 
            new Vector3(
                initialXPosition, cloudBackgroundGameObject.transform.position.y, cloudBackgroundGameObject.transform.position.z),
            Quaternion.identity);
        CloudBackgroundInstances.Add(cloudBackgroundInstance);
        CloudBackgroundReplaced.Add(cloudBackgroundInstance, false);
    }
}
