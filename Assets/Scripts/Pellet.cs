using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private Rigidbody rig;
    private Vector3 rotationalChange;

    void Awake()
    {
        rig = transform.GetComponent<Rigidbody>();
    }

    void Start()
    {
        rotationalChange = new Vector3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-8, 8));
    }

    void FixedUpdate()
    {
        Vector3 gravity = new Vector3(0, 0, 4.9f);
        rig.AddForce(Quaternion.Euler(45, 0, 0) * gravity);
        transform.localEulerAngles += rotationalChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}