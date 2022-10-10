using UnityEngine;

public class Energy : MonoBehaviour
{
    public float EnergyValue { get; private set; }

    private const float maxEnergyValue = 110f; // a slime's energy ranges from 0 to the maxEnergyValue. 
    private const float depletionRate = 7f; // the amount of energy the slime loses every second

    private const float energyPerNormalPellet = 8f;
    private const float energyPerSuperPellet = 24f;
    private const float energyUsedToThrow = 15f;
    private const float energyDrainedByPuddle = 0.06f;

    private Spawning spawning;
    private GeneralDeath generalDeath;

    void Awake()
    {
        spawning = transform.GetComponent<Spawning>();
        generalDeath = transform.GetComponent<GeneralDeath>();
    }

    void Start()
    {
        EnergyValue = 0f;

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

    public void GainEnergyFromEating(FoodPelletType type)
    {
        float gain = (type == FoodPelletType.Normal) ? energyPerNormalPellet : energyPerSuperPellet;
        ChangeEnergy(gain);
    }

    public void LoseEnergyFromThrowing() => ChangeEnergy(-energyUsedToThrow);

    public void LoseEnergyFromPuddle() => ChangeEnergy(-energyDrainedByPuddle);

    private void ChangeEnergy(float delta) => EnergyValue = Mathf.Min(EnergyValue + delta, maxEnergyValue); 

    private void resetEnergyLvls() => EnergyValue = maxEnergyValue;
}