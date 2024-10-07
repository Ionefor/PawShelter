using CSharpFunctionalExtensions;

namespace PawShelter.Domain.Shared.ValueObjects
{
    public record Name
    {
        private Name() { }
        public string Value { get; }
        private Name(string value) => Value = value;
        public static Result<Name, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length >= Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsInvalid("Name");

            return new Name(value);
        }
    }
}
