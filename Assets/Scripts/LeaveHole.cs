using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveHole : MonoBehaviour
{
    private const float holeDelayDuration = 0.1f;
    
    private bool canSpawnHole;
    private float holeDelayTimer;

    [SerializeField] private Transform hole;
    private List<GameObject> holes;
    private int holeIndex = 0;

    private void Start()
    {
        canSpawnHole = true;
        holeDelayTimer = holeDelayDuration;

        holes = new List<GameObject>();
        for(int i = 0; i < 30; i++)
        {
            GameObject holeClone = Instantiate(hole.gameObject, transform.position, Quaternion.identity, null);
            holeClone.SetActive(false);
            holes.Add(holeClone);
        }
    }

    void LateUpdate()
    {
        if (transform.localPosition.y <= 0.01f && canSpawnHole)
        {
            canSpawnHole = false;
            holeDelayTimer = holeDelayDuration;
            leaveHole();
        }

        if (holeDelayTimer >= 0)
            holeDelayTimer -= Time.deltaTime;
        else if (!canSpawnHole)
            canSpawnHole = true;
    }

    private void leaveHole()
    {
        float holeSize = transform.localScale.x;
        holes[holeIndex].transform.localScale = new Vector3(1, 0.5f, 1) * holeSize;
        holes[holeIndex].transform.position = transform.parent.position - new Vector3(0, 0.2f / holeSize, 0);
        holes[holeIndex].SetActive(true);
        holeIndex = ++holeIndex % holes.Count;
    }
}
