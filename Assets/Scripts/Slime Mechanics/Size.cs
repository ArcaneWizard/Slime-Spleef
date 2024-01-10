using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour, IComparable<Size>
{
    public const float MaxSize = 0.729f;
    public const float MinSize = 0.3f * 0.3f * 0.3f;
    public const float StartingSize = 0.36f * 0.36f * 0.36f;

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

        if (size > MaxSize)
            size = MaxSize;

        if (size < MinSize)
            size = MinSize;

        float width = Mathf.Pow(size, 0.33f); 
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

    public void UpdateSizeAfterKill(float sizeOfKill) => increaseFullSize(Mathf.Min(0.8f * sizeOfKill, 0.512f));

    protected void updateSize() => size = fullSize * energy.NormalizedValue;

    private void increaseFullSize(float delta) => fullSize = Mathf.Min(fullSize + delta, MaxSize);
    private void decreaseFullSize(float delta) => fullSize = Mathf.Max(fullSize - delta, MinSize);

    private void resetSize()
    {
        fullSize = StartingSize;
        updateSize();
    }

    public int CompareTo(Size other)
    {
        if (size > other.size)
            return 1;
        else if (size == other.size)
            return 0;
        else
            return -1;
    }
}
