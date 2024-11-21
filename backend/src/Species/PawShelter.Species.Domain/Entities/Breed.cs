using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;

namespace PawShelter.Species.Domain.Entities;

public class Breed : SharedKernel.Models.Abstractions.Entity<BreedId>
{
    private Breed(BreedId id) : base(id) {}
    private Breed(BreedId id, string value) : base(id) => Value = value;
    public string Value { get; private set; }
    public static Result<Breed, Error> Create(BreedId id, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Breed)));
        }

        return new Breed(id, value);
    }
}