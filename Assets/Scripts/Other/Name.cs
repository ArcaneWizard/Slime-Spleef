using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Name : MonoBehaviour
{
    public string slimeName;
    private TextMeshPro textMesh;

    [SerializeField] private Spawning spawning;
    [SerializeField] private GeneralDeath generalDeath;

    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    void Start()
    {
        generalDeath.UponDying += setNameToEmpty;
        spawning.OnNewSpawn += resetName;

        setNameToEmpty();
    }

    protected virtual void resetName() => textMesh.text = slimeName;
    private void setNameToEmpty() => textMesh.text = "";
}
