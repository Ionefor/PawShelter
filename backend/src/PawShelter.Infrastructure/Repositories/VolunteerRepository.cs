using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.Application.Volunteers;
using PawShelter.Domain.PetsManagement.Aggregate;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Infrastructure.Repositories
{
    
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VolunteerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return volunteer.Id;
        }

        public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId)
        {
            var volunteer = await _dbContext.Volunteers.
                Include(v => v.Pets).
                FirstOrDefaultAsync(v => v.Id == volunteerId);

            if (volunteer is null)
                return Errors.General.NotFound(volunteerId);

            return volunteer;
        }
    }
}
