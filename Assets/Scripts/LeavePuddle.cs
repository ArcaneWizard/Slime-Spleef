using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavePuddle : MonoBehaviour
{
    private Movement movement;
    private GeneralDeath generalDeath;

    [SerializeField] private Transform puddle;
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
    }

    void Start()
    {
        canSpawnPuddle = true;
        puddleDelayTimer = puddleDelayWhenBouncing;

        puddles = new List<GameObject>();
        for(int i = 0; i < 30; i++)
        {
            GameObject puddleClone = Instantiate(puddle.gameObject, transform.position, Quaternion.identity, null);
            puddleClone.SetActive(false);
            puddles.Add(puddleClone);
        }
    }

    void LateUpdate()
    {
        //if (generalDeath.IsDead)
       //     return;

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

    private void leavePuddle()
    {
        Transform newPuddle = puddles[puddleIndex].transform;
        puddleIndex = ++puddleIndex % puddles.Count;
        float timer = 20f;

        float slimeSize = transform.localScale.x;
        newPuddle.localScale = new Vector3(1, 0.5f, 1) * slimeSize * 3;
        newPuddle.position = transform.parent.position + Constants.SlimeCenterOffsetFromSprite - new Vector3(0, 0.2f * slimeSize, 0);
        newPuddle.gameObject.SetActive(true);

        IEnumerator countdown = newPuddle.GetComponent<Puddles>().InitiateDisappearingCountdown(timer);
        StartCoroutine(countdown);
    }
}
