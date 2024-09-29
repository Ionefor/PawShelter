using PawShelter.Domain.Shared;

namespace PawShelter.Domain.VolunteerModel
{
    public record Email
    {
        private Email() { }
        private Email(string email) =>
            Value = email;
        public string Value { get;}
        public Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "Invalid email";

            return new Email(email);
        }
    }
}
