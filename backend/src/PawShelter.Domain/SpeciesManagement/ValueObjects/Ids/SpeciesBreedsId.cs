﻿namespace PawShelter.Domain.SpeciesManagement.ValueObjects.Ids
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
