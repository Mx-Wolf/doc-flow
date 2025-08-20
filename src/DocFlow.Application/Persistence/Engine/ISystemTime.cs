namespace DocFlow.Application.Persistence.Engine;

public interface ISystemTime
{
    DateTime ScopeStarted { get; }
    DateTime UtcNow { get; }
}
