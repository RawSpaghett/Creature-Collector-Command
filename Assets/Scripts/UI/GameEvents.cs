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
        
        UIButton = root.Q("CreatureClick"); //grab clickzone
        UIButton.RegisterCallback<ClickEvent>(OnCreatureClick);
   }

   void OnCreatureClick(ClickEvent evt) 
   {
     playerCreatureClick?.Invoke(); //lab week 14
   }

   void OnUpgradeClick (UpgradeEvent evt)
   {
      upgradeManager.PurchaseUpgrade(evt.upgrade, resources);
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
