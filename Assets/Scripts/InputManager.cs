using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public delegate void StartTouchEvent(Vector2 startTouchPosition);
    public event StartTouchEvent OnStartTouch;
    
    public delegate void CancelTouchEvent();
    public event CancelTouchEvent OnCancelTouch;

    public static InputManager Instance;
    public LeniAction leniAction;
    public float YTargetPosition;

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
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        leniAction.Disable();
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        leniAction.Touch.TouchPress.started += ctx => StartTouch(ctx);
        leniAction.Touch.TouchPress.canceled += ctx => CancelTouch(ctx);
    }

    public void StartTouch(InputAction.CallbackContext ctx)
    {
        if(OnStartTouch != null)
        {
            OnStartTouch(leniAction.Touch.TouchPosition.ReadValue<Vector2>());
        }
    }

    public void CancelTouch(InputAction.CallbackContext ctx)
    {
        if(OnCancelTouch != null)
        {
            OnCancelTouch();
        }
    }

    public void Update()
    {
        foreach(var finger in UnityEngine.InputSystem.EnhancedTouch.Touch.activeFingers)
        {
            if(finger.index == 0)
            {
                YTargetPosition = finger.screenPosition.y;
            }
        }
    }
}
