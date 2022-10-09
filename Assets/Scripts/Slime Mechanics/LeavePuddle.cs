using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavePuddle : MonoBehaviour
{
    private Movement movement;
    private GeneralDeath generalDeath;
    private Spawning spawning;

    [SerializeField] private Transform puddle;
    [SerializeField] private Transform centerOfSlime;

    private List<GameObject> puddles;
    private int puddleIndex = 0;

    private const float puddleDelayWhenSliding = 0.1f;
    private const float puddleDelayWhenBouncing = 0.25f;
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
        for(int i = 0; i < 30; i++)
        {
            GameObject puddleClone = Instantiate(puddle.gameObject, transform.position, Quaternion.identity, null);
            puddleClone.SetActive(false);
            puddles.Add(puddleClone);
        }

        spawning.OnNewSpawn += initializePuddleSystem;
        initializePuddleSystem();
    }

    void LateUpdate()
    {
        if (generalDeath.IsDead)
            return;

        if (movement.IsGrounded && canSpawnPuddle)
        {
            canSpawnPuddle = false;
            puddleDelayTimer = movement.IsSliding ? puddleDelayWhenSliding : puddleDelayWhenBouncing;
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
        puddleDelayTimer = puddleDelayWhenBouncing;
    }

    private void leavePuddle()
    {
        Transform newPuddle = puddles[puddleIndex].transform;
        puddleIndex = ++puddleIndex % puddles.Count;

        float slimeSize = transform.localScale.x;
        newPuddle.localScale = new Vector3(1, 0.5f, 1) * slimeSize * 3;
        newPuddle.position = centerOfSlime.position - new Vector3(0, 0.1f * slimeSize, 0);
        newPuddle.gameObject.SetActive(true);

        float time = movement.IsSliding ? 2.8f : 20f;
        IEnumerator countdown = newPuddle.GetComponent<Puddles>().InitiateDisappearingCountdown(time);
        StartCoroutine(countdown);
    }
}
