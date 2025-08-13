namespace DocFlow.Application.Persistence.Engine;

public interface ISequenceSource
{
       Task<int> GetNextSequenceAsync(CancellationToken cancellationToken);
}
