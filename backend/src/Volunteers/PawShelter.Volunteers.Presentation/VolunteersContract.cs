using Microsoft.EntityFrameworkCore;
using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application;
using PawShelter.Volunteers.Contracts;

namespace PawShelter.Volunteers.Presentation;

public class VolunteersContract(IReadDbContext context) : IVolunteersContract
{
    public async Task<PetDto?> PetWithBreed(
        Guid breedId, CancellationToken cancellationToken)
    {
        return await context.Pets.
            FirstOrDefaultAsync(
                p => p.BreedId == breedId, cancellationToken);
    }

    public async Task<PetDto?> PetWithSpecies(
        Guid speciesId, CancellationToken cancellationToken)
    {
       return await context.Pets.
            FirstOrDefaultAsync(
                p => p.SpeciesId == speciesId, cancellationToken);
    }
}