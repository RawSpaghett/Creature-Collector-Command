using UnityEngine;
using System.Collections.Generic;

// Upgrade list and their effects
public partial class GameManager
{
    public List<Upgrade> availableUpgrades = new List<Upgrade>();

    // Player stats modified by upgrades
    public float clickPower = 1f;
    public float croinChanceBonus = 0f;
    public float croinAmountBonus = 0f;
    public float luckBonus = 0f;

    private void InitializeUpgrades()
    {
        availableUpgrades.Add(new Upgrade("Click Power", 50f, 1.4f, UpgradeEffect.ClickPower));
        availableUpgrades.Add(new Upgrade("Croin Chance", 100f, 1.5f, UpgradeEffect.CroinChance));
        availableUpgrades.Add(new Upgrade("Croin Amount", 150f, 1.5f, UpgradeEffect.CroinAmount));
        availableUpgrades.Add(new Upgrade("Luck", 200f, 1.6f, UpgradeEffect.Luck));
    }

    //TODO: add upgrades with different upgrade types (science, military, conservation)
    public void PurchaseUpgrade(int index)
    {
        if (index < 0 || index >= availableUpgrades.Count)
            return;

        Upgrade upgrade = availableUpgrades[index];
        float cost = upgrade.GetCost();

        if (resourceManager.GetResource(ResourceManager.ResourceType.Croins) < cost)
        {
            Debug.Log("can't afford " + upgrade.Name);
            return;
        }

        resourceManager.SpendResource(ResourceManager.ResourceType.Croins, cost);
        upgrade.CurrentLevel++;
        ApplyUpgrade(upgrade);
        Debug.Log(upgrade.Name + " now lv" + upgrade.CurrentLevel);
    }

    private void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.Effect)
        {
            case UpgradeEffect.ClickPower:
                clickPower = 1f + (upgrade.CurrentLevel * 0.5f);
                break;
            case UpgradeEffect.CroinChance:
                croinChanceBonus = upgrade.CurrentLevel * 0.02f;
                break;
            case UpgradeEffect.CroinAmount:
                croinAmountBonus = upgrade.CurrentLevel * 0.1f;
                break;
            case UpgradeEffect.Luck:
                luckBonus = upgrade.CurrentLevel * 0.01f;
                break;
        }
    }
    //TODO: hook the luck bonus into RandomCreature rarity rolls
}