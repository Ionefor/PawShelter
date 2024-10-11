using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.ValueObjects.ForPet
{
    public record Birthday
    {
        private const int MIN_YEAR_BIRTHDAY = 1970;
        private Birthday() { }
        private Birthday(DateOnly birthday)
        {
            Value = birthday;
        }
        public DateOnly Value { get; }
        public static Result<Birthday, Error> Create(DateOnly value)
        {
            if (value.Year < MIN_YEAR_BIRTHDAY ||
                value > DateOnly.FromDateTime(DateTime.Now))
            {
                return Errors.General.ValueIsInvalid("Birthday");
            }               

            return new Birthday(value);
        }
    }
}
