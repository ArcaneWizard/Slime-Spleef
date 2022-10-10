using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawning : Spawning
{
    private GeneralDeath generalDeath;

    protected override void Awake()
    {
        base.Awake();
        generalDeath = transform.GetComponent<GeneralDeath>();
    }

    void Start()
    {
        generalDeath.UponDying += StartGame;
        StartGame();
    }
}
