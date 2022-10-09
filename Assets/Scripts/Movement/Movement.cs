using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    private Animator animator;
    protected GeneralDeath generalDeath;

    public bool IsGrounded { get; private set; }
    public bool IsSliding { get; protected set; }

    protected virtual void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        generalDeath = transform.GetComponent<GeneralDeath>();
    }

    protected virtual void Update()
    {
        animator.SetBool("IsSliding", IsSliding);

        // slime is grounded if on the landing frames of its animation
        float animationProgress = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        IsGrounded = (0f <= animationProgress && animationProgress <= 0.2f) || IsSliding;
    }
}
