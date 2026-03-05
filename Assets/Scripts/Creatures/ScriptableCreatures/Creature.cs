using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Scriptable Objects/Creature")]
public class Creature : ScriptableObject
{
    [Header ("Basics")]
    public string creatureName;
    public string description;
    public CreatureManager.CreatureRarity rarity;
    public CreatureManager.CreatureType type;

    [Header ("Stats")]
    public float CatchDifficulty;
    public float EscapeChance;
    public float CroinWorth;
    public float CroinChance;

    [Header ("Visuals")]
    public Sprite icon;
    
}
