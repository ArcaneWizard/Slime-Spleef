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
        int green = (int) (energy.NormalizedEnergyValue * 255);
        int red = 255 - green;
        image.color = new Color32((byte)red, (byte)green, (byte)0, (byte)255);
    }
}
