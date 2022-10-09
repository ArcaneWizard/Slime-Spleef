using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPelletsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject FoodPellet;
    private static Stack<GameObject> pelletsPendingSpawn;

    private const int maxPelletCapacity = 300; // maximum number of pellets possible on the map
    private const int initialPelletCapcity = 200; // initial number of pellets spawned on the map

    private Vector2 timeBtwnPelletSpawns = new Vector2(0.5f, 1f);

    void Start()
    {
        pelletsPendingSpawn = new Stack<GameObject>();

        for (int i = 0; i < maxPelletCapacity; i++)
        {
            GameObject pellet = Instantiate(FoodPellet, Spawning.randomMapPosition(), Quaternion.identity, transform);
            pellet.SetActive(i < initialPelletCapcity);

            if (i > initialPelletCapcity)
                pelletsPendingSpawn.Push(pellet);
        }

        StartCoroutine(repeatedlySpawnPellets());
    }

    public static void AddPelletAwaitingSpawn(GameObject pellet) => pelletsPendingSpawn.Push(pellet);

    private IEnumerator repeatedlySpawnPellets()
    {
        yield return new WaitForSeconds(Random.Range(timeBtwnPelletSpawns.x, timeBtwnPelletSpawns.y));
        spawnAvailablePellet();
    }

    private void spawnAvailablePellet()
    {
        if (pelletsPendingSpawn.Count == 0)
            return;

        GameObject pellet = pelletsPendingSpawn.Pop();
        pellet.transform.position = Spawning.randomMapPosition();
        pellet.SetActive(true);
    }
}
