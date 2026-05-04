using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Handles passive red creature generation
/// </summary>
public class RedCatcher : Generator
{
    public RedCatcher(ResourceManager resourceManager, UpgradeManager upgradeManager, float interval, float cost) : base(resourceManager, upgradeManager, interval,cost)
    {
    }

    public override void Produce()
    {
        float amount = 1f;
        float multiplier = upgradeManager.GetMultiplier(Target.CatchRateR);
        resourceManager.ApplyBonus(ref amount, multiplier);
        amount = Mathf.Floor(amount);
        if (amount < 1f) amount = 1f;
        resourceManager.AddResource(ResourceManager.ResourceType.RCreatures, amount);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, amount);

        // croin roll on each passive catch
        if (Random.Range(0f, 1f) <= 0.5f)
        {
            float croins = Mathf.Round(10f * upgradeManager.GetMultiplier(Target.GoldGain));
            resourceManager.AddResource(ResourceManager.ResourceType.Croins, croins);
        }

        Debug.Log("red catcher +" + amount);
    }
}