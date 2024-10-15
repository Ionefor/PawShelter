namespace PawShelter.Application.Species.AddBreed;

public record AddBreedCommand(Guid SpeciesId, string BreedName);