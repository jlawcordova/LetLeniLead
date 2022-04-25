using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

[DefaultExecutionOrder(0)]
public class Leni : MonoBehaviour
{
    public static Leni Instance { get; private set; }
    InputManager touchMover;
    public GameObject MainCamera;
    public bool IsMoving = false;
    public Vector3 LeniStartPosition;
    public Vector3 TouchStartPosition;
    public float Speed = 0.1f;
    public float EndXPosition = 23f;
    public float EndYPosition = -1f;
    public float EndSpeed = 0.1f;
    public float MinY = -4.5f;
    public float MaxY = 4.5f;
    public GameObject HeartBurst;

    #region Energy
    public static int MaxEnergy = 3;
    public int Energy = 0;
    public int EnergyDuration = 10000;
    public int EnergyDurationCounter = 0;
    #endregion

    void Awake()
    {
        touchMover = InputManager.Instance;

        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void OnEnable()
    {
        touchMover.OnStartTouch += SetMove;
        touchMover.OnCancelTouch += StopMove;
    }

    void OnDisable()
    {
        touchMover.OnStartTouch -= p => SetMove(p);
        touchMover.OnCancelTouch -= StopMove;
    }

    void Start()
    {
        Energy = MaxEnergy;
    }

    void SetMove(Vector2 startTouchPosition)
    {
        IsMoving = true;

        var touchPosition = new Vector2(0, startTouchPosition.y);
        TouchStartPosition = MainCamera.GetComponent<Camera>().ScreenToWorldPoint(touchPosition);
        TouchStartPosition.x = transform.position.x;
        TouchStartPosition.z = 0;

        LeniStartPosition = transform.position;
    }

    void StopMove()
    {
        IsMoving = false;
    }

    void MoveY(float yPosition)
    {
        var touchPosition = new Vector2(0, yPosition);
        var targetPosition = MainCamera.GetComponent<Camera>().ScreenToWorldPoint(touchPosition);
        
        var touchDeltaY = TouchStartPosition.y - targetPosition.y;
        var worldY = Mathf.MoveTowards(transform.position.y, LeniStartPosition.y - touchDeltaY, Speed);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(worldY, MinY, MaxY), transform.position.z);
    }


    // Update is called once per frame
    void FixedUpdate()
    {   
        if (GameManager.Instance.Frozen)
        {
            return;
        }

        if (GameManager.GameState == GameState.End)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, EndXPosition, EndSpeed),
                Mathf.MoveTowards(transform.position.y, EndYPosition, EndSpeed),
                transform.position.z
            );
        }

        if (GameManager.GameState != GameState.Game)
        {
            return;
        }

        if (IsMoving)
        {
            MoveY(touchMover.YTargetPosition);
        }

        HandleEnergyDepletion();
        HandleHeartCollision();
    }

    private void HandleEnergyDepletion()
    {
        EnergyDurationCounter++;

        if (EnergyDurationCounter >= EnergyDuration)
        {
            RemoveEnergy();
            EnergyDurationCounter = 0;
            if (Energy <= 0)
            {
                GameManager.SetEnd();
            }
        }
    }

    private void RemoveEnergy()
    {
        Energy--;
        EnergyBar.Instance.SetEnergy(Energy);
    }

    public void AddEnergy()
    {
        Instance.Energy = Mathf.Clamp(Instance.Energy + 1, 0, MaxEnergy);
        Instance.EnergyDurationCounter = 0;
        EnergyBar.Instance.SetEnergy(Instance.Energy);
    }


    void HandleHeartCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Finish")
            {
                GameManager.SetEnd();
                return;
            }

            if (hit.collider.gameObject.tag == "PowerUp")
            {
                var powerUp = hit.collider.gameObject.GetComponent<PowerUp>();
                powerUp.Consume();
                return;
            }

            var heart = hit.collider.gameObject.GetComponent<Heart>();
            var heartValue = heart.Consume();
            if (heartValue.Style == HeartStyle.Heart)
            {
                var heartBurst = Instantiate(HeartBurst, new Vector3(transform.position.x, transform.position.y, -4), HeartBurst.transform.rotation);
                ScoreManager.AddScore(heartValue);
            } else if (heartValue.Style == HeartStyle.Rosas)
            {
                GameManager.Instance.TotalRosas++;
            } else if (heartValue.Style == HeartStyle.Energy)
            {
                AddEnergy();
            }
            Destroy(hit.collider.gameObject);
        }
    }
}
