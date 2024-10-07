using CSharpFunctionalExtensions;

namespace PawShelter.Domain.Shared.ValueObjects
{
    public record Description
    {
        private Description() { }
        public string Value { get; }
        private Description(string value) => Value = value;
        public static Result<Description, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length >= Constants.MAX_HIGH_TEXT_LENGTH)
                return Errors.General.ValueIsInvalid("Description");

            return new Description(value);
        }
    }
}
