using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameEvents : MonoBehaviour //handles UI input
{
    private UIDocument UIDocGame; // references our main menu doc
    private VisualElement UIButton;
    private VisualElement UpgradeButton;
    private VisualElement StaffButton;
    private Label resourceLabel;
    public GameManager gameManager;
    public CreatureManager creatureManager;
    public ResourceManager resources;
    public UpgradeManager upgradeManager;

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

        UpgradeButton = root.Q("Upgrade1Button"); //grab clickzone
        UpgradeButton.RegisterCallback<UpgradeEvent>(OnUpgradeClick);

        StaffButton = root.Q("Staff1Button"); //grab clickzone
        StaffButton.RegisterCallback<ClickEvent>(OnStaffClick);
   }

   void OnCreatureClick(ClickEvent evt) 
   {
     playerCreatureClick?.Invoke(); //lab week 14
   }

   void OnUpgradeClick (UpgradeEvent evt)
   {
      upgradeManager.PurchaseUpgrade(evt.upgrade, resources);
   }

   void OnStaffClick (ClickEvent evt)
   {
      Debug.Log("<Color = Green> OnStaff! </Color");
      Creature randomC = creatureManager.RandomCreature(creatureManager.AllCreatures);
      CreatureManager.CreatureType random = randomC.type;//by no means efficient, but hey it was quick
      gameManager.HireCatcher(random);
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
