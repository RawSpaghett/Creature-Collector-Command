using UnityEngine;
using UnityEngine.UIElements;

private class ShopKeeper : MonoBehaviour
{
   public UpgradeManager upgradeManager;
   public VisualTreeAsset upgrade_UI_Template;
   public UIDocument UIDoc;

   private VisualElement root;


   void OnEnable()
   {
        root = UIDoc.rootVisualElement;
        GenerateUpgradeUI();
   }

   private void GenerateUpgradeUI()
   {
        //Instantiate upgrade panels for tier 1
   }


}
