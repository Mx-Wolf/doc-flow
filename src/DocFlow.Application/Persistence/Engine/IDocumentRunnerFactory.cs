using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;

namespace DocFlow.Application.Persistence.Engine;

public interface IDocumentRunnerFactory
{
    IDocumentRunner BeginComputeSession(Document document);
    IDocumentRunner BeginFowrardSession(Document document, Channel channel);
    IDocumentRunner BeginRecallSession(ForwardSession forwardSession);
}
