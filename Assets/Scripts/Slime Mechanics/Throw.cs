using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Throw : MonoBehaviour
{
    [SerializeField] private Transform centerOfSlime;
    [SerializeField] private Transform pellet;
    protected GeneralDeath generalDeath;

    private List<Transform> pellets;
    private int pelletIndex;

    protected const float power = 350;


    void Awake()
    {
        generalDeath = transform.parent.GetComponent<GeneralDeath>();
    }

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
        Vector3 dir = location - transform.position;
        Vector3 dirOnPlane = Constants.WorldPlaneRotation * new Vector3(dir.x, dir.y, 0);
        Vector3 throwDir = (dirOnPlane + new Vector3(0, 2.5f, -2.5f)).normalized;
        Vector3 force = power * throwDir;

        Transform currPellet = pellets[pelletIndex];
        Pellet pellet = currPellet.GetComponent<Pellet>();

        currPellet.gameObject.SetActive(true);
        pellet.ConfigureTrajectory(centerOfSlime.position, force);

        pelletIndex = ++pelletIndex % pellets.Count;
    }
}
