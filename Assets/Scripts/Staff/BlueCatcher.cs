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
        amount = Mathf.Floor(amount);
        if (amount < 1f) amount = 1f;
        resourceManager.AddResource(ResourceManager.ResourceType.BCreatures, amount);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, amount);

        if (Random.Range(0f, 1f) <= 0.5f)
        {
            float croins = Mathf.Round(10f * upgradeManager.GetMultiplier(Target.GoldGain));
            resourceManager.AddResource(ResourceManager.ResourceType.Croins, croins);
        }

        Debug.Log("blue catcher +" + amount);
    }
}