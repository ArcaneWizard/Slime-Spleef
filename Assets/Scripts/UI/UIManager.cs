using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject energyBar;
    [SerializeField] private GameObject scoreBar;
    [SerializeField] private Text lobbyScore;

    [SerializeField] private Text energyText;
    [SerializeField] private Text scoreText;

    [SerializeField] private Spawning spawning;
    [SerializeField] private GeneralDeath generalDeath;
    [SerializeField] private Energy energy;
    [SerializeField] private Score score;

    void Start()
    {
        spawning.OnNewSpawn += () => toggleInGameUI(true);
        generalDeath.UponDying += () => toggleInGameUI(false);

        energyBar.SetActive(false);
        scoreBar.SetActive(false);
        deathScreen.SetActive(true);
    }

    void Update()
    {
        energyText.text = $"Energy: {(int)energy.EnergyValue}";
        scoreText.text = $"Score: {score.SlimeScore}";
    }

    private void toggleInGameUI(bool toggle)
    {
        energyBar.SetActive(toggle);
        scoreBar.SetActive(toggle);
        deathScreen.SetActive(!toggle);
        lobbyScore.text = $"Score: {score.SlimeScore}";
    }
}
