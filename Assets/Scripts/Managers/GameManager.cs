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

    // save manager writes these in awake then start reads them to restore state
    [HideInInspector] public List<string> savedUpgrades = new List<string>();
    [HideInInspector] public int savedRedCatchers;
    [HideInInspector] public int savedBlueCatchers;
    [HideInInspector] public int savedGreenCatchers;


    private void OnEnable()
    {
        GameEvents.playerCreatureClick += OnCatchClick; //subscribe
    }

    private void OnDisable()
    {
        GameEvents.playerCreatureClick -= OnCatchClick; //unsubscribe
    }

    
    private void Start()
    {
        // retore upgrades and catchers from save
        for(int i = 0; i < savedUpgrades.Count; i++)
        {
            Upgrade upgrade = upgradeManager.GetUpgrade(savedUpgrades[i]);
            if(upgrade != null)
                upgrade.SetState(UpgradeState.Purchased);
        }
        for(int i = 0; i < savedRedCatchers; i++)
            generators.Add(new RedCatcher(resourceManager, upgradeManager, BASE_CATCH_INTERVAL, cost));
        for(int i = 0; i < savedBlueCatchers; i++)
            generators.Add(new BlueCatcher(resourceManager, upgradeManager, BASE_CATCH_INTERVAL, cost));
        for(int i = 0; i < savedGreenCatchers; i++)
            generators.Add(new GreenCatcher(resourceManager, upgradeManager, BASE_CATCH_INTERVAL, cost));
        resourceManager.LogResources();
        StartNewCatch();
    }

    private void Update()
    {
        creatureManager.ProgressUpdate();
        TickGenerators(Time.deltaTime);
    }

    private void TickGenerators(float deltaTime)
    {
        for (int i = 0; i < generators.Count; i++)
            generators[i].Tick(deltaTime);
    }

    public int GetCatcherCount(CreatureManager.CreatureType color)
    {
        int count = 0;
        for (int i = 0; i < generators.Count; i++)
        {
            if (color == CreatureManager.CreatureType.RedCreature && generators[i] is RedCatcher)
                count++;
            else if (color == CreatureManager.CreatureType.BlueCreature && generators[i] is BlueCatcher)
                count++;
            else if (color == CreatureManager.CreatureType.GreenCreature && generators[i] is GreenCatcher)
                count++;
        }
        return count;
    }
}