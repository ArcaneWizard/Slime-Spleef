using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void Update() 
    {
        if (!generalDeath.IsDead)
        {
            IsSliding = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space);
            GetUserInput();
        }

        base.Update();
    }

    private void GetUserInput()
    {
        int x = 0;
        int y = 0;

        if (Input.GetKey(KeyCode.W))
            ++y;
        if (Input.GetKey(KeyCode.S))
            --y;
        if (Input.GetKey(KeyCode.A))
            --x;
        if (Input.GetKey(KeyCode.D))
            ++x;

        MovementDir = new Vector2(x, y);
    }
}
