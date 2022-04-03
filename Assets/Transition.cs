using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            Destroy(gameObject);
        }
    }
}
