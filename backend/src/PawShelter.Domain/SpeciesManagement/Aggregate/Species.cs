using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.Entities;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Domain.SpeciesManagement.Aggregate;

public class Species : Shared.Entity<SpeciesId>
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

    public static Result<Species, Error> Create(SpeciesId id, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Species");

        return new Species(id, value);
    }
}