using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
{
    public record Birthday
    {
        private Birthday()
        {
            
        }
        private Birthday(DateOnly birthday)
        {
            Value = birthday;
        }
        public DateOnly Value { get; }
        public Result<Birthday> Create(DateOnly birthday)
        {
            if (birthday.Year < 1970 || birthday > DateOnly.FromDateTime(DateTime.Now))
                return "Invalid birthday";

            return new Birthday(Value);
        }
    }
}
