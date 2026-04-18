using UnityEngine;

/// <summary>
/// Handles passive green creature generation
/// </summary>
public class GreenCatcher : Generator
{
    public GreenCatcher(ResourceManager resourceManager, float interval) : base(resourceManager, interval)
    {
    }

    public override void Produce()
    {
        float amount = 1f;
        resourceManager.ApplyBonus(ref amount, 1f);
        resourceManager.AddResource(ResourceManager.ResourceType.GCreatures, amount);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, amount);
        Debug.Log("Green catcher +" + amount);
    }
}