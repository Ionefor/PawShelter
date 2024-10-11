using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer
{
    public record Email
    {
        private const int MAX_LENGTH = 100;
        private Email() { }
        private Email(string email) => Value = email;
        public string Value { get;}
        public static Result<Email, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("Email");

            return new Email(value);
        }
    }
}
