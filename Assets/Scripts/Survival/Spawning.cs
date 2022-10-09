using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public event Action OnNewSpawn;

    public void StartGame()
    {
        OnNewSpawn?.Invoke();
        transform.position = randomMapPosition();
    }

    public static Vector3 randomMapPosition()
    {
        float x = UnityEngine.Random.Range(Constants.mapXBounds.x, Constants.mapXBounds.y);
        float y = UnityEngine.Random.Range(Constants.mapYBounds.x, Constants.mapYBounds.y);

        return new Vector3(x, y, 0);
    }
}