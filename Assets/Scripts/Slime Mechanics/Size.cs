using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    protected const float maxSize = 1.5f * 1.5f;
    protected const float minSize = 0.3f * 0.3f;
    protected const float startingSize = 0.4f * 0.4f;

    public float size { get; private set; }
    protected float fullSize;

    private Energy energy;
    private Spawning spawning;

    void Awake()
    {
        energy = transform.parent.GetComponent<Energy>();
        spawning = transform.parent.GetComponent<Spawning>();
    }

    protected virtual void Start()
    {
        spawning.OnNewSpawn += resetSize;
        resetSize();
    }

    void Update()
    {
        updateSize();

        if (size > maxSize)
            size = maxSize;

        if (size < minSize)
            size = minSize;

        float width = Mathf.Pow(size, 0.5f); 
        transform.localScale = new Vector3(width, width, width);
    }

    public void UpdateSizeAfterThrowingPellet() => decreaseFullSize(0.01f);

    public void UpdateSizeAfterEatingFood(FoodPelletType type)
    {
        if (type == FoodPelletType.Normal)
            increaseFullSize(0.002f); 

        else if (type == FoodPelletType.Super)
            increaseFullSize(0.01f);
    }

    protected void updateSize() => size = fullSize * Mathf.Exp(2f * energy.NormalizedValue - 2f);

    private void increaseFullSize(float delta) => fullSize = Mathf.Min(fullSize + delta, maxSize);
    private void decreaseFullSize(float delta) => fullSize = Mathf.Max(fullSize - delta, minSize);

    private void resetSize()
    {
        fullSize = startingSize;
        updateSize();
    }
}
