using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PawShelter.Accounts.Infrastructure.DbContexts;
using PawShelter.Core.Abstractions;

namespace PawShelter.Accounts.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AccountDbContext _dbContext;

    public UnitOfWork(AccountDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}