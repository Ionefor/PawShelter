using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Volunteers.Domain.ValueObjects;

public class Birthday : ComparableValueObject
{
    private Birthday() {}
    private Birthday(DateOnly birthday) =>  Value = birthday;
    public DateOnly Value { get; }

    public static Result<Birthday, Error> Create(DateOnly value)
    {
        if (value.Year < Constants.Volunteers.MinYearBirthday ||
            value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Birthday)));
        }
        
        return new Birthday(value);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}