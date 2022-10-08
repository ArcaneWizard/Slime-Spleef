using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeActions : MonoBehaviour
{
    [SerializeField] private Transform pellet;
    private List<Transform> pellets;

    private int pelletIndex;

    void Start()
    {
        pellets = new List<Transform>();

        for (int i = 0; i < 5; i++)
        {
            GameObject pelletClone = Instantiate(pellet.gameObject, transform.position, Quaternion.identity, null);
            pelletClone.SetActive(false);
            pellets.Add(pelletClone.transform);
        }

        pelletIndex = 0;
    }
    protected void throwBit(Vector3 location, float power)
    {
        Vector3 slimeOffset = location - transform.position;
        Vector3 force = (slimeOffset.normalized + new Vector3(0, 0, 1)) * power;

        Transform currPellet = pellets[pelletIndex];
        Rigidbody pelletRig = currPellet.GetComponent<Rigidbody>();

        pelletRig.velocity = Vector3.zero;
        currPellet.position = transform.position;
        pelletRig.AddForce(force);

        pelletIndex = ++pelletIndex % pellets.Count;
    }

    protected void superJump(Vector2 dir)
    {

    }
}
