using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    private Rigidbody2D rig;

    private void Awake()
    {
        rig = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() 
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

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 8)
            Debug.Log("Entered Hole");
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 8)
            Debug.Log("Exited Hole");
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        
    }
}
