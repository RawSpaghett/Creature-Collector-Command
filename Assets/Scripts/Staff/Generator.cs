/// <summary>
/// A base class for anything that passively generates resources over time.
/// </summary>
public abstract class Generator
{
    public float Interval;
    public float Timer;

    protected ResourceManager resourceManager;

    public Generator(ResourceManager resourceManager, float interval)
    {
        this.resourceManager = resourceManager;
        Interval = interval;
        Timer = interval;
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