using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEnergyBar : MonoBehaviour
{
    [SerializeField] private GameObject energyBar;
    [SerializeField] private PlayerDeath playerDeath;

    void Start() => energyBar.SetActive(!playerDeath.IsDead);

    void Update() => energyBar.SetActive(!playerDeath.IsDead);
}
