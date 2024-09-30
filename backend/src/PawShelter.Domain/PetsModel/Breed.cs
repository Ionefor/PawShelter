using PawShelter.Domain.PetsModel.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
{
    public class Breed : Entity<BreedId>
    {     
        private Breed(BreedId id, string value) : base(id)
        {
            Value = value;
        }        
        public string Value { get; private set; } = null!;

        public static Result<Breed> Create(BreedId id, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "Invalid breed name";

            return new Breed(id, value);
        }
    }
}
