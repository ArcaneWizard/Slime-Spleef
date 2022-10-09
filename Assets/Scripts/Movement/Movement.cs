using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rig;
    protected GeneralDeath generalDeath;
    private Transform sprite;

    public bool IsGrounded { get; private set; }
    public bool IsSliding { get; protected set; }

    private const float normalAnimationSpeed = 1f;
    private const float groundPoundSpeed = 1.4f;

    protected const float slidingSpeed = 2.7f;
    protected const float bouncingSpeed = 2f;

    protected virtual void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        generalDeath = transform.GetComponent<GeneralDeath>();
        rig = transform.GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0);
    }

    protected virtual void Update()
    {
        if (generalDeath.IsDead)
            return;

        animator.speed = (IsSliding && !IsGrounded) ? groundPoundSpeed : normalAnimationSpeed;

        if (!IsSliding)
            animator.SetBool("IsSliding", false);

        if (IsGrounded && IsSliding)
            animator.SetBool("IsSliding", true);

        // slime is grounded if on the landing frames of its animation
        float animationProgress = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        IsGrounded = (0f <= animationProgress && animationProgress <= 0.2f) || animator.GetBool("IsSliding");
    }

    protected void setVelocity(Vector2 dir)
    {
        float speed = animator.GetBool("IsSliding") ? slidingSpeed : bouncingSpeed;
        rig.velocity = dir.normalized * speed * Mathf.Max((sprite.localScale.x / 0.6f), 1);
    }
}
