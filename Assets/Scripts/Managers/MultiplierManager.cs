using UnityEngine;

/// <summary>
/// Catch chance and cost scaling math.
/// </summary>


public class MultiplierManager: MonoBehaviour
{
    private const float BASE_CATCH_RATE = 0.7f;
    private const float BASE_POWER = 1f;
    public UpgradeManager upgradeManager;

    public float CalculateCatchChance(float catchDifficulty, float catchBonus)
    {
        float chance = BASE_CATCH_RATE - catchDifficulty + catchBonus;

        // Clamp so nothing is impossible or guaranteed to catch
        if (chance < 0.05f)
            chance = 0.05f;
        if (chance > 0.95f)
            chance = 0.95f;

        return chance;
    }

    public float CalculateClickPower(Target target)
    {
        float rand = Random.Range(0.25f,2f);
        float click;
        click = BASE_POWER * upgradeManager.GetMultiplier(target) * rand;
        return click;
    }
 
}