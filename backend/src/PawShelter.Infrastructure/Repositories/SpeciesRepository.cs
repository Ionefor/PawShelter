using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.Application.Species;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.Aggregate;
using PawShelter.Domain.SpeciesManagement.Entities;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SpeciesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<bool, Error>> ExistSpecies(
        string speciesName,
        CancellationToken cancellationToken = default)
    {
        var species = await _dbContext.Species.
            Include(b => b.Breeds).
            FirstOrDefaultAsync(
                s => s.Value == speciesName, cancellationToken);

        if (species == null)
            return false;
        
        return true;
    }
    
    public async Task<Result<bool, Error>> ExistBreed(
        string breedName,
        CancellationToken cancellationToken = default)
    {
        var breedExist =  await _dbContext.Species.
            SelectMany(s => s.Breeds).
            FirstOrDefaultAsync(b => b.Value == breedName, cancellationToken);

        if (breedExist == null)
            return false;

        return true;
    }
    
    
    public async Task<Guid> Add(Species species, CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return species.Id.Value;
    }
    
    public async Task<Guid> Save(Species species, CancellationToken cancellationToken = default)
    {
        _dbContext.Species.Attach(species);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return species.Id.Value;
    }
    
    public async Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken cancellationToken = default)
    {
        var species = await _dbContext.Species.
            Include(b => b.Breeds).
            FirstOrDefaultAsync(
                s => s.Id == speciesId, cancellationToken);
        
        if(species == null)
            return Error.NotFound("Species.not.found", "Species not found");
        
        return species;
    }
}