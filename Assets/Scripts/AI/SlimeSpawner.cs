using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabSlime;
    [SerializeField] private GameObject playerSlime;
    [SerializeField] private Text leaderboardText;
    [SerializeField] private Text scoreText;

    private List<GameObject> slimes = new List<GameObject>();
    private List<Score> slimeScores = new List<Score>();

    private const int numOfSlimesOnMap = 130;

    void Start()
    {
        for (int i = 0; i < numOfSlimesOnMap; i++)
            slimes.Add(Instantiate(prefabSlime, Spawning.randomMapPosition(), Quaternion.identity, transform));

        slimes.Add(playerSlime);

        for (int i = 0; i < slimes.Count; i++)
            slimeScores.Add(slimes[i].transform.GetComponent<Score>());

        StartCoroutine(updateOrderedSlimeSizes());
    }

    private IEnumerator updateOrderedSlimeSizes()
    {
        slimeScores.Sort();

        StringBuilder text = new StringBuilder("");
        StringBuilder text2 = new StringBuilder("");
        for (int i = 1; i <= 10; i++)
        {
            text.Append($"{i}. " + slimeScores[slimeScores.Count - i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Name>().slimeName + "\n");
            text2.Append(slimeScores[slimeScores.Count - i].SlimeScore.ToString() + "\n");
        }

        leaderboardText.text = text.ToString();
        scoreText.text = text2.ToString();

        yield return new WaitForSeconds(1f);
        StartCoroutine(updateOrderedSlimeSizes());
    }

}
