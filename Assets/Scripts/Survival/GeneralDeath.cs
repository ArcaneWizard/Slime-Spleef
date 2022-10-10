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
    private Size size;
    private Score score;

    void Awake()
    {
        movement = transform.GetComponent<Movement>();
        energy = transform.GetComponent<Energy>();
        spawning = transform.GetComponent<Spawning>();
        size = transform.GetChild(0).GetComponent<Size>();
        score = transform.GetComponent<Score>();

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
        score.SlimeScore = 0;
    }

    private void EntityIsAlive()
    {
        movement.ClearCollidedPuddles();
        IsDead = false;
        score.SlimeScore = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 11)
        {
            Size enemySlimeSize = col.transform.parent.GetComponent<Size>();
            Score enemyScore = col.transform.parent.parent.GetComponent<Score>();

            if (size.size > enemySlimeSize.size)
            {
                size.UpdateSizeAfterKill(enemySlimeSize.size);
                score.GainScoreFromKill(enemyScore.SlimeScore);
                col.transform.parent.parent.GetComponent<GeneralDeath>().RegisterDeath();
            }
        }
    }
}
