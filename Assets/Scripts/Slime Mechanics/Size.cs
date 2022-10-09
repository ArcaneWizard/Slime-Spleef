using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    private const float maxSize = 1.5f;
    private const float minSize = 0.15f;
    private const float startingSize = 0.4f;
    private float fullSize;

    public float size { get; private set; }

    private Energy energy;
    private Spawning spawning;

    void Awake()
    {
        energy = transform.parent.GetComponent<Energy>();
        spawning = transform.parent.GetComponent<Spawning>();
    }

    void Start()
    {
        spawning.OnNewSpawn += resetSize;
        resetSize();
    }

    void Update()
    {

        size = energy.NormalizedValue * fullSize;

        if (size > maxSize)
            size = maxSize;

        if (size < minSize)
            size = minSize;

        transform.localScale = new Vector3(size, size, size);
    }

    public void increaseFullSize()
    {
        fullSize *= 1.05f;
    }

    public void decreaseFullSize()
    {
        fullSize *= 0.95f;
    }

    private void resetSize()
    {
        fullSize = startingSize;
        size = energy.NormalizedValue * fullSize;
    }
}
