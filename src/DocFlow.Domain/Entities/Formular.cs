using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities;
public class Formular
{
    public required int Id { get; init; }
    public required Presentable Presentable { get; init; }
}
