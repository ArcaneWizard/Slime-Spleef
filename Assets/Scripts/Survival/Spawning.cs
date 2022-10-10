
using System;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public event Action OnNewSpawn;
    private SpriteRenderer renderer;

    protected virtual void Awake()
    {
        renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();  
    }

    public virtual void StartGame()
    {
        OnNewSpawn?.Invoke();

        transform.position = randomMapPosition();

        int r = UnityEngine.Random.Range(0, 256);
        int g = UnityEngine.Random.Range(0, 256);
        int b = UnityEngine.Random.Range(0, 256);

        renderer.color = new Color32((byte)r, (byte)g, (byte)b, (byte)255);
    }

    public static Vector3 randomMapPosition()
    {
        float x = UnityEngine.Random.Range(Constants.MapXBounds.x, Constants.MapXBounds.y);
        float y = UnityEngine.Random.Range(Constants.MapYBounds.x, Constants.MapYBounds.y);

        return new Vector3(x, y, 0);
    }
}   