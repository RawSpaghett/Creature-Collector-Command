using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;

//the most difficult script in the entire project
public class GameEvents : MonoBehaviour //handles UI input
{
   private UIDocument UIDocGame; // references our main menu doc
   private VisualElement UIButton;
   private Label resourceLabel;
   public GameManager gameManager;
   public CreatureManager creatureManager;
   public ResourceManager resources;
   public UpgradeManager upgradeManager;
   private Upgrade upgrade;

   //=====================

    public static event Action playerCreatureClick;

    private Label croinsLabel;
    private Label rCreatureLabel;
    private Label bCreatureLabel;
    private Label gCreatureLabel;

   
   void OnEnable()
   {
      UIDocGame = GameObject.Find("GameUI").GetComponent<UIDocument>(); //grab UIDocument
      VisualElement root = UIDocGame.rootVisualElement;

      root.RegisterCallback<UpgradeEvent>(OnUpgradeClick); //to listen to upgrade bubbles

      //grab each resource label
      croinsLabel = root.Q<Label>("croins");
      rCreatureLabel = root.Q<Label>("rCreatures");
      bCreatureLabel = root.Q<Label>("bCreatures");
      gCreatureLabel = root.Q<Label>("gCreatures");
        
      UIButton = root.Q("ClickZone"); //grab clickzone
      UIButton.RegisterCallback<ClickEvent>(OnCreatureClick);

      SetupButton("rCatchButton","rCatchLabel","Red Catch Rate I");

      SetupButton("gCatchButton","gCatchLabel","Green Catch Rate I");
      
      SetupButton("bCatchButton","bCatchLabel","Blue Catch Rate I");

      SetupButton("croinBoostButton","croinBoostLabel","Gold Boost I");
      
      //====== Staff ====
      SetupButton("rCatcherButton","rCatcherLabel",
      null,
      () => {
         gameManager.HireCatcher(CreatureManager.CreatureType.RedCreature);
         return gameManager.redCatchers.ToString();
      });

      SetupButton("bCatcherButton","bCatcherLabel",
      null,
      () => {
         gameManager.HireCatcher(CreatureManager.CreatureType.BlueCreature);
         return gameManager.blueCatchers.ToString();
      });

      SetupButton("gCatcherButton","gCatcherLabel",
      null,
      () => {
         gameManager.HireCatcher(CreatureManager.CreatureType.GreenCreature);
         return gameManager.greenCatchers.ToString();
      });
   }

   void SetupButton(string buttonName, string labelName,string upgradeName = null, System.Func<string> customClickLogic = null) //a bit wonky
   {
      VisualElement root = UIDocGame.rootVisualElement;
      Button button = root.Q<Button>(buttonName);
      Label label = root.Q<Label>(labelName);

      //Upgrades
      if(upgradeName != null)
      {
         Upgrade upgrade = upgradeManager.GetUpgrade(upgradeName);
         button.text = upgrade.Cost.ToString();
         label.text = upgrade.State.ToString(); //available

          button.clicked += () => 
         {
            upgradeManager.PurchaseUpgrade(upgrade);
            label.text = upgrade.State.ToString(); //unavailable
         };
      }
      //staff
      else
      {
         button.clicked += () => 
        {
            label.text = customClickLogic.Invoke(); //runs staff lambda
        };
      }
   }



   void OnCreatureClick(ClickEvent evt) 
   {
     playerCreatureClick?.Invoke(); //lab week 14
   }

   void OnUpgradeClick (UpgradeEvent evt)
   {
      Debug.Log("<Color = Green> OnUpgrade! </Color");
      upgradeManager.PurchaseUpgrade(evt.upgrade);
   }

   void OnStaffClick (ClickEvent evt)
   {
      Debug.Log("<Color = Green> OnStaff! </Color");
      
   }

   void Update() //performace murderer
   {

      //resources
      croinsLabel.text = ($"{resources.GetResource(ResourceManager.ResourceType.Croins).ToString()}");
      rCreatureLabel.text =($"{resources.GetResource(ResourceManager.ResourceType.RCreatures).ToString()}");
      bCreatureLabel.text =($"{resources.GetResource(ResourceManager.ResourceType.BCreatures).ToString()}");
      gCreatureLabel.text =($"{resources.GetResource(ResourceManager.ResourceType.GCreatures).ToString()}");
   }

}
