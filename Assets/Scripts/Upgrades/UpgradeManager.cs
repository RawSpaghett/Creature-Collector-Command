using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

/// <summary>
/// Holds all upgrades and handles purchasing and multiplier lookups.
/// 
/// Apr 17, changed everything to support dictionary instead of list
/// </summary>

public class UpgradeEvent:EventBase<UpgradeEvent> //allows for bubbling, accessing deep-nested data from the surface
{
    public Upgrade upgrade {get; protected set;} //prevents accidents in larger projects
    public static UpgradeEvent GetPooled(Upgrade upgradeEvt) //reuses and recycles the event to prevent memory leaks
    {
        var evt = EventBase<UpgradeEvent>.GetPooled();
        evt.upgrade = upgradeEvt;
        return evt;
    }
}
public class UpgradeManager : MonoBehaviour
{
    private Dictionary<string,Upgrade> Upgrades = new Dictionary<string,Upgrade>();
    public ResourceManager resourceManager;
 
    private void OnEnable()
    {
        InitializeUpgrades();
    }
 
    private void InitializeUpgrades()
    {
        Debug.Log("InitializeUpgrades");
 
        void AddUpgrade(Upgrade u) => Upgrades.Add(u.Name,u);
 
        // Each upgrade is rebuyable, cost scales with level
        AddUpgrade(new Upgrade("Red Catch Rate I", 50f, 1.4f, new UpgradeEffect(0.2f, Target.CatchRateR), 1, UpgradeState.Available));
        AddUpgrade(new Upgrade("Green Catch Rate I", 50f, 1.4f, new UpgradeEffect(0.2f, Target.CatchRateG), 1, UpgradeState.Available));
        AddUpgrade(new Upgrade("Blue Catch Rate I", 50f, 1.4f, new UpgradeEffect(0.2f, Target.CatchRateB), 1, UpgradeState.Available));
        AddUpgrade(new Upgrade("Gold Boost I", 75f, 1.5f, new UpgradeEffect(0.3f, Target.GoldGain), 1, UpgradeState.Available));
    }
 
    public Upgrade GetUpgrade(string name)
    {
        if (Upgrades.ContainsKey(name))
            return Upgrades[name];
        return null;
    }
 
    public List<string> GetPurchasedUpgrades()
    {
        List<string> data = new List<string>();
        foreach (var kvp in Upgrades)
        {
            if (kvp.Value.Level > 0)
                data.Add(kvp.Key + ":" + kvp.Value.Level);
        }
        return data;
    }
 
    // Returns total multiplier for a target based on upgrade level
    public float GetMultiplier(Target target)
    {
        float total = 1f;
        
        foreach (var upgrade in Upgrades.Values)
        {
            if (target == upgrade.Effect.Target && upgrade.Level > 0)
            {
                total += upgrade.Effect.Multiplier * upgrade.Level;
            }
        }
        return total;
    }
 
    public bool PurchaseAttempt(Upgrade upgrade,out string errorMessage)
    {
        Debug.Log("PurchaseAttempt");
        if(upgrade.Cost > resourceManager.GetResource(ResourceManager.ResourceType.Croins))
        {
            errorMessage = "Not Enough Croins!";
            return false;
        }
        errorMessage = string.Empty;
        return true;
    }
 
    public void PurchaseUpgrade(Upgrade upgrade)
    {
        Debug.Log("PurchaseUpgrade");
 
        if(PurchaseAttempt(upgrade, out string errorMessage))
        {
            resourceManager.SpendResource(ResourceManager.ResourceType.Croins, upgrade.Cost);
            upgrade.LevelUp();
            Debug.Log("purchased " + upgrade.Name + " lv" + upgrade.Level + ", " + upgrade.Effect.Target + " multiplier now " + GetMultiplier(upgrade.Effect.Target));
        }
        else
        {
            Debug.Log($"Error: {errorMessage}");
        }
    }
 
    // Flips all locked upgrades at the given tier to available
    public void UnlockTier(int tier)
    {
        Debug.Log("UnlockTier");
         foreach (var upgrade in Upgrades.Values)
        {
            if (upgrade.Tier == tier && upgrade.State == UpgradeState.Locked)
            {
                upgrade.SetState(UpgradeState.Available);
                Debug.Log("unlocked " + upgrade.Name);
            }
        }
    }
 
    public void LogUpgrades()
    {
        Debug.Log("LogUpgrades");
        string output = "--- upgrades ---\n";
        //print upgrades
        Debug.Log(output);
    }
 
    public void UpdateUpgradeUI()
    {
 
    }
}

    /* Davis: Commenting it in case i somehow mess everything up
    public class UpgradeManager : MonoBehaviour
{
    private Dictionary<string,Upgrade> Upgrades = new Dictionary<string,Upgrade>();
    public ResourceManager resourceManager;

    private void OnEnable()
    {
        InitializeUpgrades();
    }

    private void InitializeUpgrades() //hardcoded, alternative would use scriptable objects
    {
        Debug.Log("InitializeUpgrades");

        void AddUpgrade(Upgrade u) => Upgrades.Add(u.Name,u);

        // Tier 1
        AddUpgrade(new Upgrade("Red Catch Rate I", 50f, new UpgradeEffect(1.2f, Target.CatchRateR), 1, UpgradeState.Available));
        AddUpgrade(new Upgrade("Green Catch Rate I", 50f, new UpgradeEffect(1.2f, Target.CatchRateG), 1, UpgradeState.Available));
        AddUpgrade(new Upgrade("Blue Catch Rate I", 50f, new UpgradeEffect(1.2f, Target.CatchRateB), 1, UpgradeState.Available));
        AddUpgrade(new Upgrade("Gold Boost I", 75f, new UpgradeEffect(1.3f, Target.GoldGain), 1, UpgradeState.Available));

        // Tier 2
        AddUpgrade(new Upgrade("Red Catch Rate II", 200f, new UpgradeEffect(1.5f, Target.CatchRateR), 2, UpgradeState.Locked));
        AddUpgrade(new Upgrade("Green Catch Rate II", 200f, new UpgradeEffect(1.5f, Target.CatchRateG), 2, UpgradeState.Locked));
        AddUpgrade(new Upgrade("Blue Catch Rate II", 200f, new UpgradeEffect(1.5f, Target.CatchRateB), 2, UpgradeState.Locked));
        AddUpgrade(new Upgrade("Gold Boost II", 300f, new UpgradeEffect(1.6f, Target.GoldGain), 2, UpgradeState.Locked));
    }

    public Upgrade GetUpgrade(string name)
    {
        if (Upgrades.ContainsKey(name))
            return Upgrades[name];
        return null;
    }

    public List<string> GetPurchasedUpgrades()
    {
        List<string> names = new List<string>();
        foreach (var kvp in Upgrades)
        {
            if (kvp.Value.State == UpgradeState.Purchased)
                names.Add(kvp.Key);
        }
        return names;
    }

    // Stacks all purchased multipliers for a given target, returns 1 if nothing applies
    public float GetMultiplier(Target target)
    {
        Debug.Log("GetMultiplier");
        float total = 1f;
        
        foreach (var upgrade in Upgrades.Values)
        {
            if (upgrade.State == UpgradeState.Purchased && target == upgrade.Effect.Target)
            {
                total += upgrade.Effect.Multiplier;
            }
        }
        return total;
    }

    public bool PurchaseAttempt(Upgrade upgrade,out string errorMessage)
    {
        Debug.Log("PurchaseAttempt");
        if(upgrade.Cost > resourceManager.GetResource(ResourceManager.ResourceType.Croins))
        {
            errorMessage = "Not Enough Croins!";
            return false;
        }
        if (upgrade.State != UpgradeState.Available)
        {
            errorMessage = "Upgrade Locked!";
            return false;
        }
        errorMessage = string.Empty;
        return true;
    }

    public void PurchaseUpgrade(Upgrade upgrade)
    {
        Debug.Log("PurchaseUpgrade");

        if(PurchaseAttempt(upgrade, out string errorMessage))
        {
            resourceManager.SpendResource(ResourceManager.ResourceType.Croins, upgrade.Cost);
            upgrade.SetState(UpgradeState.Purchased);
            Debug.Log("purchased " + upgrade.Name + ", " + upgrade.Effect.Target + " multiplier now " + GetMultiplier(upgrade.Effect.Target));
        }
        else
        {
            Debug.Log($"Error: {errorMessage}");
        }
    }

    // Flips all locked upgrades at the given tier to available
    public void UnlockTier(int tier)
    {
        Debug.Log("UnlockTier");
         foreach (var upgrade in Upgrades.Values)
        {
            if (upgrade.Tier == tier && upgrade.State == UpgradeState.Locked)
            {
                upgrade.SetState(UpgradeState.Available);
                Debug.Log("unlocked " + upgrade.Name);
            }
        }
    }

    public void LogUpgrades()
    {
        Debug.Log("LogUpgrades");
        string output = "--- upgrades ---\n";
        //print upgrades
        Debug.Log(output);
    }

    public void UpdateUpgradeUI()
    {

    }
}
    */