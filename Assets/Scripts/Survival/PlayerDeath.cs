using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : GeneralDeath
{
    [SerializeField] private Animator deathScreenAnimator;
    private AudioSource audioSource;

    protected override void Awake()
    {
        audioSource = transform.GetChild(0).GetComponent<AudioSource>();
        UponDying += playDeathAudio;
        base.Awake();
    }

    private void playDeathAudio() => audioSource.Play();

    public override void RegisterDeath()
    {
        base.RegisterDeath();
        deathScreenAnimator.gameObject.SetActive(true);
    }
}
