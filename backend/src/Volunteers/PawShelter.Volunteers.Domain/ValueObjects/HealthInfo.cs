using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Volunteers.Domain.ValueObjects;

public class HealthInfo : ComparableValueObject
{
    private HealthInfo() {}
    private HealthInfo(string healthInfo) => Value = healthInfo;
    public string Value { get; }
    public static Result<HealthInfo, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            value.Length > Constants.Shared.MaxLargeTextLength)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(HealthInfo)));
        }

        return new HealthInfo(value);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}