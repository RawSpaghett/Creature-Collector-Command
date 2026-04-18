using UnityEngine;

/// <summary>
/// Handles passive blue creature generation
/// </summary>
public class BlueCatcher : Generator
{
    public BlueCatcher(ResourceManager resourceManager, float interval) : base(resourceManager, interval)
    {
    }

    public override void Produce()
    {
        float amount = 1f;
        resourceManager.ApplyBonus(ref amount, 1f);
        resourceManager.AddResource(ResourceManager.ResourceType.BCreatures, amount);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, amount);
        Debug.Log("Blue catcher +" + amount);
    }
}