using UnityEngine;

public class Upgrade
{
    public string Name {get; private set;}
    public float Cost {get; private set;}
    public UpgradeEffect Effect {get; private set;}
    public int Tier {get; private set;}
    public UpgradeState State {get; private set;}
    public int Level {get; private set;}
    public float BaseCost {get; private set;}
    public float CostMultiplier {get; private set;}

    public Upgrade ( string name, float baseCost, float costMultiplier, UpgradeEffect effect, int tier, UpgradeState state)
    {
        Name = name;
        BaseCost = baseCost;
        CostMultiplier = costMultiplier;
        Cost = baseCost;
        Effect = effect;
        Tier = tier;
        State = state;
        Level = 0;
    }

    public float GetCost()
    {
        return Mathf.Round(BaseCost * Mathf.Pow(CostMultiplier, Level));
    }

    public void LevelUp()
    {
        Level++;
        Cost = GetCost();
    }

    public void SetState(UpgradeState newState)
    {
        State = newState;
    }

    public void SetLevel(int newLevel)
    {
        Level = newLevel;
        Cost = GetCost();
}
}