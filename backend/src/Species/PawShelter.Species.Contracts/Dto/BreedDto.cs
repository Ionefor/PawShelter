namespace PawShelter.Species.Contracts.Dto;

public class BreedDto
{
    public Guid BreedId { get; init; }
    
    public Guid SpeciesId { get; init; }
    
    public string Breed { get; init; }
}