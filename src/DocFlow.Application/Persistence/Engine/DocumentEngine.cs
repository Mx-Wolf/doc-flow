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

        return await Activate(
            document,
            e => actionLoop.RunActions(e, cancellationToken), 
            cancellationToken);

    }


    public async Task<Result<RunSession, Exception>> ForwardAsync(Document document, CancellationToken cancellationToken)
    {
        // TODO: Implement ForwardAsync logic
        return await Activate(
            document,
            e=>Task.FromResult(Result<RunSession,Exception>.Failure(new NotImplementedException())),
            cancellationToken);
    }

    public async Task<Result<RunSession, Exception>> RecallAsync(Document document, CancellationToken cancellationToken)
    {
        //TODO: Implement RecallAsync logic
        return await Activate(
             document,
             e => Task.FromResult(Result<RunSession, Exception>.Failure(new NotImplementedException())),
             cancellationToken);
    }
    private async Task<Result<RunSession, Exception>> Activate(
        Document document, 
        Func<RunSession, Task<Result<RunSession, Exception>>> runActionsCallback, 
        CancellationToken cancellationToken)
    {
        return await sessionFactory.Create(document, cancellationToken)
            .BindAsync(sessions.Add)
            .BindAsync(runActionsCallback)
            .BindAsync(e => unitOfWork.SaveChangesAsync(e, cancellationToken));
    }
}