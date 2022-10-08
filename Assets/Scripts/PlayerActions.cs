using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : SlimeActions
{
    [SerializeField] Camera camera;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 worldMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            throwBit(worldMousePosition, 1.0f);
        }
    }
}
