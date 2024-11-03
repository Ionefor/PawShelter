using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.Application.Volunteers;
using PawShelter.Domain.PetsManagement.Aggregate;
using PawShelter.Domain.PetsManagement.Entities;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;
using PawShelter.Infrastructure.DbContexts;

namespace PawShelter.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly WriteDbContext _dbContext;

    public VolunteerRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        return volunteer.Id;
    }

    public Guid Save(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Attach(volunteer);
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
        var volunteer = await _dbContext.Volunteers.Include(v => v.Pets)
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