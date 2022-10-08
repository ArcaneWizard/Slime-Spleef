using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float EnergyValue { get; private set; }
    private const float depletionRate = 10f;

    void Start()
    {
        EnergyValue = 100f;
    }

    void Update()
    {
        if (EnergyValue > 0)
            EnergyValue -= Time.deltaTime * depletionRate;
        if (EnergyValue < 0)
            EnergyValue = 0;
        if (EnergyValue > 100)
            EnergyValue = 100;
    }

    public float NormalizedEnergyValue => EnergyValue / 100f;
}