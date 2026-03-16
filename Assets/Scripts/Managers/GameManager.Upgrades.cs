using UnityEngine;

// Upgrade logic moved to UpgradeManager, this just bridges the multipliers into the game systems
public partial class GameManager
{
    public UpgradeManager upgradeManager;

    // Takes a base value and scales it by whatever upgrades have been purchased for that target
    public float ApplyUpgrade(float baseValue, Target target)
    {
        return baseValue * upgradeManager.GetMultiplier(target);
    }

    //TODO: add upgrades with different upgrade types (science, military, conservation)
    //TODO: hook the luck bonus into RandomCreature rarity rolls
}