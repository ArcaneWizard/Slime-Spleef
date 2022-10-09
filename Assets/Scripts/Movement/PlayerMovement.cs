using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    protected override void Update() 
    {
        if (generalDeath.IsDead)
        {
            setVelocity(Vector2.zero);
            return;
        }

        IsSliding = Input.GetKey(KeyCode.LeftShift);
        base.Update();
        setVelocity();
    }

    private void setVelocity()
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

        setVelocity(new Vector2(x, y));
    }
}
