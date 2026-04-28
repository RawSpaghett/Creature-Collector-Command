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
   private Upgrade upgrade;

//===================== NOT EFFICIENT
   private Button rUpgradeButton;
   private Label rUpgradeLabel;
   private Button gUpgradeButton;
   private Label gUpgradeLabel;
   private Button bUpgradeButton;
   private Label bUpgradeLabel;
   private Button croinUpgradeButton;
   private Label croinsUpgradeLabel;
   
   private Button rStaffButton;
   private Label rStaffCount;

   private Button gStaffButton;
   private Label gStaffCount;

   private Button bStaffButton;
   private Label bStaffCount;
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
      //Upgrade buttons and labels
      rUpgradeButton = root.Q<Button>("rCatchButton");  //red
      rUpgradeButton.clicked += () => {
         Upgrade upgrade = upgradeManager.GetUpgrade("Red Catch Rate I");
         upgradeManager.PurchaseUpgrade(upgrade);
         };
      rUpgradeLabel = root.Q<Label>("rCatchLabel");

      gUpgradeButton = root.Q<Button>("gCatchButton"); //green
      gUpgradeButton.clicked += () => {
         Upgrade upgrade = upgradeManager.GetUpgrade("Green Catch Rate I");
         upgradeManager.PurchaseUpgrade(upgrade);
         };
      gUpgradeLabel = root.Q<Label>("gCatchLabel");

      bUpgradeButton = root.Q<Button>("bCatchButton"); //blue
      bUpgradeButton.clicked += () => {
         Upgrade upgrade = upgradeManager.GetUpgrade("Blue Catch Rate I");
         upgradeManager.PurchaseUpgrade(upgrade);
         };
      bUpgradeLabel = root.Q<Label>("bCatchLabel");

      croinUpgradeButton = root.Q<Button>("croinBoostButton"); //croins
      croinUpgradeButton.clicked += () => {
         Upgrade upgrade = upgradeManager.GetUpgrade("Gold Boost I");
         upgradeManager.PurchaseUpgrade(upgrade);
         };
      croinsUpgradeLabel = root.Q<Label>("croinBoostLabel");

      //By NO means efficient
      //Staff buttons and labels
      rStaffButton = root.Q<Button>("rCatcherButton"); 
      rStaffButton.clicked += () => gameManager.HireCatcher(CreatureManager.CreatureType.RedCreature);
      rStaffCount = root.Q<Label>("rCatcherLabel");

      bStaffButton = root.Q<Button>("bCatcherButton"); 
      bStaffButton.clicked += () => gameManager.HireCatcher(CreatureManager.CreatureType.BlueCreature);
      bStaffCount = root.Q<Label>("bCatcherLabel");

      gStaffButton = root.Q<Button>("gCatcherButton"); 
      gStaffButton.clicked += () => gameManager.HireCatcher(CreatureManager.CreatureType.GreenCreature);
      gStaffCount = root.Q<Label>("gCatcherLabel");
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
      upgrade = upgradeManager.GetUpgrade("Red Catch Rate I");
      rUpgradeLabel.text = ($"{upgrade.State.ToString()}");
      upgrade = upgradeManager.GetUpgrade("Green Catch Rate I");
      gUpgradeLabel.text = ($"{upgrade.State.ToString()}");
      upgrade = upgradeManager.GetUpgrade("Blue Catch Rate I");
      bUpgradeLabel.text = ($"{upgrade.State.ToString()}");
      upgrade = upgradeManager.GetUpgrade("Gold Boost I");
      croinsUpgradeLabel.text = ($"{upgrade.State.ToString()}");

      //Staff
      rStaffCount.text = ($"{gameManager.redCatchers.ToString()}");
      gStaffCount.text = ($"{gameManager.greenCatchers.ToString()}");
      bStaffCount.text = ($"{gameManager.blueCatchers.ToString()}");
   }

}
