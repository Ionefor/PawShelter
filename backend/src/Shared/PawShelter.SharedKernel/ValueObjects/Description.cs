using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.SharedKernel.ValueObjects;

public class Description : ComparableValueObject
{
    private Description() {}
    private Description(string value) =>  Value = value;
    public string Value { get; }
    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            value.Length >= Constants.Shared.MaxLargeTextLength)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Description)));
        }
        
        return new Description(value);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}