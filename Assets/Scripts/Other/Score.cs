using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int SlimeScore { get; private set; }

    void Start() => SlimeScore = 0;

    public void GainScoreFromFood(FoodPelletType type)
    {
        SlimeScore += (type == FoodPelletType.Normal) ? 5 : 25;
    }

    public void GainScoreFromKill(int deadSlimeScore)
    {
        SlimeScore += (int)Mathf.Min(1000, deadSlimeScore * 0.8f);
    }
}
