using System.ComponentModel.DataAnnotations;

namespace DocFlow.Domain.Values;

public class AtBy
{
    public required DateTime At { get; init; }
    [MaxLength(80)]
    public required string By { get; init; }
}