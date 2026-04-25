using UnityEngine;

/// <summary>
/// Handles passive green creature generation
/// </summary>
public class GreenCatcher : Generator
{
    public GreenCatcher(ResourceManager resourceManager, UpgradeManager upgradeManager, float interval) : base(resourceManager, upgradeManager, interval)
    {
    }

    public override void Produce()
    {
        float amount = 1f;
        float multiplier = upgradeManager.GetMultiplier(Target.CatchRateG);
        resourceManager.ApplyBonus(ref amount, multiplier);
        resourceManager.AddResource(ResourceManager.ResourceType.GCreatures, amount);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, amount);
        Debug.Log("Green catcher +" + amount);
    }
}