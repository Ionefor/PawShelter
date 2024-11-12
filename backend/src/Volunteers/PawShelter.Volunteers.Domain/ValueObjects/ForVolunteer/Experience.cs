using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;

namespace PawShelter.Volunteers.Domain.ValueObjects.ForVolunteer;

public record Experience
{
    private const int MAX_YEAR_EXPERIENCE = 100;
    private const int MIN_YEAR_EXPERIENCE = 0;

    private Experience()
    {
    }

    private Experience(int experience)
    {
        Value = experience;
    }

    public int Value { get; }

    public static Result<Experience, Error> Create(int value)
    {
        if (value < MIN_YEAR_EXPERIENCE || value > MAX_YEAR_EXPERIENCE)
            return Errors.General.ValueIsInvalid("Experience");

        return new Experience(value);
    }
}