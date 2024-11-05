using PawShelter.Application.Species.UseCases.AddSpecies;

namespace PawShelter.API.Controllers.Species.Requests;

public record AddSpeciesRequest(string Species)
{
    public AddSpeciesCommand ToCommand()
    {
        return new AddSpeciesCommand(Species);
    }
}