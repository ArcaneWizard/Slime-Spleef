using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private StateMachine stateMachine;
    private AIMovement movement;
    private Energy energy;

    void Awake()
    {
        stateMachine = new StateMachine();

        energy = transform.GetComponent<Energy>();
        movement = transform.GetComponent<AIMovement>();
    }

    void Start()
    {
        Wander wander = new Wander(movement);
        CollectFood collectFood = new CollectFood(movement, transform);

        stateMachine.SetState(wander);

        transition(wander, collectFood, () => hungry());
        transition(collectFood, wander, () => !hungry());

        void transition(IState from, IState to, Func<bool> condition) =>
            stateMachine.AddTransition(from, to, condition);
    }

    void Update() => stateMachine.Tick();

    private bool hungry()
    {
        return energy.NormalizedValue <= 0.4f ||
            (energy.NormalizedValue <= 0.8f && UnityEngine.Random.Range(0f, 1f) < 0.05f);
    }
}