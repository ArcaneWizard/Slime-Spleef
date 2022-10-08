using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : SlimeActions
{
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            throwBit(Input.mousePosition, 1.0f);
    }
}
