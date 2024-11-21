using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Volunteers.Domain.ValueObjects;

public class Color : ComparableValueObject
{
    private Color() {}
    private Color(string color) => Value = color;
    public string Value { get; }
    public static Result<Color, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Color)));
        }

        return new Color(value);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}