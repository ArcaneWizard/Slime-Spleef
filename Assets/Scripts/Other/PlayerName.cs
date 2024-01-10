using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : Name
{
    [SerializeField] private Text userInput;

    protected override void resetName()
    {
        slimeName = userInput.text;
        base.resetName();
    }
}
