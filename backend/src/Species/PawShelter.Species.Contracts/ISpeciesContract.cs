using PawShelter.Core.Dto;
using PawShelter.Species.Contracts.Dto;

namespace PawShelter.Species.Contracts;

public interface ISpeciesContract
{
    public Task<SpeciesDto?> SpeciesExists(string species, CancellationToken cancellationToken = default);
    
    public Task<BreedDto?> BreedExists(string breed, CancellationToken cancellationToken = default);
}