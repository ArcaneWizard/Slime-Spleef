using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour, IComparable<Score>
{
    public int SlimeScore;

    void Start() => SlimeScore = 0;

    public void GainScoreFromFood(FoodPelletType type)
    {
        SlimeScore += (type == FoodPelletType.Normal) ? 5 : 25;
    }

    public void GainScoreFromKill(int deadSlimeScore)
    {
        SlimeScore += (int)Mathf.Min(1000, deadSlimeScore * 0.8f);
    }

    public int CompareTo(Score other)
    {
        if (SlimeScore > other.SlimeScore)
            return 1;
        else if (SlimeScore == other.SlimeScore)
            return 0;
        else
            return -1;
    }
}
