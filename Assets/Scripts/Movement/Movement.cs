 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rig;
    protected GeneralDeath generalDeath;
    private Transform body;
    private LeavePuddle puddleLeaver;
    private Energy energy;

    private HashSet<Transform> collidedPuddles;

    public bool IsGrounded { get; private set; }
    public bool IsSliding { get; protected set; }

    public float Speed { get; private set; }
    public Vector2 MovementDir { get; protected set; }

    private const float normalAnimationSpeed = 1.1f;
    private const float groundPoundSpeed = 1.4f;

    protected const float slidingSpeed = 2.7f;
    protected const float bouncingSpeed = 2f;
    protected float puddleSlowFactor = 1f;

    protected virtual void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        generalDeath = transform.GetComponent<GeneralDeath>();
        rig = transform.GetComponent<Rigidbody2D>();
        body = transform.GetChild(0);
        puddleLeaver = transform.GetChild(0).GetComponent<LeavePuddle>();
        energy = transform.GetComponent<Energy>();

        collidedPuddles = new HashSet<Transform>();

        MovementDir = Vector2.zero;
    }

    protected virtual void Update()
    {
        if (generalDeath.IsDead)
        {
            animator.SetBool("IsSliding", true);
            rig.velocity = Vector2.zero;
            return;
        }

        animator.speed = (IsSliding && !IsGrounded) ? groundPoundSpeed : normalAnimationSpeed;

        if (!IsSliding)
            animator.SetBool("IsSliding", false);

        if (IsGrounded && IsSliding)
            animator.SetBool("IsSliding", true);

        // slime is grounded if on the landing frames of its animation
        float animationProgress = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        IsGrounded = (0f <= animationProgress && animationProgress <= 0.2f) || animator.GetBool("IsSliding");

        // set slime velocity
        Speed = animator.GetBool("IsSliding") ? slidingSpeed : bouncingSpeed;
        Speed *= puddleSlowFactor;
        rig.velocity = MovementDir.normalized * Speed * Mathf.Max((body.localScale.x / 0.6f), 1);

        updateSpeedAndEnergy();
    }

    public void ClearCollidedPuddles()
    {
        collidedPuddles.Clear();
    }

    private float sumCollidedPuddlesSize()
    {
        float sumSize = 0f;

        foreach (Transform puddle in collidedPuddles)
        {
            if (!puddleLeaver.recentlySpawnedPuddles.Contains(puddle))
                sumSize += puddle.localScale.x * puddle.localScale.x;
        }

        return sumSize;
    }

    private void checkPuddleEnter(Collider2D col)
    {
        if (generalDeath.IsDead)
            return;

        if (col.gameObject.layer == 8)
            collidedPuddles.Add(col.transform);

        updateSpeedAndEnergy();
    }

    private void updateSpeedAndEnergy()
    {
        if (generalDeath.IsDead || !IsGrounded)
        {
            puddleSlowFactor = 1f;
            return;
        }

        // Decrease speed proportionally to the volume of the puddles
        puddleSlowFactor = 1 - Mathf.Min(0.5f, sumCollidedPuddlesSize() / (3 * Size.MaxSize));

        // Decrease energy per puddle the slime is in
        for (int i = 0; i < Mathf.Min(3, getNumCollidedPuddles()); i++)
        {
            energy.LoseEnergyFromPuddle();
        }
        
    }

    private void checkPuddleExit(Collider2D col)
    {
        if (generalDeath.IsDead)
            return;

        if (col.gameObject.layer == 8)
            collidedPuddles.Remove(col.transform);
    }

    void OnTriggerEnter2D(Collider2D col) => checkPuddleEnter(col);
    void OnTriggerExit2D(Collider2D col) => checkPuddleExit(col);

    private float getNumCollidedPuddles()
    {
        int num = 0;

        foreach (Transform puddle in collidedPuddles)
        {
            if (!puddleLeaver.recentlySpawnedPuddles.Contains(puddle))
                num++;
        }

        return num;
    }
}
