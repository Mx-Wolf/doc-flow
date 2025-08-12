using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public record StationDto(Formular Formular, Track Track, Station Station);
public record DocumentDto(Formular Formular, Track Traak, Station Station, Document Document, RunSession RunSession);
public interface IStationsRepository
{
    Task<Result<StationDto, Exception>> GetStationAsync(StationId stationId, CancellationToken cancellationToken);
}

public interface IDocumentEngine
{
    Task<Result<DocumentDto, Exception>> CreateDocumentAsync(
        StationDto startupStation,
        JsonObject body,
        CancellationToken cancellationToken);   
    Task<Result<DocumentKey, Exception>> UpdateDocumentAsync(
        DocumentDto document,
        CancellationToken cancellationToken);
}