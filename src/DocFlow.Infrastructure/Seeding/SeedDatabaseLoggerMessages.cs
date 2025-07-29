using Microsoft.Extensions.Logging;

namespace DocFlow.Infrastructure.Seeding;

internal static partial class SeedDatabaseLoggerMessages
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Begin Seeding")]
    public static partial void BeginSeeding(this ILogger logger);
}