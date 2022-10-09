using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : Throw
{
    [SerializeField] Camera mainCamera;

    void Update()
    {
        if (generalDeath.IsDead)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            throwBit(worldMousePosition, power);
        }
    }
}
