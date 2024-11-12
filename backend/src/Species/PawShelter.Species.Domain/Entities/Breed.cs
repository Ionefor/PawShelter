using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Species.Domain.Entities;

public class Breed : SharedKernel.Entity<BreedId>
{
    private Breed(BreedId id, string value) : base(id)
    {
        Value = value;
    }

    public string Value { get; private set; } = null!;

    public static Result<Breed, Error> Create(BreedId id, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Errors.General.ValueIsInvalid("Breed");

        return new Breed(id, value);
    }
}