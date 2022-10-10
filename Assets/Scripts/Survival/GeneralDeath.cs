using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralDeath : MonoBehaviour
{
    public event Action UponDying;
    public bool IsDead { get; private set; }

    private Movement movement;
    private Energy energy;
    private Spawning spawning;


    void Awake()
    {
        movement = transform.GetComponent<Movement>();
        energy = transform.GetComponent<Energy>();
        spawning = transform.GetComponent<Spawning>();
        IsDead = true;
    }

    void Start()
    {
        spawning.OnNewSpawn += EntityIsAlive;
    }

    void Update()
    {
        if (IsDead)
            return;

        if (energy.NormalizedValue <= 0f)
            RegisterDeath();
    }

    public virtual void RegisterDeath()
    {
        IsDead = true;
        UponDying?.Invoke();
    }

    private void EntityIsAlive()
    {
        movement.ClearCollidedPuddles();
        IsDead = false;
    }
}
