using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;

public record StationDto(Formular Formular, Track Track, Station Station);
public interface IStationsRepository
{
    Task<Result<StationDto, Exception>> GetStationAsync(StationId stationId, CancellationToken cancellationToken);
}

public interface IDocumentEngine
{
    Task<Result<DocumentKey,Exception>> CreateDocumentAsync(
        StationDto startupStation,
        JsonObject body,
        CancellationToken cancellationToken);   
}