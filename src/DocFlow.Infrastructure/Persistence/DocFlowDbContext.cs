using Microsoft.EntityFrameworkCore;

namespace DocFlow.Infrastructure.Persistence;
public class DocFlowDbContext(DbContextOptions<DocFlowDbContext> options): DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("df");
        
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Infrastructure);

        base.OnModelCreating(modelBuilder);
    }
}
