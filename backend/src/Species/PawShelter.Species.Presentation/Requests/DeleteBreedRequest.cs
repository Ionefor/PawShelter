using PawShelter.Species.Application.Species.Commands.DeleteBreed;

namespace PawShelter.Species.Presentation.Requests;

public record DeleteBreedRequest(Guid SpeciesId, Guid BreedId)
{
   public DeleteBreedCommand ToCommand() =>
        new(SpeciesId, BreedId);
}