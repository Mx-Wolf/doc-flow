namespace DocFlow.Application.Persistence.GenericRepository;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}