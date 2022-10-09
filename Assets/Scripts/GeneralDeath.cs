using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralDeath : MonoBehaviour
{
    public event Action UponDying;
    public bool IsDead { get; private set; }

    private Energy energy;
    private Transform body;

    void Awake()
    {
        IsDead = false;
        energy = transform.GetComponent<Energy>();
        body = transform.GetChild(0);
    }

    void Update()
    {
        if (IsDead)
            return;

        if (energy.EnergyValue <= 0f)
            RegisterDeath();
    }

    public virtual void RegisterDeath()
    {
        IsDead = true;
        UponDying?.Invoke();
    }

    private void checkPuddleLanding(Collider2D col)
    {
        if (IsDead)
            return;

        if (col.gameObject.layer == 8 && body.localPosition.y <= 0.01f && col.gameObject.transform.localScale.x > 1.2f * body.localScale.x)
            RegisterDeath();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        checkPuddleLanding(col);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        checkPuddleLanding(col);
    }
}
