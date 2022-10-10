using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavePuddle : MonoBehaviour
{

    public HashSet<Transform> recentlySpawnedPuddles; /* { get; private set; } */

    private Movement movement;
    private GeneralDeath generalDeath;
    private Spawning spawning;

    [SerializeField] private Transform puddle;
    [SerializeField] private Transform centerOfSlime;

    private List<GameObject> puddles;
    private int puddleIndex = 0;

    private const float delayWhenSliding = 0.1f;
    private const float delayWhenBouncing = 0.25f;

    private const float lingerDurationAfterSliding = 3f;
    private const float lingerDurationAfterBouncing = 10f;

    private float puddleDelayTimer;
    private bool canSpawnPuddle;

    void Awake()
    {
        movement = transform.parent.GetComponent<Movement>();
        generalDeath = transform.parent.GetComponent<GeneralDeath>();
        spawning = transform.parent.GetComponent<Spawning>();
    }

    void Start()
    {
        puddles = new List<GameObject>();
        recentlySpawnedPuddles = new HashSet<Transform>();
        for(int i = 0; i < 30; i++)
        {
            GameObject puddleClone = Instantiate(puddle.gameObject, transform.position, Quaternion.identity, null);
            puddleClone.SetActive(false);
            puddles.Add(puddleClone);
        }

        spawning.OnNewSpawn += initializePuddleSystem;
        initializePuddleSystem();
    }

    private void Update()
    {
        Debug.Log(recentlySpawnedPuddles.Count);
    }

    void LateUpdate()
    {
        if (generalDeath.IsDead)
            return;

        if (movement.IsGrounded && canSpawnPuddle)
        {
            canSpawnPuddle = false;
            puddleDelayTimer = movement.IsSliding ? delayWhenSliding : delayWhenBouncing;
            leavePuddle();
        }

        if (puddleDelayTimer >= 0)
            puddleDelayTimer -= Time.deltaTime;

        else if (!canSpawnPuddle)
            canSpawnPuddle = true;
    }

    private void initializePuddleSystem()
    {
        canSpawnPuddle = true;
        puddleDelayTimer = delayWhenBouncing;
        recentlySpawnedPuddles.Clear();
    }

    private IEnumerator InitiateRecentlySpawned(float time, Transform puddle)
    {
        recentlySpawnedPuddles.Add(puddle);
        yield return new WaitForSeconds(time);
        recentlySpawnedPuddles.Remove(puddle);
    }

    private void leavePuddle()
    {
        Transform newPuddle = puddles[puddleIndex].transform;
        puddleIndex = ++puddleIndex % puddles.Count;

        float slimeSize = transform.localScale.x;
        newPuddle.localScale = new Vector3(1.61f, 1.47f, 1) * slimeSize * 3;

        Vector3 sizeOffsetForPuddle = -new Vector3(0, 0.1f * slimeSize, 0);
        newPuddle.position = centerOfSlime.position + sizeOffsetForPuddle + speedOffsetForPuddle();
        newPuddle.gameObject.SetActive(true);

        // for 1 second, puddle is added to "recently spawned set"
        IEnumerator setRecentSpawn = InitiateRecentlySpawned(1f, newPuddle.transform);
        StartCoroutine(setRecentSpawn);

        // puddle will disappear after x seconds
        float time = movement.IsSliding ? lingerDurationAfterSliding : lingerDurationAfterBouncing;
        IEnumerator countdown = newPuddle.GetComponent<Puddles>().InitiateDisappearingCountdown(time);
        StartCoroutine(countdown);
    }

    private Vector3 speedOffsetForPuddle()
    {
        if (movement.IsSliding)
            return Vector3.zero;

        else
        {
            float multiplier;
            multiplier = (movement.MovementDir.normalized.y > 0.5f) ? 0.09f : 0.09f;

            return new Vector3(movement.MovementDir.x, movement.MovementDir.y, 0).normalized
                * transform.localScale.x * multiplier * movement.Speed;
        }

    }
}
