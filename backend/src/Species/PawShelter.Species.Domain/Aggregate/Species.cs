using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Species.Domain.Entities;

namespace PawShelter.Species.Domain.Aggregate;

public class Species : SharedKernel.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds;

    private Species(SpeciesId id, string value) : base(id)
    {
        Value = value;
    }

    public string Value { get; private set; } = null!;
    public IReadOnlyList<Breed> Breeds => _breeds;

    public void AddBreed(Breed breed)
    {
        _breeds.Add(breed);
    }

    public void DeleteBreed(Breed breed)
    {
        _breeds.Remove(breed);
    }
    public static Result<Species, Error> Create(SpeciesId id, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Species");

        return new Species(id, value);
    }
}