using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerEnergy : MonoBehaviour
{
    [SerializeField] private Energy energy;
    private Image image;

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
        int blue = (int)((4 * Mathf.Max((energy.NormalizedEnergyValue - 0.75f), 0)) * 255 );
        int green = (int) Mathf.Min((energy.NormalizedEnergyValue * 255 / 0.75f), 255);
        int red = 255 - green;
        image.color = new Color32((byte)red, (byte)green, (byte)blue, (byte)255);
    }
}
