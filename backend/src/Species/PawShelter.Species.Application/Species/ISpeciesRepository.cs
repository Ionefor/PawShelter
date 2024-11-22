using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Species.Domain.Entities;

namespace PawShelter.Species.Application.Species;

public interface ISpeciesRepository
{
    Task<Result<PawShelter.Species.Domain.Aggregate.Species, Error>> ExistSpecies(
        string speciesName, CancellationToken cancellationToken = default);

    public Task<Result<Breed, Error>> ExistBreed(
        string breedName,
        CancellationToken cancellationToken = default);

    public Task<Guid> Add(PawShelter.Species.Domain.Aggregate.Species species,
        CancellationToken cancellationToken = default);

    public Task<Result<PawShelter.Species.Domain.Aggregate.Species, Error>> GetById(SpeciesId speciesId,
        CancellationToken cancellationToken = default);
    
    public void DeleteSpecies(PawShelter.Species.Domain.Aggregate.Species species);
}