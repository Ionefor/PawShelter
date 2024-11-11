using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;

public record FullName
{
    private FullName()
    {
    }

    private FullName(string firstName, string middleName, string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string MiddleName { get; }
    public string LastName { get; }

    public static Result<FullName, Error> Create(string firstName, string middleName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(middleName) ||
            string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsInvalid("Fullname");

        return new FullName(firstName, middleName, lastName);
    }
}