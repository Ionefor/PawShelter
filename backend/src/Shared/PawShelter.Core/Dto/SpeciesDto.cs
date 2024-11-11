namespace PawShelter.Core.Dto;

public class SpeciesDto
{
    public Guid SpeciesId { get; init; }
    
    public string Species { get; init; }
    
    public IReadOnlyList<BreedDto> Breeds { get; init; }
}