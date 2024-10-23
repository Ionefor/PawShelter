using PawShelter.Application.Species.AddBreed;

namespace PawShelter.API.Controllers.Species.Requests;

public record AddBreedRequest(string BreedName)
{
    public AddBreedCommand ToCommand(Guid speciesId) =>
        new(speciesId, BreedName);
}