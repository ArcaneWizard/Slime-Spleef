using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    private Animator animator;

    void Awake() => animator = transform.GetComponent<Animator>();

    public void DisableDeathScreen() => animator.gameObject.SetActive(false);
}
