using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    public const float MaxSize = 1.5f * 1.5f;
    public const float MinSize = 0.3f * 0.3f;
    public const float StartingSize = 0.4f * 0.4f;

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

    protected void updateSize() => size = fullSize * (energy.NormalizedValue * 0.7f + 0.3f);

    private void increaseFullSize(float delta) => fullSize = Mathf.Min(fullSize + delta, MaxSize);
    private void decreaseFullSize(float delta) => fullSize = Mathf.Max(fullSize - delta, MinSize);

    private void resetSize()
    {
        fullSize = StartingSize;
        updateSize();
    }
}
