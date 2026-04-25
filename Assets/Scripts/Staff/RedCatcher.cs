using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Handles passive red creature generation
/// </summary>
public class RedCatcher : Generator
{
    public RedCatcher(ResourceManager resourceManager, UpgradeManager upgradeManager, float interval) : base(resourceManager, upgradeManager, interval)
    {
    }

    public override void Produce()
    {
        float amount = 1f;
        float multiplier = upgradeManager.GetMultiplier(Target.CatchRateR);
        resourceManager.ApplyBonus(ref amount, multiplier);
        resourceManager.AddResource(ResourceManager.ResourceType.RCreatures, amount);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, amount);
        Debug.Log("Red catcher +" + amount);
    }
    // TODO: plug all the generators into the upgrade system
}