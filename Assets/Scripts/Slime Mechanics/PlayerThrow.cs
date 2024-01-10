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
            Vector2 dir = Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
           // Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            throwBit(dir.normalized, power);
        }
    }
}
