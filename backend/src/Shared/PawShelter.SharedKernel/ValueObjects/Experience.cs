using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.SharedKernel.ValueObjects;

public class Experience : ComparableValueObject
{
    private Experience() {}
    private Experience(int experience) => Value = experience;
    public int Value { get; }
    public static Result<Experience, Error> Create(int value)
    {
        if (value < Constants.Volunteers.MinYearExperience ||
            value > Constants.Volunteers.MaxYearExperience)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(Experience)));
        }

        return new Experience(value);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        throw new NotImplementedException();
    }
}