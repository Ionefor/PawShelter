using PawShelter.Core.Dto;

namespace PawShelter.Species.Contracts;

public interface ISpeciesContract
{
    public Task<SpeciesDto?> SpeciesExists(string species, CancellationToken cancellationToken = default);
    
    public Task<BreedDto?> BreedExists(string breed, CancellationToken cancellationToken = default);
}