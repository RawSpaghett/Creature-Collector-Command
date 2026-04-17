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
        resourceManager.LogResources();
        StartNewCatch();


        /* Temporary catchers for testing passive income
        HireCatcher(CreatureManager.CreatureType.RedCreature);
        HireCatcher(CreatureManager.CreatureType.BlueCreature);
        */
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
}