using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Species.UseCases.DeleteBreed;

public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;
