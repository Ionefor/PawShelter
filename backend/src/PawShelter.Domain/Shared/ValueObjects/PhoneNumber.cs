using CSharpFunctionalExtensions;

namespace PawShelter.Domain.Shared.ValueObjects
{
    public record PhoneNumber
    {
        private PhoneNumber() { }
        public string Value { get; }
        private PhoneNumber(string value) => Value = value;
        public static Result<PhoneNumber, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Errors.General.ValueIsInvalid("PhoneNumber");

            return new PhoneNumber(value);
        }
    }
}
