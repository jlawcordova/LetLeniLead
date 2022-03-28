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
        Debug.Log("TouchStartPosition" + TouchStartPosition);

        LeniStartPosition = transform.position;
        Debug.Log("LeniStartPosition" + LeniStartPosition);
    }

    void StopMove()
    {
        IsMoving = false;
    }

    void Move(float yPosition)
    {
        var touchPosition = new Vector2(0, yPosition);
        var targetPosition = MainCamera.GetComponent<Camera>().ScreenToWorldPoint(touchPosition);
        
        var touchDeltaY = TouchStartPosition.y - targetPosition.y;
        var worldY = LeniStartPosition.y - touchDeltaY;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(worldY, MinY, MaxY), transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameState != GameState.Game)
        {
            return;
        }

        if (IsMoving)
        {
            Move(touchMover.YTargetPosition);
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
