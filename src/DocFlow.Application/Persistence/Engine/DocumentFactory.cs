using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Entities.StateMachine.State;
using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.Engine;
public class DocumentFactory(
    ISequenceSource sequenceSource) : IDocumentFactory
{
    private static readonly Type OpenDocumentType = typeof(DocumentData<>);
    private static readonly PropertyInfo IdProp = typeof(Document).GetProperty(nameof(Document.Id)) 
        ?? throw new InvalidOperationException("Document.Id property not found.");
    private static readonly PropertyInfo FormularProp = typeof(Document).GetProperty(nameof(Document.FormularId)) 
        ?? throw new InvalidOperationException("Document.FormularId property not found.");
    private static readonly PropertyInfo TrackProp = typeof(Document).GetProperty(nameof(Document.TrackId)) 
        ?? throw new InvalidOperationException("Document.TrackId property not found.");
    private static readonly PropertyInfo StationProp = typeof(Document).GetProperty(nameof(Document.StationId)) 
        ?? throw new InvalidOperationException("Document.StationId property not found.");

    public async Task<Result<Document,Exception>> CreateFromJson(Station station, JsonObject rowData, CancellationToken cancellationToken)
    {
        // Implementation for creating a Document from JSON data
        var tData = JsonSerializer.Deserialize(rowData.ToJsonString(), station.Track.Formular.DocumentData);
        var id = new DocumentId(await sequenceSource.GetNextSequenceAsync(cancellationToken));
        var instanceType = OpenDocumentType.MakeGenericType(station.Track.Formular.DocumentData);
        PropertyInfo dataProp = instanceType.GetProperty(nameof(DocumentData<object>.Data))?? throw new InvalidOperationException("DocumentData.Data property not found.");
        var instance = Activator.CreateInstance(instanceType);

        IdProp.SetValue(instance, id);
        FormularProp.SetValue(instance, station.Track.Formular.Id);
        TrackProp.SetValue(instance, station.Track.Id);
        StationProp.SetValue(instance, station.Id);

        dataProp.SetValue(instance, tData);

        return instance is Document document 
            ? Result<Document, Exception>.Success(document) 
            : Result<Document, Exception>.Failure(new InvalidOperationException("Deserialized object is not a Document."));

    }
}
