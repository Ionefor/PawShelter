﻿using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Species.Application.Species;
using PawShelter.Species.Domain.Entities;
using PawShelter.Species.Infrastructure.DbContexts;

namespace PawShelter.Species.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _dbContext;

    public SpeciesRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Species.Domain.Aggregate.Species, Error>> ExistSpecies(
        string speciesName,
        CancellationToken cancellationToken = default)
    {
        var species = await _dbContext.Species.Include(b => b.Breeds).FirstOrDefaultAsync(
            s => s.Value == speciesName, cancellationToken);

        if (species == null)
            return Error.NotFound("Species.not.found", "Species not found");

        return species;
    }

    public async Task<Result<Breed, Error>> ExistBreed(
        string breedName,
        CancellationToken cancellationToken = default)
    {
        var breed = await _dbContext.Species.SelectMany(s => s.Breeds)
            .FirstOrDefaultAsync(b => b.Value == breedName, cancellationToken);

        if (breed == null)
            return Error.NotFound("Breed.not.found", "Breed not found");

        return breed;
    }
    
    public async Task<Guid> Add(Domain.Aggregate.Species species, CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return species.Id.Value;
    }
    
    public async Task<Result<Domain.Aggregate.Species, Error>> GetById(SpeciesId speciesId,
        CancellationToken cancellationToken = default)
    {
        var species = await _dbContext.Species.
            Include(b => b.Breeds).
            FirstOrDefaultAsync(
                s => s.Id == speciesId, cancellationToken);

        if (species == null)
            return Error.NotFound("Species.not.found", "Species not found");
        
        return species;
    }
    
    public void DeleteSpecies(Domain.Aggregate.Species species)
    {
        _dbContext.Species.Remove(species);
    }
}