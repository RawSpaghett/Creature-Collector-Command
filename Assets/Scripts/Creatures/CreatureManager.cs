using UnityEngine;
using System.Collections.Generic;

public partial class CreatureManager : MonoBehaviour //handles creatures
{
    /*
    To-Do list
     - Handle creature spawn Chance via rarity
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
        Creature randomCreature = null;
        return randomCreature;
    }

    public void CreatureSpawn()
    {}

    public void CreatureDespawn()
    {}


}
