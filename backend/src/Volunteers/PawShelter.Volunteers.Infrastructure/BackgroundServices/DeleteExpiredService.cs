using Microsoft.EntityFrameworkCore;
using PawShelter.Volunteers.Infrastructure.DbContexts;

namespace PawShelter.Volunteers.Infrastructure.BackgroundServices;

public class DeleteExpiredService
{
    private readonly WriteDbContext _writeDbContext;

    public DeleteExpiredService(WriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task Process(CancellationToken cancellationToken)
    {
        var volunteers = await _writeDbContext.Volunteers.
            Include(v => v.Pets).ToListAsync(cancellationToken);

        foreach (var volunteer in volunteers)
        {
            if(!volunteer.IsDeleted)
            {
                volunteer.Delete();
            }
            else
            {
                foreach (var pet in volunteer.Pets)
                {
                    volunteer.HardDeletePet(pet);
                }
            }
        }
    }
}