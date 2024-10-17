using System.Data;

namespace PawShelter.Application;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default);
    
    Task SaveChanges(CancellationToken cancellationToken = default);
}