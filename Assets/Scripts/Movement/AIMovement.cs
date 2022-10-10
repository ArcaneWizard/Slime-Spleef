using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Movement
{
    private HashSet<Vector2> nearbyPuddles;

    private const float puddleScanRadius = 3f;
    private const float puddleAversionAngle = 30f;
    private float cosineAversionAngle = Mathf.Cos(puddleAversionAngle); 

    private IState state;
    private float maxAttemptsToChangeDir;
    private Vector2 intelligenceRange = new Vector2(3f, 10f);

    private float randomNumber;
    private bool isSliding;

    void Start()
    {
        nearbyPuddles = new HashSet<Vector2>();
        randomNumber = UnityEngine.Random.Range(-1000000f, 1000000f);
        maxAttemptsToChangeDir = UnityEngine.Random.Range(intelligenceRange.x, intelligenceRange.y);
    }

    protected override void Update()
    {
        IsSliding = IsAISliding();
        MovementDir = GetAIDirection();

        base.Update();
    }

    public void EnterState(IState state)
    {
        float r = Random.Range(0, 1f);
        isSliding = (r < 0.5f);
        this.state = state;
    }

    public void ExitState() => state = null;

    private bool IsAISliding() => isSliding;

    private Vector2 GetAIDirection()
    {
        if (state is CollectFood || isSliding)
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
        else if (state is Wander)
            return movementDirection();

        return Vector2.zero;
    }

    private void scanForNearbyPuddles()
    {
        Collider2D[] closePuddles = new Collider2D[0];
        HashSet<Collider2D> closeObstaclePuddles = new HashSet<Collider2D>();

        // add puddles within a 3 or 5 unit radius to the slime's track of close puddles
        int radiusIncrease = 0;
        while (closePuddles.Length == 0 && (puddleScanRadius + radiusIncrease) <= 5)
        {
            closePuddles = Physics2D.OverlapCircleAll(transform.position, puddleScanRadius + radiusIncrease, Constants.PuddleLM);
            radiusIncrease += 2;
        }
        
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
