namespace PawShelter.Domain.Shared.ValueObjects
{
    public record PhoneNumber
    {
        private PhoneNumber()
        {
            
        }
        public string Value { get; }
        private PhoneNumber(string value) => Value = value;
        public static Result<PhoneNumber> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "Invalid phoneNumber";

            return new PhoneNumber(value);
        }
    }
}
