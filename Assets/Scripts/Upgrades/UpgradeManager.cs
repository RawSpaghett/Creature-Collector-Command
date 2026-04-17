using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Holds all upgrades and handles purchasing and multiplier lookups.
/// </summary>
public class UpgradeManager : MonoBehaviour
{
    private List<Upgrade> Upgrades = new List<Upgrade>();
    public ResourceManager resourceManager;

    private void Start()
    {
        InitializeUpgrades();
        LogUpgrades();
    }

    private void InitializeUpgrades()
    {
        Debug.Log("InitializeUpgrades");
        // Tier 1, available from the start
        Upgrades.Add(new Upgrade("Red Catch Rate I", 50f,
            new UpgradeEffect(1.2f, Target.CatchRateR), 1, UpgradeState.Available));
        Upgrades.Add(new Upgrade("Green Catch Rate I", 50f,
            new UpgradeEffect(1.2f, Target.CatchRateG), 1, UpgradeState.Available));
        Upgrades.Add(new Upgrade("Blue Catch Rate I", 50f,
            new UpgradeEffect(1.2f, Target.CatchRateB), 1, UpgradeState.Available));
        Upgrades.Add(new Upgrade("Gold Boost I", 75f,
            new UpgradeEffect(1.3f, Target.GoldGain), 1, UpgradeState.Available));

        // Tier 2, locked until player progresses
        Upgrades.Add(new Upgrade("Red Catch Rate II", 200f,
            new UpgradeEffect(1.5f, Target.CatchRateR), 2, UpgradeState.Locked));
        Upgrades.Add(new Upgrade("Green Catch Rate II", 200f,
            new UpgradeEffect(1.5f, Target.CatchRateG), 2, UpgradeState.Locked));
        Upgrades.Add(new Upgrade("Blue Catch Rate II", 200f,
            new UpgradeEffect(1.5f, Target.CatchRateB), 2, UpgradeState.Locked));
        Upgrades.Add(new Upgrade("Gold Boost II", 300f,
            new UpgradeEffect(1.6f, Target.GoldGain), 2, UpgradeState.Locked));
    }

    // Stacks all purchased multipliers for a given target, returns 1 if nothing applies
    public float GetMultiplier(Target target)
    {
        Debug.Log("GetMultiplier");
        float total = 1f;
        for (int i = 0; i < Upgrades.Count; i++)
        {
            if (Upgrades[i].State == UpgradeState.Purchased && Upgrades[i].Effect.Target == target)
                total *= Upgrades[i].Effect.Multiplier;
        }
        return total;
    }

    public bool PurchaseAttempt(Upgrade upgrade, ResourceManager resourceManager, out string errorMessage)
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

    public void PurchaseUpgrade(int index, ResourceManager resourceManager)
    {
        Debug.Log("PurchaseUpgrade");
        if (index < 0 || index >= Upgrades.Count)
            return;

        Upgrade upgrade = Upgrades[index];

        if(PurchaseAttempt(upgrade,resourceManager, out string errorMessage))
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
        for (int i = 0; i < Upgrades.Count; i++)
        {
            if (Upgrades[i].Tier == tier && Upgrades[i].State == UpgradeState.Locked)
            {
                Upgrades[i].SetState(UpgradeState.Available);
                Debug.Log("unlocked " + Upgrades[i].Name);
            }
        }
    }

    public void LogUpgrades()
    {
        Debug.Log("LogUpgrades");
        string output = "--- upgrades ---\n";
        for (int i = 0; i < Upgrades.Count; i++)
            output += i + ": " + Upgrades[i].Name + " [" + Upgrades[i].State + "] t" + Upgrades[i].Tier + " $" + Upgrades[i].Cost + "\n";
        Debug.Log(output);
    }
}