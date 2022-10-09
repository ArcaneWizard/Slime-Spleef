using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : GeneralDeath
{
    [SerializeField] private Animator deathScreenAnimator;

    public override void RegisterDeath()
    {
        base.RegisterDeath();
        deathScreenAnimator.gameObject.SetActive(true);
    }
}
