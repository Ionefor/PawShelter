using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
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
        public Result<Birthday, Error> Create(DateOnly birthday)
        {
            if (birthday.Year < MIN_YEAR_BIRTHDAY || 
                birthday > DateOnly.FromDateTime(DateTime.Now))
            {
                return Errors.General.ValueIsInvalid("Birthday");
            }               

            return new Birthday(Value);
        }
    }
}
