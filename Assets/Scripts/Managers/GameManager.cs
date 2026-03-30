using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Central game state. Connects catching, passive income, upgrades, staff, and the market.
/// </summary>
public partial class GameManager : MonoBehaviour
{
    public ResourceManager resourceManager;
    public CreatureManager creatureManager;

    private List<Generator> generators = new List<Generator>();
    private const float BASE_CATCH_INTERVAL = 5f;

    // Avoids a switch statement every time a color needs to map to a resource type
    private static readonly Dictionary<CreatureManager.CreatureType, ResourceManager.ResourceType> colorToResource =
        new Dictionary<CreatureManager.CreatureType, ResourceManager.ResourceType>()
    {
        { CreatureManager.CreatureType.RedCreature, ResourceManager.ResourceType.RCreatures },
        { CreatureManager.CreatureType.BlueCreature, ResourceManager.ResourceType.BCreatures },
        { CreatureManager.CreatureType.GreenCreature, ResourceManager.ResourceType.GCreatures }
    };

    // Maps color to its catch rate upgrade target
    private static readonly Dictionary<CreatureManager.CreatureType, Target> colorToCatchTarget =
        new Dictionary<CreatureManager.CreatureType, Target>()
    {
        { CreatureManager.CreatureType.RedCreature, Target.CatchRateR },
        { CreatureManager.CreatureType.BlueCreature, Target.CatchRateB },
        { CreatureManager.CreatureType.GreenCreature, Target.CatchRateG }
    };

    private void Start()
    {
        resourceManager.LogResources();
        StartNewCatch();
        // Temporary catchers for testing passive income
        HireCatcher(CreatureManager.CreatureType.RedCreature);
        HireCatcher(CreatureManager.CreatureType.BlueCreature);
    }

    private void Update()
    {
        TickGenerators(Time.deltaTime);
    }

    private void TickGenerators(float deltaTime)
    {
        for (int i = 0; i < generators.Count; i++)
            generators[i].Tick(deltaTime);
    }

    public void HireCatcher(CreatureManager.CreatureType color)
    {
        Generator catcher = null;

        switch (color)
        {
            case CreatureManager.CreatureType.RedCreature:
                catcher = new RedCatcher(resourceManager, BASE_CATCH_INTERVAL);
                break;
            case CreatureManager.CreatureType.BlueCreature:
                catcher = new BlueCatcher(resourceManager, BASE_CATCH_INTERVAL);
                break;
            case CreatureManager.CreatureType.GreenCreature:
                catcher = new GreenCatcher(resourceManager, BASE_CATCH_INTERVAL);
                break;
        }

        if (catcher != null)
        {
            generators.Add(catcher);
            Debug.Log("hired " + color + " catcher, total generators: " + generators.Count);
        }
        //TODO: add cost and scaling for hiring catchers
    }
}