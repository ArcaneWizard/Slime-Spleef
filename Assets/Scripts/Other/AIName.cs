using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIName : Name
{
    protected override void resetName()
    {
        slimeName = TextGenerator.GenerateAIName();
        base.resetName();
    }
}
