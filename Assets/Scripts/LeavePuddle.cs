using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavePuddle : MonoBehaviour
{
    private const float puddleDelayDuration = 0.1f;
    
    private bool canSpawnPuddle;
    private float puddleDelayTimer;

    [SerializeField] private Transform puddle;
    private List<GameObject> puddles;
    private int puddleIndex = 0;

    void Start()
    {
        canSpawnPuddle = true;
        puddleDelayTimer = puddleDelayDuration;

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
        if (transform.localPosition.y <= 0.01f && canSpawnPuddle)
        {
            canSpawnPuddle = false;
            puddleDelayTimer = puddleDelayDuration;
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
        newPuddle.position = transform.parent.position - new Vector3(0, 0.2f * slimeSize, 0);
        newPuddle.gameObject.SetActive(true);

        IEnumerator countdown = newPuddle.GetComponent<Puddles>().InitiateDisappearingCountdown(timer);
        StartCoroutine(countdown);
    }
}
