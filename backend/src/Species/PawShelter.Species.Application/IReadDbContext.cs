using PawShelter.Core.Dto;
using PawShelter.Species.Contracts.Dto;

namespace PawShelter.Species.Application;

public interface IReadDbContext
{
    IQueryable<SpeciesDto> Species { get; }
    
    IQueryable<BreedDto> Breeds { get; }
}