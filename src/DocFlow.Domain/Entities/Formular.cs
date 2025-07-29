using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities;
public class Formular
{
    private readonly Type? _documentData;
    public required int Id { get; init; }

    public required Type DocumentData
    {
        get => _documentData ?? throw new InvalidDataException();
        
        init => _documentData = value;
    }

    public required Presentable Presentable { get; init; }
}
