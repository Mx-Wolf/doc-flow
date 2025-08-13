using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;

namespace DocFlow.Application.Persistence.Engine;

public record DocumentDto(Formular Formular, Track Traak, Station Station, Document Document, RunSession RunSession);
