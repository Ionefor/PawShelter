namespace PawShelter.Application.Species.AddBreed;

public record AddBreedRequest(string BreedName)
{
    public AddBreedCommand ToCommand(Guid speciesId) =>
        new(speciesId, BreedName);
}