using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class TouchMover : MonoBehaviour
{
    public delegate void StartTouchEvent(Vector2 position);
    public event StartTouchEvent OnStartTouch;

    public static TouchMover Instance;
    public LeniAction leniAction;
    public Camera camera;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        leniAction = new LeniAction();
    }

    void OnEnable()
    {
        leniAction.Enable();
    }

    void OnDisable()
    {
        leniAction.Disable();
    }

    void Start()
    {
        leniAction.Touch.TouchPress.started += ctx => StartTouch(ctx);
    }

    public void StartTouch(InputAction.CallbackContext ctx)
    {
        if(OnStartTouch != null)
        {
            OnStartTouch(leniAction.Touch.TouchPosition.ReadValue<Vector2>());
        }
        // var touchPosition = leniAction.Touch.TouchPosition.ReadValue<Vector2>();
        // var cameraPosition = camera.ScreenToWorldPoint(touchPosition);
        // Debug.Log("touchPosition" + touchPosition);
        // Debug.Log("cameraPosition" + cameraPosition);
        // Debug.Log("Touched!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 )
        {
            Debug.Log("Input.touchCount " + Input.touchCount);
        }
    }
}
