using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;

namespace PawShelter.Volunteers.Domain.ValueObjects.ForPet;

public record PetCharacteristics
{
    private const int MIN_VALUE = 0;
    private const int MAX_VALUE = 400;

    private PetCharacteristics()
    {
    }

    private PetCharacteristics(double height, double weight)
    {
        Height = height;
        Weight = weight;
    }

    public double Height { get; }
    public double Weight { get; }

    public static Result<PetCharacteristics, Error> Create(double height, double weight)
    {
        if (height <= MIN_VALUE || height > MAX_VALUE ||
            weight <= MIN_VALUE || weight > MAX_VALUE)
            return Errors.General.ValueIsInvalid("PetCharacteristics");

        return new PetCharacteristics(height, weight);
    }
}