using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;
public interface IStationsRepository
{
    Task<Result<Station, Exception>> GetStationAsync(StationId stationId, CancellationToken cancellationToken);
}
