using PawShelter.Application.Species.UseCases.DeleteBreed;

namespace PawShelter.API.Controllers.Species.Requests;

public record DeleteBreedRequest(Guid SpeciesId, Guid BreedId)
{
   public DeleteBreedCommand ToCommand() =>
        new(SpeciesId, BreedId);
}