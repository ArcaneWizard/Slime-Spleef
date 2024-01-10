using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private StateMachine stateMachine;
    private AIMovement movement;
    private Energy energy;
    private Size size;
    private ChaseSlime chaseSlime;

    private Collider2D[] nearbySlimes;
    private bool canChaseSmallerSlime;

    private bool hungryButWantFood;

    void Awake()
    {
        stateMachine = new StateMachine();

        energy = transform.GetComponent<Energy>();
        movement = transform.GetComponent<AIMovement>();
        size = transform.GetChild(0).GetComponent<Size>();

        StartCoroutine(scanForSlimes());
    }

    void Start()
    {
        Wander wander = new Wander(movement);
        CollectFood collectFood = new CollectFood(movement);
        chaseSlime = new ChaseSlime(transform, movement);

        stateMachine.SetState(wander);

        alwaysTransition(chaseSlime, () => (!hungry() && canChaseSmallerSlime)
            || (hungry() && hungryButWantFood && canChaseSmallerSlime));
        alwaysTransition(collectFood, () => hungry());
        alwaysTransition(wander, () => !hungry() && !canChaseSmallerSlime);

        void transition(IState from, IState to, Func<bool> condition) =>
            stateMachine.AddTransition(from, to, condition);

        void alwaysTransition(IState to, Func<bool> condition) =>
            stateMachine.AddAlwaysCalledTransition(to, condition);
    }

    void Update() => stateMachine.Tick();

    private bool hungry()
    {
        return energy.NormalizedValue <= 0.4f ||
            (energy.NormalizedValue <= 0.76f && UnityEngine.Random.Range(0f, 1f) < 0.07f);
    }

    private IEnumerator scanForSlimes()
    {
        if (!hungry())
        {
            nearbySlimes = Physics2D.OverlapCircleAll(transform.position, 10f, 1 << 6);

            bool smallerSlimeNearby = false;
            foreach (Collider2D slime in nearbySlimes)
            {
                if (slime.transform.GetComponent<Size>().size < size.size)
                {
                    smallerSlimeNearby = true;
                    chaseSlime.UpdateSlimeToChase(slime.transform);
                    break;
                }
            }

            canChaseSmallerSlime = (smallerSlimeNearby && UnityEngine.Random.Range(0f, 1f) < 0.75f);
        }

        if (hungry())
            hungryButWantFood = UnityEngine.Random.Range(0f, 1f) < 0.3f;
        else
            hungryButWantFood = false;

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(scanForSlimes());
    }


}