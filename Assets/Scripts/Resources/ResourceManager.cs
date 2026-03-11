using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Tracks all currencies with a central dictionary.
/// </summary>
public class ResourceManager : MonoBehaviour
{
    /*To-Do List
    - Track Currencies
    - Update Currencies
        - Taking in Currency and creatures from 
            -catch events
            -Markets
    - UI currency tracker
    */ 

    public enum ResourceType //for dictionary
    {
        Croins,TotalCreatures,RCreatures,BCreatures,GCreatures,Prestige
    }
    Dictionary<ResourceType, float> Resources = new Dictionary<ResourceType, float>() //dictionary of resources and their current amount
    {
        {ResourceType.Croins,0f}, //basic currency
        {ResourceType.TotalCreatures,0f}, //total amount of creatures
        {ResourceType.RCreatures,0f}, //total red creatures
        {ResourceType.BCreatures,0f}, //total blue creatures
        {ResourceType.GCreatures,0f}, //total green creaturess
        {ResourceType.Prestige,0f} //total prestige currency
    };
    //=================================
    // Public so other scripts can call these to update resources without touching the dictionary directly

    public void AddResource(ResourceType type, float amount)
    {
        if (Resources.ContainsKey(type))
            Resources[type] += amount;
    }

    public void SpendResource(ResourceType type, float amount)
    {
        if (Resources.ContainsKey(type))
            Resources[type] -= amount;
    }

    public float GetResource(ResourceType type)
    {
        if (Resources.ContainsKey(type))
            return Resources[type];
        return 0f;
    }
    
    // Returns a copy of so nothing outside this class can mess with the original dictionary
    public Dictionary<ResourceType, float> GetAllResources()
    {
        return new Dictionary<ResourceType, float>(Resources);
    }

    public void LoadResources(Dictionary<ResourceType, float> saved)
    {
        foreach (KeyValuePair<ResourceType, float> kvp in saved)
        {
            if (Resources.ContainsKey(kvp.Key))
                Resources[kvp.Key] = kvp.Value;
        }
    }

    public void ResetAll()
    {
        List<ResourceType> keys = new List<ResourceType>(Resources.Keys);
        for (int i = 0; i < keys.Count; i++)
            Resources[keys[i]] = 0f;
    }

    public void LogResources()
    {
        string output = "--- resources ---\n";
        foreach (KeyValuePair<ResourceType, float> kvp in Resources)
            output += kvp.Key + ": " + kvp.Value + "\n";
        Debug.Log(output);
    }
}
