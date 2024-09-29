using PawShelter.Domain.PetsModel.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
{
    public class Breed : Entity<BreedId>
    {     
        private Breed() : base(BreedId.NewPetId()) { }
        private Breed(BreedId id) : base(id) { }        
        public string Value { get; private set; } = null!;
    }
}
