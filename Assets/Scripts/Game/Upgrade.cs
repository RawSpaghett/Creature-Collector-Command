/// <summary>
/// Purchasable upgrade. Cost scales exponentially per upgrade.
/// </summary>
[System.Serializable]
public class Upgrade
{
    public string Name;
    public float BaseCost;
    public float CostMultiplier;
    public int CurrentLevel;
    public UpgradeEffect Effect;

    public Upgrade(string name, float baseCost, float costMultiplier, UpgradeEffect effect)
    {
        Name = name;
        BaseCost = baseCost;
        CostMultiplier = costMultiplier;
        Effect = effect;
        CurrentLevel = 0;
    }

    // base cost * multiplier ^ current level so costs more each purchase
    public float GetCost()
    {
        return BaseCost * UnityEngine.Mathf.Pow(CostMultiplier, CurrentLevel);
    }
}

public enum UpgradeEffect
{
    ClickPower,
    CroinChance,
    CroinAmount,
    Luck
}