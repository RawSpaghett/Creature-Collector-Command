using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameEvents : MonoBehaviour //handles UI input
{
   private UIDocument UIDocGame; // references our main menu doc
   private VisualElement UIButton;
   private Label resourceLabel;
   public GameManager gameManager;
   public CreatureManager creatureManager;
   public ResourceManager resources;
   public UpgradeManager upgradeManager;

//===================== NOT EFFICIENT
   private Button rUpgradeButton;
   private Button gUpgradeButton;
   private Button bUpgradeButton;
   private Button croinUpgradeButton;
   
   private Button rStaffButton;
   private Button gStaffButton;
   private Button bStaffButton;
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

      //By NO means efficient
      //Upgrade buttons
      rUpgradeButton = root.Q<Button>("rCatchButton"); 
      rUpgradeButton.clicked += () => {
         Upgrade upgrade = upgradeManager.GetUpgrade("Red Catch Rate I");
         upgradeManager.PurchaseUpgrade(upgrade);
         };

      gUpgradeButton = root.Q<Button>("bCatchButton"); 
      gUpgradeButton.clicked += () => {
         Upgrade upgrade = upgradeManager.GetUpgrade("Green Catch Rate I");
         upgradeManager.PurchaseUpgrade(upgrade);
         };

      bUpgradeButton = root.Q<Button>("gCatchButton"); 
      bUpgradeButton.clicked += () => {
         Upgrade upgrade = upgradeManager.GetUpgrade("Blue Catch Rate I");
         upgradeManager.PurchaseUpgrade(upgrade);
         };

      croinUpgradeButton = root.Q<Button>("croinBoostButton"); 
      croinUpgradeButton.clicked += () => {
         Upgrade upgrade = upgradeManager.GetUpgrade("Gold Boost I");
         upgradeManager.PurchaseUpgrade(upgrade);
         };

      //By NO means efficient
      //Staff buttons
      rStaffButton = root.Q<Button>("rCatcherButton"); 
      rStaffButton.clicked += () => gameManager.HireCatcher(CreatureManager.CreatureType.RedCreature);
      bStaffButton = root.Q<Button>("bCatcherButton"); 
      bStaffButton.clicked += () => gameManager.HireCatcher(CreatureManager.CreatureType.BlueCreature);
      gStaffButton = root.Q<Button>("gCatcherButton"); 
      gStaffButton.clicked += () => gameManager.HireCatcher(CreatureManager.CreatureType.GreenCreature);
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

   void Update()
   {
      

      //resources
      croinsLabel.text = ($"{resources.GetResource(ResourceManager.ResourceType.Croins).ToString()}");
      rCreatureLabel.text =($"{resources.GetResource(ResourceManager.ResourceType.RCreatures).ToString()}");
      bCreatureLabel.text =($"{resources.GetResource(ResourceManager.ResourceType.BCreatures).ToString()}");
      gCreatureLabel.text =($"{resources.GetResource(ResourceManager.ResourceType.GCreatures).ToString()}");

      //Upgrades

   }

}
