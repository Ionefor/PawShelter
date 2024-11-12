using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;

namespace PawShelter.Volunteers.Domain.ValueObjects.Shared;

public record Requisite
{
    private Requisite()
    {
    }

    private Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }

    public static Result<Requisite, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(description))
            return Errors.General.ValueIsInvalid("Requisite");
        return new Requisite(name, description);
    }
}