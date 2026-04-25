using UnityEngine;
using System.Collections.Generic;

public partial class GameManager: MonoBehaviour
{
    public void HireCatcher(CreatureManager.CreatureType color)
    {
        Debug.Log("<Color=Green> HireCatcher </Color>");
        Generator catcher = null;

        switch (color)
        {
            case CreatureManager.CreatureType.RedCreature:
                catcher = new RedCatcher(resourceManager, upgradeManager, BASE_CATCH_INTERVAL);
                break;
            case CreatureManager.CreatureType.BlueCreature:
                catcher = new BlueCatcher(resourceManager, upgradeManager, BASE_CATCH_INTERVAL);
                break;
            case CreatureManager.CreatureType.GreenCreature:
                catcher = new GreenCatcher(resourceManager, upgradeManager, BASE_CATCH_INTERVAL);
                break;
        }

        if (catcher != null)
        {
            generators.Add(catcher);
            Debug.Log("hired " + color + " catcher, total generators: " + generators.Count);
        }
        //TODO: add cost and scaling for hiring catchers
    }
}
