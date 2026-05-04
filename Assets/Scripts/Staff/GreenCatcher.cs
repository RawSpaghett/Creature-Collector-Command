using UnityEngine;

/// <summary>
/// Handles passive green creature generation
/// </summary>
public class GreenCatcher : Generator
{
    public GreenCatcher(ResourceManager resourceManager, UpgradeManager upgradeManager, float interval, float cost) : base(resourceManager, upgradeManager, interval,cost)
    {
    }

    public override void Produce()
    {
        float amount = 1f;
        float multiplier = upgradeManager.GetMultiplier(Target.CatchRateG);
        resourceManager.ApplyBonus(ref amount, multiplier);
        amount = Mathf.Floor(amount);
        if (amount < 1f) amount = 1f;
        resourceManager.AddResource(ResourceManager.ResourceType.GCreatures, amount);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, amount);

        if (Random.Range(0f, 1f) <= 0.5f)
        {
            float croins = Mathf.Round(10f * upgradeManager.GetMultiplier(Target.GoldGain));
            resourceManager.AddResource(ResourceManager.ResourceType.Croins, croins);
        }

        Debug.Log("green catcher +" + amount);
    }
}