using PawShelter.Domain.PetsModel.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsModel
{
    public record SpeciesBreedsId
    {
        private SpeciesBreedsId() { }
        private SpeciesBreedsId(SpeciesId speciesId, Guid breedId) 
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }
        public SpeciesId SpeciesId { get; }
        public Guid BreedId { get; }
        public static Result<SpeciesBreedsId> Create(SpeciesId speciesId, Guid breedId) =>
            new SpeciesBreedsId(speciesId, breedId);
    }
}
