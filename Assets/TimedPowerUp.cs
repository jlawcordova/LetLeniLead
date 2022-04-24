using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPowerUp : MonoBehaviour
{
    public int Duration = 600;
    private int DurationCounter = 0;
    private bool Exited = false;
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DurationCounter++;

        if ((DurationCounter > Duration || GameManager.GameState != GameState.Game) && !Exited)
        {
            animator.SetTrigger("Exit");
            AudioManager.PlayNormalMusic();
            Exited = true;
        }
    }
}
