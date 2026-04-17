using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

//for any other project I wouldve used Newtonsoft, but gotta learn a foundation
//And surely there is a better way than ResourceManager.ResourceType.TotalCreatures,state.totalCreatures
public class SaveHandler: MonoBehaviour
{
    private string _dataPath;
    private string _jsonFile;
    public ResourceManager ResourceManager;

    void Awake() //from the slides
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(_dataPath);
    
        if(!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
            Debug.Log("New directory created!");
        }
        _jsonFile = _dataPath + "Player_Data.json";

        if(File.Exists(_jsonFile))
        {
            LoadData();
        }
    }

    void OnApplicationQuit()
    {
        try
        {
            SaveData state = new SaveData
            {
                croins = ResourceManager.GetResource(ResourceManager.ResourceType.Croins),
                rCreatures = ResourceManager.GetResource(ResourceManager.ResourceType.RCreatures),
                bCreatures = ResourceManager.GetResource(ResourceManager.ResourceType.BCreatures),
                gCreatures = ResourceManager.GetResource(ResourceManager.ResourceType.GCreatures),
                totalCreatures = ResourceManager.GetResource(ResourceManager.ResourceType.TotalCreatures)
            };

            string json = JsonUtility.ToJson(state, true);
            File.WriteAllText(_jsonFile, json);

            Debug.Log("<color=green>session data saved</color>");
        }
        catch(Exception e)
        {
             Debug.LogError("<Color=Red> Critical OnApplicationQuit Error!</Color>");
        }
    }

    void LoadData()
    {
        try
        {
            ResourceManager.ResetAll();
            string json = File.ReadAllText(_jsonFile);
            SaveData state = JsonUtility.FromJson<SaveData>(json);
            ResourceManager.AddResource(ResourceManager.ResourceType.Croins,state.croins);
            ResourceManager.AddResource(ResourceManager.ResourceType.RCreatures,state.rCreatures);
            ResourceManager.AddResource(ResourceManager.ResourceType.BCreatures,state.bCreatures);
            ResourceManager.AddResource(ResourceManager.ResourceType.GCreatures,state.gCreatures);
            ResourceManager.AddResource(ResourceManager.ResourceType.TotalCreatures,state.totalCreatures);
            Debug.Log("<Color=Green>Save Data Loaded</Color>");
        }
        catch (Exception e)
        {
            Debug.LogError("<Color=Red> Critical LoadData Error!</Color>");
        }

    }

}

[System.Serializable]
public class SaveData
{
    //resources
    public float croins;
    public float rCreatures;
    public float bCreatures;
    public float gCreatures;
    public float totalCreatures;

    //upgrades

}

