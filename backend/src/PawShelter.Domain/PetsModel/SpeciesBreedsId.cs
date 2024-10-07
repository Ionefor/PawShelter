using PawShelter.Domain.PetsModel.Ids;

namespace PawShelter.Domain.PetsModel
{
    public record SpeciesBreedsId
    {
        private SpeciesBreedsId() { }
        public SpeciesBreedsId(SpeciesId speciesId, Guid breedId) 
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }
        public SpeciesId SpeciesId { get; }
        public Guid BreedId { get; }
    }
}
