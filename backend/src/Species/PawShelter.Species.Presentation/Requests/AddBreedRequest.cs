using PawShelter.Species.Application.Species.Commands.AddBreed;

namespace PawShelter.Species.Presentation.Requests;

public record AddBreedRequest(string BreedName)
{
    public AddBreedCommand ToCommand(Guid speciesId)
    {
        return new AddBreedCommand(speciesId, BreedName);
    }
}