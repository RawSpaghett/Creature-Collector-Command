using UnityEngine;

public class Upgrade
{
    public string Name {get; private set;}
    public float Cost {get; private set;}
    public UpgradeEffect Effect {get; private set;}
    public int Tier {get; private set;}
    public UpgradeState State {get; private set;}

    public Upgrade ( string name, float cost, UpgradeEffect effect, int tier, UpgradeState state)
    {
        Name = name;
        Cost = cost;
        Effect = effect;
        Tier = tier;
        State = state;
    }

    public void SetState(UpgradeState newState)
    {
        State = newState;
    }
}
