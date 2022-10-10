using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPellet : MonoBehaviour
{
    private FoodPelletType type;
    private SpriteRenderer renderer;
    private AudioSource source;
    private Collider2D col;

    private float superFoodLikelihood = 0.2f;

    private static Color32 normal = new Color32(121, 243, 104, 255);
    private static Color32 super = new Color32(148, 136, 250, 255);

    void Awake()
    {
        renderer = transform.GetComponent<SpriteRenderer>();
        source = transform.GetComponent<AudioSource>();
        col = transform.GetComponent<Collider2D>();
    }

    public void InitializeType()
    {
        float random = Random.Range(0, 1000);

        type = (random < 1000 * superFoodLikelihood) ? FoodPelletType.Super : FoodPelletType.Normal;
        renderer.color = (type == FoodPelletType.Super) ? super : normal;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // upon colliding with a slime, this pellet supplies the slime with energy, additional size, and additional score.
        if (col.gameObject.layer == 6)
        {
            col.transform.parent.GetComponent<Energy>().GainEnergyFromEating(type);
            col.transform.GetComponent<Size>().UpdateSizeAfterEatingFood(type);
            col.transform.parent.GetComponent<Score>().GainScoreFromFood(type);

            StartCoroutine(playAudioBeforeDisappearing(col.transform));
        }
    }

    private IEnumerator playAudioBeforeDisappearing(Transform slime)
    {
        col.enabled = false;
        renderer.enabled = false;

        if (slime.CompareTag("Player"))
        {
            source.clip = type == FoodPelletType.Super
                ? slime.parent.GetComponent<FoodAudioClips>().super
                : slime.parent.GetComponent<FoodAudioClips>().normal;

            source.Play();
            yield return new WaitForSeconds(0.25f);
        }
        else
            yield return new WaitForSeconds(0.01f);

        col.enabled = true;
        renderer.enabled = true;

        FoodPelletsSpawner.RespawnPellet(gameObject);
    }
}

public enum FoodPelletType
{
    Normal,
    Super
}