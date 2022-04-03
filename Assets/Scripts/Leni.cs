using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

[DefaultExecutionOrder(0)]
public class Leni : MonoBehaviour
{
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

    void Awake()
    {
        touchMover = InputManager.Instance;
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
    void Update()
    {   
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

        HandleHeartCollision();
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

            var heart = hit.collider.gameObject.GetComponent<Heart>();
            var heartValue = heart.Consume();
            ScoreManager.AddScore(heartValue);
            Destroy(hit.collider.gameObject);
        }
    }
}
