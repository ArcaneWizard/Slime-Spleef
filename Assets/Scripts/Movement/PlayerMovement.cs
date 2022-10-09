using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    [SerializeField] private float speed = 2;
    private Rigidbody2D rig;

    protected override void Awake()
    {
        base.Awake();

        rig = transform.GetComponent<Rigidbody2D>();
    }

    protected override void Update() 
    {
        if (generalDeath.IsDead)
        {
            rig.velocity = Vector2.zero;
            return;
        }

        base.Update();
        IsSliding = Input.GetKey(KeyCode.LeftShift);
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

        rig.velocity = new Vector2(x, y) * speed;
    }
}
