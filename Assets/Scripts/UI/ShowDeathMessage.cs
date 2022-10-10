using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeathMessage : MonoBehaviour
{
    [SerializeField] private Energy playerEnergy;
    [SerializeField] private GeneralDeath playerDeath;
    private Text text;

    void Awake()
    {
        text = transform.GetComponent<Text>();
    }

    void Start()
    {
        playerDeath.UponDying += showMessage;
        text.text = "";
    }

    private void showMessage()
    {
        text.text = TextGenerator.GenerateDeathMessage(playerEnergy.NormalizedValue <= 0);
    }
}
