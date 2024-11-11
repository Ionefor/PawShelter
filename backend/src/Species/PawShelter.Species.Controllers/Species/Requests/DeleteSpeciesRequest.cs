using PawShelter.Application.Species.UseCases.DeleteSpecies;

namespace PawShelter.Web.Controllers.Species.Requests;

public record DeleteSpeciesRequest(Guid SpeciesId)
{
   public DeleteSpeciesCommand ToCommand() =>
        new DeleteSpeciesCommand(SpeciesId);
}