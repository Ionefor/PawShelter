using PawShelter.Species.Application.Species.Commands.AddSpecies;

namespace PawShelter.Species.Presentation.Requests;

public record AddSpeciesRequest(string Species)
{
    public AddSpeciesCommand ToCommand()
    {
        return new AddSpeciesCommand(Species);
    }
}