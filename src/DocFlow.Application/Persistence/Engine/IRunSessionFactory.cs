using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public interface IRunSessionFactory
{
    Task<Result<RunSession, Exception>> Create(Document document, CancellationToken cancellationToken);
}