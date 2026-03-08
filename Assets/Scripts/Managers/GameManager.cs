using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Central game state. Connects catching, passive income, upgrades, staff, and the market.
/// </summary>
public partial class GameManager : MonoBehaviour
{
    public ResourceManager resourceManager;
    public CreatureManager creatureManager;

    // Color catchers for passive income, count per color
    public Dictionary<CreatureManager.CreatureType, int> catchers = new Dictionary<CreatureManager.CreatureType, int>()
    {
        { CreatureManager.CreatureType.RedCreature, 0 },
        { CreatureManager.CreatureType.BlueCreature, 0 },
        { CreatureManager.CreatureType.GreenCreature, 0 }
    };

    private Dictionary<CreatureManager.CreatureType, float> catchTimers = new Dictionary<CreatureManager.CreatureType, float>();
    private const float BASE_CATCH_INTERVAL = 5f;
    //TODO: scale catcher cost with upgrades and catcher level

    // Avoids a switch statement every time a color needs to map to a resource type
    private static readonly Dictionary<CreatureManager.CreatureType, ResourceManager.ResourceType> colorToResource =
        new Dictionary<CreatureManager.CreatureType, ResourceManager.ResourceType>()
    {
        { CreatureManager.CreatureType.RedCreature, ResourceManager.ResourceType.RCreatures },
        { CreatureManager.CreatureType.BlueCreature, ResourceManager.ResourceType.BCreatures },
        { CreatureManager.CreatureType.GreenCreature, ResourceManager.ResourceType.GCreatures }
    };

    private void Start()
    {
        InitializeUpgrades();

        // Set all catcher timers to the base interval
        foreach (CreatureManager.CreatureType color in catchers.Keys)
            catchTimers[color] = BASE_CATCH_INTERVAL;

        resourceManager.LogResources();
        StartNewCatch();
        // Temporary catchers for testing passive income and for the video 
        HireCatcher(CreatureManager.CreatureType.RedCreature);
        HireCatcher(CreatureManager.CreatureType.BlueCreature);
    }

    private void Update()
    {
        TickCatchers(Time.deltaTime);
    }

    /// <summary>
    /// Loops through each color's catchers and ticks their timers independently.
    /// </summary>
    private void TickCatchers(float deltaTime)
    {
        List<CreatureManager.CreatureType> colors = new List<CreatureManager.CreatureType>(catchers.Keys);

        for (int i = 0; i < colors.Count; i++)
        {
            CreatureManager.CreatureType color = colors[i];

            if (catchers[color] <= 0)
                continue;

            catchTimers[color] -= deltaTime;
            if (catchTimers[color] <= 0f)
            {
                float creatures = catchers[color] * 1f;
                resourceManager.AddResource(colorToResource[color], creatures);
                resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, creatures);
                catchTimers[color] = BASE_CATCH_INTERVAL;
                Debug.Log(color + " catchers +" + creatures);
            }
        }
    }

    public void HireCatcher(CreatureManager.CreatureType color)
    {
        if (catchers.ContainsKey(color))
            catchers[color]++;
      // TODO: add cost and scaling for hiring catchers
      // TODO: hook catchers into the full catch system so they can have rarity rolls

        Debug.Log("hired " + color + " catcher, total " + catchers[color]);
    }
}