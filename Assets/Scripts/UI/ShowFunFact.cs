using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFunFact : MonoBehaviour
{
    [SerializeField] private GeneralDeath playerDeath;
    private Text text;

    void Awake()
    {
        text = transform.GetComponent<Text>();
    }

    void Start()
    {
        playerDeath.UponDying += showMessage;
        showMessage();
    }

    private void showMessage()
    {
        text.text = TextGenerator.GenerateFunFact();
    }
}
