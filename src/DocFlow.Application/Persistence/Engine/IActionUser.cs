using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public interface IActionUser
{
    Task<AtBy> GetUserStamp(CancellationToken cancellationToken);
}

