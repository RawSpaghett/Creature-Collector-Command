using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

public class CreatureManager : MonoBehaviour //handles creatures
{
    /*
    To-Do list
     - Handle Creature spawning and despawning
    */
    //script references
    public GameManager gameManager;

    //Internal references
     private VisualElement creatureImage;
     private Label creatureLabel;
     private ProgressBar creatureProgressBar;
     private UIDocument UIDoc;
     public Creature activeCreature {get; private set;}


    public List<Creature> AllCreatures = new List<Creature>();//for the ScriptableObjects
    public enum CreatureType
    {
        RedCreature,
        BlueCreature,
        GreenCreature
    }

    public enum CreatureRarity
    {
        Common,
        Rare,
        SuperRare,
        Legendary,
        Unheard
    }

    public Dictionary<CreatureRarity,float> RarityOdds = new Dictionary<CreatureRarity,float>()
    {
        {CreatureRarity.Common,70f},
        {CreatureRarity.Rare,15f},
        {CreatureRarity.SuperRare,5f},
        {CreatureRarity.Legendary,7f},
        {CreatureRarity.Unheard,3f}
    };

    public Dictionary<CreatureRarity,string> RarityColors = new Dictionary<CreatureRarity,string>() //rarity hex codes
    {
        {CreatureRarity.Common,"#FFFFFF"},
        {CreatureRarity.Rare,"#4287f5"},
        {CreatureRarity.SuperRare,"#ff0000"},
        {CreatureRarity.Legendary,"#ff8000"},
        {CreatureRarity.Unheard,"#a335ee"}
    };

    void OnEnable()
   {
        Debug.Log("Creature OnEnable Go");
        UIDoc = GameObject.Find("GameUI").GetComponent<UIDocument>(); //grab UIDocument

        creatureImage = UIDoc.rootVisualElement.Q<Image>("CreatureImage"); //grab Image
        creatureProgressBar = UIDoc.rootVisualElement.Q<ProgressBar>("CatchProgress");
        creatureLabel = UIDoc.rootVisualElement.Q<Label>("CreatureLabel");
   }

    public Creature RandomCreature(List<Creature> AllCreatures)
    {
        Debug.Log("RandomCreature");
        if (AllCreatures.Count == 0)
            return null;

        // Roll a rarity using the weighted odds
        float totalWeight = 0f;
        foreach (KeyValuePair<CreatureRarity, float> kvp in RarityOdds)
            totalWeight += kvp.Value;

        float roll = Random.Range(0f, totalWeight);
        float runningTotal = 0f;
        CreatureRarity chosenRarity = CreatureRarity.Common;

        foreach (KeyValuePair<CreatureRarity, float> kvp in RarityOdds)
        {
            runningTotal += kvp.Value;
            if (roll <= runningTotal)
            {
                chosenRarity = kvp.Key;
                break;
            }
        }

        // Grab only creatures that match the rolled rarity
        List<Creature> filtered = new List<Creature>();
        for (int i = 0; i < AllCreatures.Count; i++)
        {
            if (AllCreatures[i].rarity == chosenRarity)
                filtered.Add(AllCreatures[i]);
        }

        // Fallback if no creatures exist at that rarity
        if (filtered.Count == 0)
        {
            Debug.Log("no " + chosenRarity + " creatures, picking random");
            return AllCreatures[Random.Range(0, AllCreatures.Count)];
        }

        return filtered[Random.Range(0, filtered.Count)];
    }

    public void CreatureSpawn()
    {
        CreatureDespawn();

        activeCreature = RandomCreature(AllCreatures);

        string hex = RarityColors[activeCreature.rarity]; //searches and stores hex code

        if (ColorUtility.TryParseHtmlString(hex, out Color labelColor)) //converts hex to actual color
        {
            creatureLabel.text = activeCreature.creatureName;
            creatureLabel.style.color = labelColor;
        }

        //Set image container as a proper frame
       Image frame = creatureImage as Image;
       frame.sprite = activeCreature.icon;


       //set progress bar
       creatureProgressBar.lowValue = 0f; //set low
       creatureProgressBar.value = 0f; //set current
       creatureProgressBar.highValue = activeCreature.CatchDifficulty; //set high
    }

    public void ProgressUpdate()
    {
        creatureProgressBar.title = ($"{gameManager.catchProgress}/{gameManager.progressNeeded}");
        creatureProgressBar.value = gameManager.catchProgress;
    }

    public void CreatureDespawn()
    {
        //wipe the slate clean
    }
}