using PawShelter.Application.Species.AddSpecies;

namespace PawShelter.API.Controllers.Species.Requests;

public record AddSpeciesRequest(string Species)
{
    public AddSpeciesCommand ToCommand() =>
        new(Species);
}