using DocFlow.Application.Persistence.GenericRepository;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;
public interface IDocumentRunner 
{
    Task<Result<RunSession, Exception>> RunActions(CancellationToken cancellationToken);
}
public interface IDocumentRunnerFactory
{
    IDocumentRunner BeginComputeSession(Document document);
    IDocumentRunner BeginFowrardSession(Document document, Channel channel);
    IDocumentRunner BeginRecallSession(ForwardSession forwardSession);
}
public class DocumentEngine(
    IDocumentRunnerFactory documentRunnerFactory,
    IRepository<RunSession, RunSessionId> sessions,
    IUnitOfWork unitOfWork) : IDocumentEngine
{
    public async Task<Result<RunSession, Exception>> ComputeAsync(Document document, CancellationToken cancellationToken)
    {

        var runner = documentRunnerFactory.BeginComputeSession(document);
        return await runner.RunActions(cancellationToken)
            .BindAsync(e=>sessions.Add(e))
            .BindAsync(e=>unitOfWork.SaveChangesAsync(e,cancellationToken));



    }


    public async Task<Result<RunSession, Exception>> ForwardAsync(Document document, Channel channel, CancellationToken cancellationToken)
    {
        var runner = documentRunnerFactory.BeginFowrardSession(document, channel);
        return await runner.RunActions(cancellationToken);
    }

    public async Task<Result<RunSession, Exception>> RecallAsync(ForwardSession forwardSession, CancellationToken cancellationToken)
    {
        var runner = documentRunnerFactory.BeginRecallSession(forwardSession);
        return await runner.RunActions(cancellationToken);
    }
}