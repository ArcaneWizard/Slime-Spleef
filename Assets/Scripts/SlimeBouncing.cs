using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBouncing : MonoBehaviour
{
    void Update()
    {
        float y = (Mathf.Sin(5*Time.time) + 1) * 0.5f;
        transform.localPosition = new Vector3(0, y, -3);
    }
}
