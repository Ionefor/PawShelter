namespace PawShelter.Application.Species.AddSpecies;

public record AddSpeciesRequest(string Species)
{
    public AddSpeciesCommand ToCommand() =>
        new(Species);
}