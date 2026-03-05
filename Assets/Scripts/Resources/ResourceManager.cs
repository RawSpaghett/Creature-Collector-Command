using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    /*To-Do List
    - Track Currencies
    - Update Currencies
        - Taking in Currency and creatures from 
            -catch events
            -Markets
    - UI currency tracker
    */ 

    public enum ResourceType //for dictionary
    {
        Croins,TotalCreatures,RCreatures,BCreatures,GCreatures,Prestige
    }
    Dictionary<ResourceType, float> Resources = new Dictionary<ResourceType, float>() //dictionary of resources and their current amount
    {
        {ResourceType.Croins,0f}, //basic currency
        {ResourceType.TotalCreatures,0f}, //total amount of creatures
        {ResourceType.RCreatures,0f}, //total red creatures
        {ResourceType.BCreatures,0f}, //total blue creatures
        {ResourceType.GCreatures,0f}, //total green creaturess
        {ResourceType.Prestige,0f} //total prestige currency
    };

    //=================================


}
