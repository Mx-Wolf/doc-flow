using System.ComponentModel.DataAnnotations;

namespace DocFlow.Domain.Values;

public class Presentable
{
    [MaxLength(80)]
    public required string Name { get; init; }

    [MaxLength(16)]
    public required string Code { get; init; }

    [MaxLength(16)]
    public required string Color { get; init; }

    public required int SequenceNumber { get; init; }

    public required bool IsEnabled { get; init; }
}
