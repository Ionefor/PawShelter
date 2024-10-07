using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.VolunteerModel
{
    public record Email
    {
        private const int MAX_LENGTH = 30;
        private Email() { }
        private Email(string email) => Value = email;
        public string Value { get;}
        public Result<Email, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("Email");

            return new Email(value);
        }
    }
}
