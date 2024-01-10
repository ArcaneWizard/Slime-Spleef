using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetName : MonoBehaviour
{
    [SerializeField] private GeneralDeath playerDeath;
    private Text text;

    void Awake()
    {
        text = transform.GetComponent<InputField>().textComponent;
    }

    void Start()
    {
        playerDeath.UponDying += resetName;
        resetName();
    }

    private void resetName()
    {
        text.text = "";
    }
}
