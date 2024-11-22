using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.SharedKernel.ValueObjects;

public class Name : ComparableValueObject
{
    private Name() {}
    private Name(string value) => Value = value;
    public string Value { get; }
    public static Result<Name, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            value.Length >= Constants.Shared.MaxLowTextLength)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Name)));
        }
        
        return new Name(value);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}