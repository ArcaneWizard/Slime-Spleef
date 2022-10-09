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
    private const float epsilon = 0.1f;

    private List<Transform> puddles;
    private int puddleIndex;

    void Awake()
    {
        rig = transform.GetComponent<Rigidbody>();
    }

    void Start()
    {
        canSpawnPellet = true;
        pelletDelayTimer = pelletDelayDuration;
        rotationalChange = new Vector3(0, 0, UnityEngine.Random.Range(-10, 10));

        puddles = new List<Transform>();
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            GameObject child = transform.GetChild(0).GetChild(i).gameObject;
            child.SetActive(false);

            GameObject puddleClone = Instantiate(child, transform.GetChild(0).position, Quaternion.identity, null);
            float pelletSize = transform.localScale.x;
            puddleClone.transform.localScale = new Vector3(4, 2, 4) * pelletSize;

            puddleClone.SetActive(false);

            puddles.Add(puddleClone.transform);
        }
        puddleIndex = 0;
    }

    public void ConfigureTrajectory(Vector3 initPos, Vector3 force)
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
            leavePuddle();
            
            gameObject.SetActive(false);
        }

        if (pelletDelayTimer >= 0)
            pelletDelayTimer -= Time.deltaTime;

        else if (!canSpawnPellet)
            canSpawnPellet = true;
    }

    void FixedUpdate()
    {
        rig.AddForce(Constants.WorldGravity);
    }

    private void leavePuddle()
    {
        Transform newPuddle = puddles[puddleIndex];
        puddleIndex = ++puddleIndex % puddles.Count;

        float timer = 20f;

        float pelletSize = transform.localScale.x;
        newPuddle.localScale = new Vector3(4, 2, 4) * pelletSize;
        newPuddle.position = transform.GetChild(0).position - new Vector3(0, 0.2f * pelletSize, 0);
        newPuddle.eulerAngles = new Vector3(0, 0, 0);
        newPuddle.gameObject.SetActive(true);

        newPuddle.parent = null;

        IEnumerator countdown = newPuddle.GetComponent<Puddles>().InitiateDisappearingCountdown(timer);
        StartCoroutine(countdown);
    }

}