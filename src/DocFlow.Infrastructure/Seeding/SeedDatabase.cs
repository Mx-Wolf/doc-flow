using DocFlow.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DocFlow.Infrastructure.Seeding;

internal class SeedDatabase
{
    public static async Task SeedAsync(DbContext context, bool storeManaged, CancellationToken cancellationToken)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        var logger = context.GetService<ILogger<SeedDatabase>>();
        if(logger == null) throw new InvalidOperationException("Logger is not configured for SeedDatabase.");
        logger.BeginSeeding();
        await SeedFormulars.SeedAsync(context, storeManaged, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public static void Seed(DbContext context, bool storeManaged)
    {
        var task = SeedAsync(context, storeManaged, CancellationToken.None);
        task.Wait(); 
    }
}
