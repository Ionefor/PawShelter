using PawShelter.Domain.Shared.ValueObjects;

namespace PawShelter.Domain.VolunteerModel
{
    public record FullName
    {
        private FullName() { }
        public FullName(Name firstName, Name middleName, Name lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
        public Name FirstName { get; } 
        public Name MiddleName { get; }
        public Name LastName { get; }
    }
}
