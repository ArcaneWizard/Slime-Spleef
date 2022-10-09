using UnityEngine;

public class Energy : MonoBehaviour
{
    private float EnergyValue;

    private const float maxEnergyValue = 110f; // a slime's energy ranges from 0 to the maxEnergyValue. 
    private const float depletionRate = 7f; // the amount of energy the slime loses every second

    private Spawning spawning;
    private GeneralDeath generalDeath;

    void Awake()
    {
        spawning = transform.GetComponent<Spawning>();
        generalDeath = transform.GetComponent<GeneralDeath>();
    }

    void Start()
    {
        spawning.OnNewSpawn += resetEnergyLvls;
        resetEnergyLvls();
    }

    void Update()
    {
        if (generalDeath.IsDead)
            return;

        if (EnergyValue > 0)
            EnergyValue -= Time.deltaTime * depletionRate;

        if (EnergyValue < 0)
            EnergyValue = 0;

        if (EnergyValue > maxEnergyValue)
            EnergyValue = maxEnergyValue;
    }

    // returns a range from 0-1 depending on how full of energy the Slime is.
    // A Slime is full once it's energy is 100 or more.
    public float NormalizedValue => Mathf.Min(1f, EnergyValue / 100f);

    public void GainEnergy(float gain) => EnergyValue = Mathf.Min(EnergyValue + gain, maxEnergyValue); 

    private void resetEnergyLvls() => EnergyValue = maxEnergyValue;
}