using UnityEngine;

/// <summary>
/// Handles passive red creature generation
/// </summary>
public class RedCatcher : Generator
{
    public RedCatcher(ResourceManager resourceManager, float interval) : base(resourceManager, interval)
    {
    }

    public override void Produce()
    {
        float amount = 1f;
        resourceManager.ApplyBonus(ref amount, 1f);
        resourceManager.AddResource(ResourceManager.ResourceType.RCreatures, amount);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, amount);
        Debug.Log("Red catcher +" + amount);
    }
    // TODO: plug all the generators into the upgrade system
}