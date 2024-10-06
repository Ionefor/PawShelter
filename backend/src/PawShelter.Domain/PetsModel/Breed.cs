using CSharpFunctionalExtensions;
using PawShelter.Domain.PetsModel.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
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
