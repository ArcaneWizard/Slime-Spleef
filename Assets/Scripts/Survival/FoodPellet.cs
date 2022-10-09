using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour
{
    private FoodPelletType type;
    private SpriteRenderer renderer;
    private float superFoodLikelihood = 0.075f;

    private static Color32 normal = new Color32(121, 243, 104, 255);
    private static Color32 super = new Color32(148, 136, 250, 255);

    void Awake() => renderer = transform.GetComponent<SpriteRenderer>();

    public void InitializeType()
    {
        float random = Random.Range(0, 1000);

        type = (random < 1000 * superFoodLikelihood) ? FoodPelletType.Super : FoodPelletType.Normal;
        renderer.color = (type == FoodPelletType.Super) ? super : normal;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // upon colliding with a slime, this pellet supplies the slime with energy, increases the slime's size,
        // and then this pellet disappears.
        if (col.gameObject.layer == 6)
        {
            col.transform.parent.GetComponent<Energy>().GainEnergyFromEating(type);
            col.transform.GetComponent<Size>().UpdateSizeAfterEatingFood(type);

            FoodPelletsSpawner.AddPelletAwaitingSpawn(gameObject);
            gameObject.SetActive(false);
        }
    }
}

public enum FoodPelletType
{
    Normal,
    Super
}