using PawShelter.Application.Species.UseCases.AddBreed;

namespace PawShelter.API.Controllers.Species.Requests;

public record AddBreedRequest(string BreedName)
{
    public AddBreedCommand ToCommand(Guid speciesId)
    {
        return new AddBreedCommand(speciesId, BreedName);
    }
}