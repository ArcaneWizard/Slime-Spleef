using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHandler : MonoBehaviour
{
    private Animator animator;

    public bool IsGrounded { get; private set; }

    void Awake()
    {
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        float animationProgress = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        Debug.Log(animationProgress);
        IsGrounded = (0f <= animationProgress && animationProgress <= 0.2f);
    }
}
