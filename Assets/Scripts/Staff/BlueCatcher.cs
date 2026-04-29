using UnityEngine;

/// <summary>
/// Handles passive blue creature generation
/// </summary>
public class BlueCatcher : Generator
{
    public BlueCatcher(ResourceManager resourceManager, UpgradeManager upgradeManager, float interval, float cost) : base(resourceManager, upgradeManager, interval,cost)
    {
    }

    public override void Produce()
    {
        float amount = 1f;
        float multiplier = upgradeManager.GetMultiplier(Target.CatchRateB);
        resourceManager.ApplyBonus(ref amount, multiplier);
        resourceManager.AddResource(ResourceManager.ResourceType.BCreatures, amount);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, amount);
        Debug.Log("Blue catcher +" + amount);
    }
}