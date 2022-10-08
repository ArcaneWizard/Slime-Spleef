using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : SlimeActions
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float power;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            throwBit(worldMousePosition, power);
        }
    }
}
