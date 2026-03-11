using UnityEngine;

/// <summary>
/// Catch chance and cost scaling math.
/// </summary>
public static class MultiplierManager
{
    private const float BASE_CATCH_RATE = 0.7f;

    public static float CalculateCatchChance(float catchDifficulty, float catchBonus)
    {
        float chance = BASE_CATCH_RATE - catchDifficulty + catchBonus;

        // Clamp so nothing is impossible or guaranteed to catch
        if (chance < 0.05f)
            chance = 0.05f;
        if (chance > 0.95f)
            chance = 0.95f;

        return chance;
    }
    //TODO: market price scaling
    //TODO: factor in luck from upgrades
}