/// <summary>
/// A base class for anything that passively generates resources over time.
/// </summary>
public abstract class Generator
{
    public float Interval;
    public float Timer;
    public float Cost;

    protected ResourceManager resourceManager;
    protected UpgradeManager upgradeManager;

    public Generator(ResourceManager resourceManager, UpgradeManager upgradeManager, float interval, float cost)
    {
        this.resourceManager = resourceManager;
        this.upgradeManager = upgradeManager;
        Interval = interval;
        Timer = interval;
        Cost = cost;
    }

    // Each generator type decides what it produces
    public abstract void Produce();

    public void Tick(float deltaTime)
    {
        Timer -= deltaTime;
        if (Timer <= 0f)
        {
            Produce();
            Timer = Interval;
        }
    }
}