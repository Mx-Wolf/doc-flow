namespace DocFlow.Application.Persistence.Engine;

public class SystemTime : ISystemTime
{
    public SystemTime()
    {
        ScopeStarted = DateTime.UtcNow;
    }
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime ScopeStarted { get; private set; }
}