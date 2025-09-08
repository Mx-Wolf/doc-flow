using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;
namespace DocFlow.Application.Persistence.Engine;

public interface IDocumentRunnerFactory
{
    Result<IDocumentRunner, Exception> BeginComputeSession(Document document);
    Result<IDocumentRunner, Exception> BeginForwardSession(Document document, Channel channel);
    Result<IDocumentRunner, Exception> BeginRecallSession(ForwardSession forwardSession);
}
