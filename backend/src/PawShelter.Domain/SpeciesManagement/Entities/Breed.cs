using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Domain.SpeciesManagement.Entities
{
    public class Breed : Shared.Entity<BreedId>
    {     
        private Breed(BreedId id, string value) : base(id)
        {
            Value = value;
        }        
        public string Value { get; private set; } = null!;
        public static Result<Breed, Error> Create(BreedId id, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Errors.General.ValueIsInvalid("Breed");

            return new Breed(id, value);
        }
    }
}
