using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum UpgradeState
{
    Locked,
    Available,
    Purchased
}

public enum Target
{
    CatchRateR,
    CatchRateG,
    CatchRateB,
    GoldGain,
    PopulationDensityR,
    PopulationDensityG,
    PopulationDensityB
}

public struct UpgradeEffect //structure of the upgrade effects
{
    public float Multiplier;
    public Target Target;
    public UpgradeEffect ( float multiplier, Target target)
    {
        Multiplier = multiplier;
        Target = target;
    }
}
