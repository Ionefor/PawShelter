using CSharpFunctionalExtensions;

namespace PawShelter.Domain.Shared.ValueObjects
{
    public record Requisite
    {
        private Requisite() { }
        public Requisite(Name name, Description description)
        {
            Name = name;
            Description = description;
        }
        public Name Name { get; }
        public Description Description { get; }
    }
}
