using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject slime;

    private const int numOfSlimesOnMap = 100;

    void Start()
    {
        for (int i = 0; i < numOfSlimesOnMap; i++)
            Instantiate(slime, Spawning.randomMapPosition(), Quaternion.identity, transform);
    }
}
