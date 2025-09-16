using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;
public class DocumentEngine(
    IDocumentRunnerFactory documentRunnerFactory,
    IRepository<RunSession, RunSessionId> sessions) : IDocumentEngine
{
    public async Task<Result<RunSession, Exception>> ComputeAsync(Document document, CancellationToken cancellationToken) 
        => await documentRunnerFactory.BeginComputeSession(document)
            .BindAsync(p => RunAndPersistSessionAsync(p, cancellationToken));


    public async Task<Result<RunSession, Exception>> ForwardAsync(Document document, Channel channel, CancellationToken cancellationToken)
        => await documentRunnerFactory.BeginForwardSession(document, channel)
            .BindAsync(s => RunAndPersistSessionAsync(s, cancellationToken));

    public async Task<Result<RunSession, Exception>> RecallAsync(ForwardSession forwardSession, CancellationToken cancellationToken)
        => await documentRunnerFactory.BeginRecallSession(forwardSession)
            .BindAsync(r => RunAndPersistSessionAsync(r, cancellationToken));
    private async Task<Result<RunSession, Exception>> RunAndPersistSessionAsync(IDocumentRunner runner, CancellationToken cancellationToken) 
        => await runner.RunActions(cancellationToken)
            .BindAsync(sessions.Add);
}