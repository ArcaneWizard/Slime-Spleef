using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Throw : MonoBehaviour
{
    [SerializeField] private Transform centerOfSlime;
    [SerializeField] private Transform pellet;
    
    protected GeneralDeath generalDeath;
    private Energy energy;
    private Size size;

    private List<Transform> pellets;
    private int pelletIndex;

    protected const float power = 650;

    void Awake()
    {
        generalDeath = transform.parent.GetComponent<GeneralDeath>();
        energy = transform.parent.GetComponent<Energy>();
        size = transform.GetComponent<Size>();
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

    protected void throwBit(Vector3 dir, float power)
    {
        Vector3 dirOnPlane = Constants.WorldPlaneRotation * new Vector3(dir.x, dir.y, 0);
        Vector3 throwDir = (dirOnPlane + new Vector3(0, 1f, -1f)).normalized;
        Vector3 force = power * throwDir * (0.06f * (centerOfSlime.position.y - transform.localPosition.y - transform.parent.position.y) + 1f);

        // get available pellet, spawn it, and throw it correctly
        Transform currPellet = pellets[pelletIndex];
        Pellet pellet = currPellet.GetComponent<Pellet>();

        currPellet.gameObject.SetActive(true);
        pellet.ConfigureTrajectory(centerOfSlime.position, force);

        // decrease slime size, slime energy, and use different pellet in future
        size.UpdateSizeAfterThrowingPellet();
        energy.LoseEnergyFromThrowing();
        pelletIndex = ++pelletIndex % pellets.Count;
    }
}
