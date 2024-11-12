using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Application.Volunteers;
using PawShelter.Volunteers.Domain.Aggregate;
using PawShelter.Volunteers.Domain.Entities;
using PawShelter.Volunteers.Infrastructure.DbContexts;

namespace PawShelter.Volunteers.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly WriteDbContext _dbContext;

    public VolunteerRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Save(Volunteer volunteer)
    {
       
      
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
       await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return volunteer.Id;
    }
    
    public Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Remove(volunteer);
        return volunteer.Id;
    }
    
    public async Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers.
            Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(volunteerId);

        return volunteer;
    }

    public async Task<Result<Pet, Error>> GetPetById(
        PetId petId,
        CancellationToken cancellationToken = default)
    {
        var volunteers = _dbContext.Volunteers.Include(v => v.Pets);

        var volunteer = await volunteers.FirstOrDefaultAsync(v => v.Pets!.Any(p => p.Id == petId), cancellationToken);

        if (volunteer is null)
            return Error.Failure("volunteer.not.found", "volunteer not found");

        var pet = volunteer.Pets!.FirstOrDefault(p => p.Id == petId);

        if (pet is null)
            return Errors.General.NotFound(petId.Value);

        return pet;
    }
}