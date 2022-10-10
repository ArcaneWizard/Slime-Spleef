using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFood : IState
{
    private AIMovement movement;
    private Transform slime;

    private List<Transform> nearbyFoods;

    private bool isHungry;
    private const float foodRadius = 5f;

    private float timer;
    private const float timeBtwnFoodScans = 0.3f;

    public CollectFood(AIMovement movement, Transform slime)
    {
        this.movement = movement;
        this.slime = slime;
    }

    public void OnEnter()
    {
        isHungry = true;
        movement.EnterState(this);
    }

    public void OnExit()
    {
        isHungry = false;
        movement.ExitState();
    }

    public void Tick()
    {
        timer -= Time.deltaTime;

        if (isHungry && timer <= 0)
        {
            ScanForFood();
            timer = timeBtwnFoodScans;
        }
    }

    public void ScanForFood()
    {
        Collider2D[] foodWithinRadius = Physics2D.OverlapCircleAll(slime.position, foodRadius, Constants.FoodLM);
        if (foodWithinRadius.Length == 0)
            foodWithinRadius = Physics2D.OverlapCircleAll(slime.position, foodRadius * 2f, Constants.FoodLM);

        nearbyFoods.Clear();
        foreach (Collider2D food in foodWithinRadius)
            nearbyFoods.Add(food.transform);
    }
}
