using UnityEngine;

/// <summary>
/// Progress bar catch system. Each click fills the bar, rarer creatures need more clicks.
/// </summary>
public partial class GameManager
{
    public Creature currentCreature;
    public float catchProgress;
    public float progressNeeded;
    public bool catchActive;

    private float GetRarityMultiplier(CreatureManager.CreatureRarity rarity)
    {
        switch (rarity)
        {
            case CreatureManager.CreatureRarity.Common: return 1f;
            case CreatureManager.CreatureRarity.Rare: return 2.5f;
            case CreatureManager.CreatureRarity.SuperRare: return 5f;
            case CreatureManager.CreatureRarity.Legendary: return 10f;
            case CreatureManager.CreatureRarity.Unheard: return 25f;
            default: return 1f;
        }
    }

    // Picks a random creature and sets the catch requirements based on rarity.
    public void StartNewCatch()
    {
        currentCreature = creatureManager.RandomCreature(creatureManager.AllCreatures);
        if (currentCreature == null)
        {
            Debug.Log("no creatures in pool");
            return;
        }

        catchProgress = 0f;
        progressNeeded = currentCreature.CatchDifficulty * GetRarityMultiplier(currentCreature.rarity);
        catchActive = true;
        Debug.Log("catching: " + currentCreature.creatureName + ", need " + progressNeeded);
    }

    // Called by GameEvents when the player clicks
    public void OnCatchClick()
    {
        if (!catchActive || currentCreature == null)
            return;

        catchProgress += clickPower;
        Debug.Log("click +" + clickPower + ", " + catchProgress + "/" + progressNeeded);
        // TODO: still need to finalize formula 

        if (catchProgress >= progressNeeded)
            CompleteCatch();
    }

    public void SkipCreature()
    {
        if (!catchActive)
            return;

        Debug.Log("skipped " + currentCreature.creatureName);
        catchActive = false;
        StartNewCatch();
    }
    //TODO: tell the UI to update the creature info

    private void CompleteCatch()
    {
        catchActive = false;
        Debug.Log("caught " + currentCreature.creatureName);

        // Add to the right color bucket using the lookup dictionary
        if (colorToResource.ContainsKey(currentCreature.type))
            resourceManager.AddResource(colorToResource[currentCreature.type], 1f);
        resourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures, 1f);

        // Croin drop based on creature's chance + player bonus
        float croinChance = currentCreature.CroinChance + croinChanceBonus;
        if (Random.Range(0f, 1f) <= croinChance)
        {
            float croinDrop = currentCreature.CroinWorth * (1f + croinAmountBonus);
            resourceManager.AddResource(ResourceManager.ResourceType.Croins, croinDrop);
            Debug.Log("+" + croinDrop + " croins");
        }

        // Pheromone drop used for prestige currency
        if (Random.Range(0f, 1f) <= 0.001f)
        {
            resourceManager.AddResource(ResourceManager.ResourceType.Prestige, 1f);
            Debug.Log("pheromone drop!");
        }
        //TODO: add the chance that the creature escapes after filling the progress bar

        resourceManager.LogResources();
        StartNewCatch();
    }
}