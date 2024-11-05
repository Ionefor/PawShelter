using System.Data;

namespace PawShelter.Application.Database;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    
    void SaveChanges();
}