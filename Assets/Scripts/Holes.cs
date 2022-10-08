using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holes : MonoBehaviour
{
    public IEnumerator InitiateDisappearingCountdown (float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
