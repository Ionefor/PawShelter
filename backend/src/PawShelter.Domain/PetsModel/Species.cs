using PawShelter.Domain.PetsModel.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
{
    public class Species : Entity<SpeciesId>
    {
        private readonly List<Breed> _breeds;
        private Species() : base(SpeciesId.NewPetId()) { }
        private Species(SpeciesId id) : base(id) { }
        public string Value { get; private set; } = null!;
        public IReadOnlyList<Breed> Breeds => _breeds;
    }
}
