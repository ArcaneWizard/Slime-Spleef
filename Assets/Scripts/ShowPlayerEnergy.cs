using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerEnergy : MonoBehaviour
{
    [SerializeField] private Energy energy;
    private Image image;

    private const float minPercentForBlue = 0.6f;
    private const float blueScale = 1f / (1f - minPercentForBlue);

    void Awake()
    {
        image = transform.GetComponent<Image>();
    }

    void Start()
    {
        image.fillAmount = energy.NormalizedEnergyValue;
    }

    void Update()
    {
        image.fillAmount = energy.NormalizedEnergyValue;
        updateEnergyColor();
    }

    private void updateEnergyColor()
    {
        float blueValue = Mathf.Max(energy.NormalizedEnergyValue - minPercentForBlue, 0);
        int blue = (int)(255 * blueScale * blueValue);

        float greenValue = energy.NormalizedEnergyValue * 255 / minPercentForBlue;
        int green = (int) Mathf.Min(greenValue, 255);

        int red = 255 - green;

        image.color = new Color32((byte)red, (byte)green, (byte)blue, (byte)255);
    }
}
