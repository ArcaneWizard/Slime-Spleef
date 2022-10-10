using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISize : Size
{
    private static Vector2 sizeRange = new Vector2(0.7f * StartingSize, StartingSize * 5);

    protected override void Start()
    {
        base.Start();

        fullSize = Random.Range(sizeRange.x, sizeRange.y);
        updateSize();
    }
}
