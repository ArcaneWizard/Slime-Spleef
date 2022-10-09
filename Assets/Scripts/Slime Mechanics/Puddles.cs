using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddles : MonoBehaviour
{
    public IEnumerator InitiateDisappearingCountdown (float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
