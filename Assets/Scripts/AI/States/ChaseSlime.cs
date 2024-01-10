using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSlime : IState
{
    private Transform slimeToChase;

    private Transform slime;
    private AIMovement movement;

    public ChaseSlime(Transform slime, AIMovement movement)
    {
        this.slime = slime;
        this.movement = movement;
    }

    public void UpdateSlimeToChase(Transform slime) => slimeToChase = slime;

    public void OnEnter()
    {
        Vector2 dirToSlime = slimeToChase.position - slime.position;
        movement.UpdateChaseDir(dirToSlime);
    }

    public void Tick()
    {
        Vector2 dirToSlime = slimeToChase.position - slime.position;
        movement.UpdateChaseDir(dirToSlime);
    }

    public void OnExit()
    {
        movement.UpdateChaseDir(Vector2.zero);
    }
}
