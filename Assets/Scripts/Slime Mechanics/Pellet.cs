using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    private Rigidbody rig;

    private Vector3 rotationalChange;
    private Vector3 initPos;

    private const float epsilon = 0.2f;
    private Vector3 initialSplashSize = new Vector3(4, 3.4f, 4) * 4f;

    private List<Transform> puddles;
    private int puddleIndex;

    void Awake() => rig = transform.GetComponent<Rigidbody>();

    void Start()
    {
        rotationalChange = new Vector3(0, 0, Random.Range(-10, 10));

        puddles = new List<Transform>();
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            GameObject child = transform.GetChild(0).GetChild(i).gameObject;
            child.SetActive(false);

            GameObject puddleClone = Instantiate(child, transform.GetChild(0).position, Quaternion.identity, null);
            float pelletSize = transform.localScale.x;
            puddleClone.transform.localScale = initialSplashSize * pelletSize;

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
    }

    void Update()
    {
        transform.localEulerAngles += rotationalChange;
    }

    void FixedUpdate()
    {
        rig.AddForce(Constants.WorldGravity);

        if (Mathf.Abs((transform.position.y - initPos.y) - (transform.position.z - initPos.z)) < epsilon &&
                sqrDistance(initPos, transform.position) > 0.1f)
        {
            leavePuddle();
            gameObject.SetActive(false);
        }
    }

    private void leavePuddle()
    {
        Transform newPuddle = puddles[puddleIndex];
        puddleIndex = ++puddleIndex % puddles.Count;

        float pelletSize = transform.localScale.x;
        newPuddle.localScale = initialSplashSize * pelletSize;
        newPuddle.position = transform.GetChild(0).position - new Vector3(0, 0.2f * pelletSize, 0);
        newPuddle.eulerAngles = new Vector3(0, 0, 0);

        newPuddle.GetComponent<SpriteRenderer>().color = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        newPuddle.gameObject.SetActive(true);

        newPuddle.parent = null;

        IEnumerator countdown = newPuddle.GetComponent<Puddles>().InitiateDisappearingCountdown(20f);
        StartCoroutine(countdown);
    }

    // returns the square distance between 2 world positions
    private float sqrDistance(Vector3 a, Vector3 b) 
        => (b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y) + (b.z - a.z) * (b.z - a.z);

}