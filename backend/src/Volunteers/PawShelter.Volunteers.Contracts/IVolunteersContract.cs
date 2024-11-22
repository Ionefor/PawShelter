using PawShelter.Core.Dto;
using PawShelter.Volunteers.Contracts.Dto.Models;

namespace PawShelter.Volunteers.Contracts;

public interface IVolunteersContract
{
    public Task<PetDto?> PetWithBreed(Guid breedId, CancellationToken cancellationToken);
    
    public Task<PetDto?> PetWithSpecies(Guid speciesId, CancellationToken cancellationToken);
    
}