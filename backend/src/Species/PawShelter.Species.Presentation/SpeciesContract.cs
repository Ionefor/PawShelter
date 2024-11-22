using Microsoft.EntityFrameworkCore;
using PawShelter.Species.Application;
using PawShelter.Species.Contracts;
using PawShelter.Species.Contracts.Dto;

namespace PawShelter.Species.Presentation;

public class SpeciesContract(IReadDbContext context) : ISpeciesContract
{
    public Task<SpeciesDto?> SpeciesExists(
        string species, CancellationToken cancellationToken = default)
    {
       return context.Species.
           FirstOrDefaultAsync(s => s.Species == species, cancellationToken);
    }

    public Task<BreedDto?> BreedExists(
        string breed, CancellationToken cancellationToken = default)
    {
        return context.Breeds.
            FirstOrDefaultAsync(s => s.Breed == breed, cancellationToken);
    }
}