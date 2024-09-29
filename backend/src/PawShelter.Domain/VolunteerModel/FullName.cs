using PawShelter.Domain.Shared;
using PawShelter.Domain.Shared.ValueObjects;

namespace PawShelter.Domain.VolunteerModel
{
    public record FullName
    {
        private FullName()
        {
            
        }
        private FullName(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
        public string FirstName { get; } 
        public string MiddleName { get; }
        public string LastName { get; }
        public Result<FullName> Create(string firstName, string middleName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(middleName)
                || string.IsNullOrWhiteSpace(lastName))
                return "Invalid FullName";

            return new FullName(firstName, middleName, lastName);
        }
            
    }
}
