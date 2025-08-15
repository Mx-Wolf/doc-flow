using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;
public class DocumentEngine(
    IRunSessionFactory sessionFactory,
    IRepository<RunSession, RunSessionId> sessions,
    IActionLoop actionLoop,
    IUnitOfWork unitOfWork) : IDocumentEngine
{
    public async Task<Result<RunSession, Exception>> ComputeAsync(Document document, CancellationToken cancellationToken)
    {

        return await sessionFactory.Create(document, cancellationToken)
            .BindAsync(sessions.Add)
            .BindAsync(e => actionLoop.RunActions(e, cancellationToken))
            .BindAsync(e => unitOfWork.SaveChangesAsync(e, cancellationToken));

    }
    
}