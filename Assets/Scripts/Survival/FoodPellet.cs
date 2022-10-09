using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// upon colliding with a slime, this pellet supplies the slime with energy and disappears. It
// awaits being respawned
public class FoodPellet : MonoBehaviour
{
    private const float energyPerPellet = 25f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)
        {
            col.transform.parent.GetComponent<Energy>().GainEnergy(energyPerPellet);
            col.transform.GetComponent<Size>().increaseFullSize();

            FoodPelletsSpawner.AddPelletAwaitingSpawn(gameObject);
            gameObject.SetActive(false);
        }
    }
}
