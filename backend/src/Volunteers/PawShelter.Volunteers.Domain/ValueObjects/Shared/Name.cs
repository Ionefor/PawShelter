using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;

namespace PawShelter.Volunteers.Domain.ValueObjects.Shared;

public record Name
{
    private Name()
    {
    }

    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Name, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length >= Constants.MAX_LOW_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        return new Name(value);
    }
}