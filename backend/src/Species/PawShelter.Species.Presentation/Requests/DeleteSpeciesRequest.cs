using PawShelter.Species.Application.Species.Commands.DeleteSpecies;

namespace PawShelter.Species.Presentation.Requests;

public record DeleteSpeciesRequest(Guid SpeciesId)
{
   public DeleteSpeciesCommand ToCommand() =>
        new DeleteSpeciesCommand(SpeciesId);
}