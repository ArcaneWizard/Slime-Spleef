using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : IState
{
    private AIMovement movement;

    public Wander(AIMovement movement)
    {
        this.movement = movement;
    }

    public void OnEnter() => movement.EnterState(this);

    public void OnExit() => movement.ExitState();

    public void Tick() { }
}
