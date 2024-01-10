using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPelletsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject FoodPellet;

    private const int maxPelletCapacity = 1480; // maximum number of pellets possible on the map

    void Start()
    {
        for (int i = 0; i < maxPelletCapacity; i++)
        {
            GameObject pellet = Instantiate(FoodPellet, Spawning.randomMapPosition(), FoodPellet.transform.rotation, transform);
            pellet.GetComponent<FoodPellet>().InitializeType();
            pellet.SetActive(true);
        }

    }

    public static void RespawnPellet(GameObject pellet)
    {
        pellet.transform.position = Spawning.randomMapPosition();
        pellet.GetComponent<FoodPellet>().InitializeType();
        pellet.SetActive(true);
    }
}
