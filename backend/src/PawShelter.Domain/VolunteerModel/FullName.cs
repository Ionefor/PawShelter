using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared.ValueObjects;

namespace PawShelter.Domain.VolunteerModel
{
    public record FullName
    {
        private FullName() { }
        private FullName(Name firstName, Name middleName, Name lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
        public Name FirstName { get; } 
        public Name MiddleName { get; }
        public Name LastName { get; }
        public Result<FullName> Create(Name firstName, Name middleName, Name lastName) =>
            new FullName(firstName, middleName, lastName);
    }
}
