
using System;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public event Action OnNewSpawn;
    private SpriteRenderer renderer;

    public Color32 SlimeColor { get; private set; }

    protected virtual void Awake()
    {
        renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();  
    }

    public virtual void StartGame()
    {
        OnNewSpawn?.Invoke();

        spawnOnMap(0);

        int r = UnityEngine.Random.Range(0, 256);
        int g = UnityEngine.Random.Range(0, 256);
        int b = UnityEngine.Random.Range(0, 256);

        SlimeColor = new Color32((byte)r, (byte)g, (byte)b, (byte)255);
        renderer.color = SlimeColor;
    }

    private void spawnOnMap(int n)
    {
        if (n == 4)
        {
            transform.position = randomMapPosition();
            return;
        }

        Vector3 pos = randomMapPosition();
        if (Physics2D.OverlapCircleAll(transform.position, 2.5f, Constants.PuddleLM).Length == 0)
            transform.position = pos;
        else
            spawnOnMap(n + 1);
    }

    public static Vector3 randomMapPosition()
    {
        float x = UnityEngine.Random.Range(Constants.MapXBounds.x, Constants.MapXBounds.y);
        float y = UnityEngine.Random.Range(Constants.MapYBounds.x, Constants.MapYBounds.y);

        return new Vector3(x, y, 0);
    }
}   