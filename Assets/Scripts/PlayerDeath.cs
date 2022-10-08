using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : GeneralDeath
{
    public override void RegisterDeath()
    {
        base.RegisterDeath();
        Debug.Log("Fell Into Puddle");
        // End Screen
        // Join Game Screen
    }
}
