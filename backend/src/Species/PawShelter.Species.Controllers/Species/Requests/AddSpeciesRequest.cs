using PawShelter.Application.Species.UseCases.AddSpecies;

namespace PawShelter.Web.Controllers.Species.Requests;

public record AddSpeciesRequest(string Species)
{
    public AddSpeciesCommand ToCommand()
    {
        return new AddSpeciesCommand(Species);
    }
}