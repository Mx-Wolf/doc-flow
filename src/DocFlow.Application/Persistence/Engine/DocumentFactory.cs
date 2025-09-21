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
    private static readonly PropertyInfo StationProp = typeof(Document).GetProperty(nameof(Document.Station)) 
        ?? throw new InvalidOperationException("Document.StationId property not found.");

    public async Task<Result<Document,Exception>> CreateFromJson(Station station, JsonObject rowData, CancellationToken cancellationToken)
    {
        // Implementation for creating a Document from JSON data
        var id = new DocumentId(await sequenceSource.GetNextSequenceAsync(cancellationToken));
        var instanceType = OpenDocumentType.MakeGenericType(station.Track.Formular.DocumentData);
        var instance = Activator.CreateInstance(instanceType) as Document ?? throw new InvalidOperationException();

        IdProp.SetValue(instance, id);
        FormularProp.SetValue(instance, station.Track.Formular.Id);
        TrackProp.SetValue(instance, station.Track.Id);
        StationProp.SetValue(instance, station);

        SetDocumentData(
            instance, 
            rowData,
            GetDataPropInfo(instanceType));

        return instance is Document document
            ? Result<Document, Exception>.Success(document)
            : Result<Document, Exception>.Failure(new InvalidOperationException("Deserialized object is not a Document."));

    }

    private static PropertyInfo GetDataPropInfo(Type instanceType)
    {
        
        return instanceType.GetProperty(nameof(DocumentData<object>.Data),
            BindingFlags.FlattenHierarchy
            | BindingFlags.Instance 
            | BindingFlags.Public
            | BindingFlags.NonPublic) ?? throw new InvalidOperationException("DocumentData.Data property not found.");
    }

    public async Task<Result<Document, Exception>> PatchFromJson(Document document, JsonObject? patch, CancellationToken cancellationToken)
    {
        if (patch == null) return await Task.FromResult(Result<Document, Exception>.Success(document));
        PropertyInfo dataProp = GetDataPropInfo(document.GetType());
        var type = document.Station.Track.Formular.DocumentData;
        var target = dataProp.GetValue(document, null);
        return target != null ? PatchDocumentData(document, patch, type, target) : SetDocumentData(document, patch, dataProp);
    }

    private static Result<Document, Exception> SetDocumentData(Document document, JsonObject patch, PropertyInfo dataProp)
    {
        var setter = dataProp.GetSetMethod(true);
        var tData = JsonSerializer.Deserialize(patch.ToJsonString(), document.Station.Track.Formular.DocumentData);
        setter?.Invoke(document, [tData]);
        return Result<Document, Exception>.Success(document);
    }

    private static Result<Document, Exception> PatchDocumentData(Document document, JsonObject patch, Type type, object target)
    {
        foreach (var kvp in patch)
        {
            var prop = type.GetProperty(kvp.Key, BindingFlags.Public | BindingFlags.Instance);
            if (prop == null || !prop.CanWrite) continue;

            var jsonValue = kvp.Value;

            // Explicit null: set property to null if it's a reference type or nullable
            if (jsonValue != null && jsonValue.GetValueKind() == JsonValueKind.Null)
            {
                if (!prop.PropertyType.IsValueType || Nullable.GetUnderlyingType(prop.PropertyType) != null)
                {
                    prop.SetValue(target, null);
                }
                continue;
            }

            // Non-null: deserialize and assign
            var value = jsonValue.Deserialize(prop.PropertyType);
            prop.SetValue(target, value);
        }
        return Result<Document, Exception>.Success(document);
    }
}
