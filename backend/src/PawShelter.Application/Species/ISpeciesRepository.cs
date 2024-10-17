using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.Entities;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Application.Species;

public interface ISpeciesRepository
{
    Task<Result<Domain.SpeciesManagement.Aggregate.Species, Error>> ExistSpecies(
        string speciesName, CancellationToken cancellationToken = default);

    public Task<Result<Breed, Error>> ExistBreed(
        string breedName,
        CancellationToken cancellationToken = default);

    public Task<Guid> Add(Domain.SpeciesManagement.Aggregate.Species species,
        CancellationToken cancellationToken = default);

    public Task<Guid> Save(Domain.SpeciesManagement.Aggregate.Species species, CancellationToken cancellationToken = default);
    
    public Task<Result<Domain.SpeciesManagement.Aggregate.Species, Error>> GetById(SpeciesId speciesId,
        CancellationToken cancellationToken = default);
}