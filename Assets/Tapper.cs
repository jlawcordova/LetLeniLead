using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(0)]
public class Tapper : MonoBehaviour
{
    public GameObject Transition;
    private Animator transitionAnimator;

    InputManager touchMover;
    void Awake()
    {
        touchMover = InputManager.Instance;
    }

    void Start()
    {
        transitionAnimator = Transition.GetComponent<Animator>();
    }

    void OnEnable()
    {
        touchMover.OnStartTouch += SetMove;
    }

    void OnDisable()
    {
        touchMover.OnStartTouch -= p => SetMove(p);
    }

    void SetMove(Vector2 startTouchPosition)
    {
        transitionAnimator.SetBool("IsTransitioning", true);
    }

    void FixedUpdate()
    {
        if(this.transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
