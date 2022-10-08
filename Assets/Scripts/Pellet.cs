using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private const float pelletDelayDuration = 0.1f;

    private bool canSpawnPellet;
    private float pelletDelayTimer;

    private Rigidbody rig;
    private Vector3 rotationalChange;
    private Vector3 initPos;
    private float epsilon;

    private Vector2 distanceRange = new Vector2(4, 5);

    void Awake()
    {
        rig = transform.GetComponent<Rigidbody>();
    }

    void Start()
    {
        canSpawnPellet = true;
        pelletDelayTimer = pelletDelayDuration;
        rotationalChange = new Vector3(0, 0, UnityEngine.Random.Range(-10, 10));
        epsilon = 0.1f;
    }

    public void ConfigureTrajectory(Vector3 dirOnPlane, float power, Vector3 initPos, Vector3 force)
    {
        rig.velocity = Vector3.zero;
        transform.position = initPos;
        this.initPos = initPos;
        rig.AddForce(force);

        canSpawnPellet = false;
        pelletDelayTimer = pelletDelayDuration;
    }

    void Update()
    {
        transform.localEulerAngles += rotationalChange;

        if (canSpawnPellet && Mathf.Abs((transform.position.y - initPos.y) - (transform.position.z - initPos.z)) < epsilon)
        {
            // spawn hole
            
            gameObject.SetActive(false);
        }

        if (pelletDelayTimer >= 0)
            pelletDelayTimer -= Time.deltaTime;

        else if (!canSpawnPellet)
            canSpawnPellet = true;
    }

    void FixedUpdate()
    {
        Vector3 gravity = 4.9f*new Vector3(0, -0.707f, 0.707f);
        rig.AddForce(gravity);
        
    }
}