using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Volunteers.Domain.ValueObjects;

public class Email : ComparableValueObject
{
    private Email() {}
    private Email(string email) => Value = email;
    public string Value { get; }
    public static Result<Email, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            value.Length > Constants.Shared.MaxMediumTextLength)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Email)));
        }
        
        return new Email(value);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}