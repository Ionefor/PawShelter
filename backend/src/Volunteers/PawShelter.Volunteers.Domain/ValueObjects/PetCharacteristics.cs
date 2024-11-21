using CSharpFunctionalExtensions;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;

namespace PawShelter.Volunteers.Domain.ValueObjects;

public class PetCharacteristics : ComparableValueObject
{
    private PetCharacteristics() {}
    private PetCharacteristics(double height, double weight)
    {
        Height = height;
        Weight = weight;
    }
    public double Height { get; }
    public double Weight { get; }
    public static Result<PetCharacteristics, Error> Create(double height, double weight)
    {
        if (height <= Constants.Volunteers.MinPhysicalValue || height > Constants.Volunteers.MaxPhysicalValue ||
            weight <= Constants.Volunteers.MinPhysicalValue || weight > Constants.Volunteers.MaxPhysicalValue)
        {
            return Errors.General.
                ValueIsInvalid(new ErrorParameters.General.ValueIsInvalid(nameof(PetCharacteristics)));
        }
        
        return new PetCharacteristics(height, weight);
    }
    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Height;
        yield return Weight;
    }
}