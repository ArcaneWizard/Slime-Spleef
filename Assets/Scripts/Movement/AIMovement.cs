using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Movement
{
    private HashSet<Vector2> nearbyPuddles;

    private const float puddleScanRadius = 5f;
    private const float puddleAversionAngle = 30f;
    private float cosineAversionAngle = Mathf.Cos(puddleAversionAngle);

    private const float foodScanRadius = 5f;

    private IState state;
    private float maxAttemptsToChangeDir;
    private Vector2 intelligenceRange = new Vector2(3f, 5f);

    private float randomNumber;
    private bool isSliding;
    private Vector2 chaseDir;

    void Start()
    {
        nearbyPuddles = new HashSet<Vector2>();
        randomNumber = UnityEngine.Random.Range(-1000000f, 1000000f);
        maxAttemptsToChangeDir = UnityEngine.Random.Range(intelligenceRange.x, intelligenceRange.y+1);
    }

    protected override void Update()
    {
        if (!generalDeath.IsDead)
        {
            IsSliding = IsAISliding();
            MovementDir = GetAIDirection();
        }

        base.Update();
    }

    public void EnterState(IState state)
    {
        float r = Random.Range(0, 1f);
        isSliding = (r < 0.5f);
        this.state = state;
    }

    public void ExitState() => state = null;

    public void UpdateChaseDir(Vector2 dir) => chaseDir = dir;

    private bool IsAISliding() => isSliding;

    private Vector2 GetAIDirection()
    {
        if (state is ChaseSlime)
            return chaseDir.normalized;

        // AI scans for and heads towards food pellets
        else if (state is CollectFood)
            return scanForNearbyFood();

        // AI wanders around by sliding and avoiding puddles
        else if (state is Wander && isSliding)
        {
            // AI scans for nearby puddles
            scanForNearbyPuddles();

            // AI has 4 attempts to change it's direction so that it avoids moving into a nearby puddle
            Vector2 dir = movementDirection();
            int attemptsToChangeDir = 0;
            while (attemptsToChangeDir <= maxAttemptsToChangeDir)
            {
                if (!IsMovingTowardsPuddle(dir))
                    return dir;

                randomNumber = UnityEngine.Random.Range(-1000000f, 1000000f);
                dir = movementDirection();
                ++attemptsToChangeDir;
            }

            // otherwise AI bounces in a random direction
            isSliding = false;
            return dir;
        }

        // AI randomly wanders around the map, oblivious to puddles
        return movementDirection();
    }

    private void scanForNearbyPuddles()
    {
        Collider2D[] closePuddles = new Collider2D[0];
        HashSet<Collider2D> closeObstaclePuddles = new HashSet<Collider2D>();

        // add puddles within a 3 or 5 unit radius to the slime's track of close puddles
        if (closePuddles.Length == 0)
            closePuddles = Physics2D.OverlapCircleAll(transform.position, puddleScanRadius, Constants.PuddleLM);
        
        foreach (Collider2D puddle in closePuddles)
            closeObstaclePuddles.Add(puddle);

        // don't count puddles that the Slime is on top of right now
        Collider2D[] superClosePuddles = Physics2D.OverlapCircleAll(transform.position, 0.8f, Constants.PuddleLM);
        foreach (Collider2D puddle in superClosePuddles)
            closeObstaclePuddles.Remove(puddle);

        // update the slime's track of nearby puddles' displacements
        nearbyPuddles.Clear();
        Vector3 currPos = transform.position;
        foreach (Collider2D puddle in closeObstaclePuddles)
            nearbyPuddles.Add(puddle.transform.position - currPos);

    }

    private Vector2 scanForNearbyFood()
    {
        Collider2D[] closeFood = Physics2D.OverlapCircleAll(transform.position, foodScanRadius, Constants.PuddleLM);

        // intialize movement direction in a random direction
        Vector2 dir = new Vector2(Random.Range(-1f,1f), Random.Range(-1f, 1f)).normalized;

        // check if any food is accessible without running into a puddle. if so head there
        scanForNearbyPuddles();
        for (int i = 0; i < Mathf.Min(closeFood.Length, 5); i++)
        {
            dir = (closeFood[i].transform.position - transform.position).normalized;
            if (!IsMovingTowardsPuddle(dir))
                return dir;
        }

        // if no food is accessible, bounce towards the last food pellet 
        isSliding = false;
        return dir;
    }

    private bool IsMovingTowardsPuddle(Vector2 dir)
    {
        // loops through each puddle and checks if the slime is heading into a puddle if it's movement
        // direction is within a given angle to the displacement vector of the puddle relative to the slime
        foreach (Vector2 puddle in nearbyPuddles)
        {
            Vector2 normalizedPuddleDisplacement = puddle.normalized;
            float cosTheta = dir.x * normalizedPuddleDisplacement.x + dir.y * normalizedPuddleDisplacement.y;

            if (cosTheta >= cosineAversionAngle)
                return true;
        }

        return false;
    }

    private Vector2 movementDirection()
    {
        float x = 2f * Mathf.PerlinNoise(0.4f * Time.time, randomNumber) - 1f;
        float y = 2f * Mathf.PerlinNoise(0.4f * Time.time, randomNumber - 100000f) - 1f;
        return new Vector2(x, y).normalized;
    }
}
