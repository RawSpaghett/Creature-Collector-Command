using UnityEngine;
using System.Collections.Generic;

public partial class GameManager: MonoBehaviour
{
    public int redCatchers;
    public int greenCatchers;
    public int blueCatchers;
    public float cost = 500;
    

    public void HireCatcher(CreatureManager.CreatureType color)
    {
        Debug.Log("<Color=Green> HireCatcher </Color>");
        Generator catcher = null;

        switch (color)
        {
            case CreatureManager.CreatureType.RedCreature:
                if(TryHire(cost))
                {
                    catcher = new RedCatcher(resourceManager, upgradeManager, BASE_CATCH_INTERVAL,cost);
                    redCatchers += 1;
                }
                break;
            case CreatureManager.CreatureType.BlueCreature  :
                if(TryHire(cost))
                {
                    catcher = new BlueCatcher(resourceManager, upgradeManager, BASE_CATCH_INTERVAL,cost);
                    blueCatchers += 1;
                }
                break;
            case CreatureManager.CreatureType.GreenCreature:
                if(TryHire(cost))
                {
                    catcher = new GreenCatcher(resourceManager, upgradeManager, BASE_CATCH_INTERVAL,cost);
                    greenCatchers += 1;
                }
                break;
        }

        if (catcher != null)
        {
            generators.Add(catcher);
            Debug.Log("hired " + color + " catcher, total generators: " + generators.Count);
        }
    }

    public bool TryHire(float cost)
    {
        Debug.Log("StaffPurchaseAttempt");
        if(cost > resourceManager.GetResource(ResourceManager.ResourceType.Croins))
        {
            Debug.Log("Not Enough Croins");
            return false;
        }
        else 
        {
            resourceManager.SpendResource(ResourceManager.ResourceType.Croins, cost);
            ExponentialCost();
            return true;

        }
    }

    public void ExponentialCost()
    {
        cost *= Random.Range(2.0f,3.0f);
    }
}
