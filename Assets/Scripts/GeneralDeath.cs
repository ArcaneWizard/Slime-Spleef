using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralDeath : MonoBehaviour
{
    public event Action UponDying;
    public bool IsDead;
    private Transform body;

    void Awake()
    {
        IsDead = false;
        body = transform.GetChild(0);
    }

    public virtual void RegisterDeath()
    {
        IsDead = true;
        UponDying?.Invoke();
    }

    private void checkHoleLanding(Collider2D col)
    {
        if (IsDead)
            return;

        if (col.gameObject.layer == 8 && body.localPosition.y <= 0.01f && col.gameObject.transform.localScale.x > 1.2f * body.localScale.x)
            RegisterDeath();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        checkHoleLanding(col);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        checkHoleLanding(col);
    }
}
