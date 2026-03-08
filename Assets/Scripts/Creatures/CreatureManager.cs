using UnityEngine;
using System.Collections.Generic;

public class CreatureManager : MonoBehaviour //handles creatures
{
    /*
    To-Do list
     - Handle Creature spawning and despawning
    */

    public List<Creature> AllCreatures = new List<Creature>();//for the ScriptableObjects
    public enum CreatureType
    {
        RedCreature,
        BlueCreature,
        GreenCreature
    }

    public enum CreatureRarity
    {
        Common,
        Rare,
        SuperRare,
        Legendary,
        Unheard
    }

    public Dictionary<CreatureRarity,float> RarityOdds = new Dictionary<CreatureRarity,float>()
    {
        {CreatureRarity.Common,70f},
        {CreatureRarity.Rare,15f},
        {CreatureRarity.SuperRare,5f},
        {CreatureRarity.Legendary,7f},
        {CreatureRarity.Unheard,3f}
    };

    public Creature RandomCreature(List<Creature> AllCreatures)
    {
        if (AllCreatures.Count == 0)
            return null;

        // Roll a rarity using the weighted odds
        float totalWeight = 0f;
        foreach (KeyValuePair<CreatureRarity, float> kvp in RarityOdds)
            totalWeight += kvp.Value;

        float roll = Random.Range(0f, totalWeight);
        float runningTotal = 0f;
        CreatureRarity chosenRarity = CreatureRarity.Common;

        foreach (KeyValuePair<CreatureRarity, float> kvp in RarityOdds)
        {
            runningTotal += kvp.Value;
            if (roll <= runningTotal)
            {
                chosenRarity = kvp.Key;
                break;
            }
        }

        // Grab only creatures that match the rolled rarity
        List<Creature> filtered = new List<Creature>();
        for (int i = 0; i < AllCreatures.Count; i++)
        {
            if (AllCreatures[i].rarity == chosenRarity)
                filtered.Add(AllCreatures[i]);
        }

        // Fallback if no creatures exist at that rarity
        if (filtered.Count == 0)
        {
            Debug.Log("no " + chosenRarity + " creatures, picking random");
            return AllCreatures[Random.Range(0, AllCreatures.Count)];
        }

        return filtered[Random.Range(0, filtered.Count)];
    }

    public void CreatureSpawn()
    {
        //uses random creature
        //change icon and name of UI to current creature
        
    }

    public void CreatureDespawn()
    {
        //strip the creature ui to default
    }
    
}