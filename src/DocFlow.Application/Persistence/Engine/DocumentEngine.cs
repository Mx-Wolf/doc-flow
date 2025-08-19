using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;
public class DocumentEngine(
    IDocumentRunnerFactory documentRunnerFactory,
    IRepository<RunSession, RunSessionId> sessions,
    IUnitOfWork unitOfWork) : IDocumentEngine
{
    public async Task<Result<RunSession, Exception>> ComputeAsync(Document document, CancellationToken cancellationToken)
    {

        return await RunAndPersistSessionAsync(
            documentRunnerFactory.BeginComputeSession(document),
            cancellationToken);

    }


    public async Task<Result<RunSession, Exception>> ForwardAsync(Document document, Channel channel, CancellationToken cancellationToken)
    {
        return await RunAndPersistSessionAsync(
            documentRunnerFactory.BeginFowrardSession(document, channel), 
            cancellationToken);
    }

    public async Task<Result<RunSession, Exception>> RecallAsync(ForwardSession forwardSession, CancellationToken cancellationToken)
    {
        return await RunAndPersistSessionAsync(
            documentRunnerFactory.BeginRecallSession(forwardSession),
            cancellationToken);
    }
    private async Task<Result<RunSession, Exception>> RunAndPersistSessionAsync(IDocumentRunner runner, CancellationToken cancellationToken)
    {
        return await runner.RunActions(cancellationToken)
            .BindAsync(sessions.Add)
            .BindAsync(e => unitOfWork.SaveChangesAsync(e, cancellationToken));
    }
}