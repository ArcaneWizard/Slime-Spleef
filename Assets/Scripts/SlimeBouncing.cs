using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBouncing : MonoBehaviour
{
    private void Update()
    {
        float y = (Mathf.Sin(5*Time.time) + 1) * 0.5f;
        float stretch = (Mathf.Cos(5 * Time.time));
        transform.localPosition = new Vector3(0, y, -3);
        transform.localScale = new Vector3(1, 1, 1) * 0.7f;
    }
}
