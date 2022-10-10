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
    private Transform body;
    private LeavePuddle puddleLeaver;

    private HashSet<Transform> collidedPuddles;

    void Awake()
    {
        movement = transform.GetComponent<Movement>();
        energy = transform.GetComponent<Energy>();
        spawning = transform.GetComponent<Spawning>();
        puddleLeaver = transform.GetChild(0).GetComponent<LeavePuddle>();
        body = transform.GetChild(0);
        IsDead = true;
        collidedPuddles = new HashSet<Transform>();
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
        collidedPuddles.Clear();
        IsDead = false;
    }

    private float sumCollidedPuddlesSize()
    {
        float sumSize = 0f;

        foreach (Transform puddle in collidedPuddles)
        {
            if(!puddleLeaver.recentlySpawnedPuddles.Contains(puddle))
                sumSize += puddle.localScale.x * puddle.localScale.x;
        }

        return sumSize;
    }

    private void checkPuddleEnter(Collider2D col)
    {
        if (IsDead)
            return;

        if (col.gameObject.layer == 8) 
            collidedPuddles.Add(col.transform);

        checkPuddleLanding();
    }

    private void checkPuddleLanding()
    {
        if (IsDead)
            return;

        if (movement.IsGrounded && sumCollidedPuddlesSize() > body.localScale.x * body.localScale.x * 11)
            RegisterDeath();
    }

    private void checkPuddleExit(Collider2D col)
    {
        if (IsDead)
            return;

        if (col.gameObject.layer == 8)
            collidedPuddles.Remove(col.transform);
    }

    void OnTriggerEnter2D(Collider2D col) => checkPuddleEnter(col);
    void OnTriggerStay2D(Collider2D col) => checkPuddleLanding();
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
