namespace DocFlow.Application.Persistence.Engine;

public interface ISequenceSource
{
    Task<int> GetNextSequenceAsync(CancellationToken cancellationToken);
    Task<Guid> GetNextGuidAsync(CancellationToken cancellationToken);
}
