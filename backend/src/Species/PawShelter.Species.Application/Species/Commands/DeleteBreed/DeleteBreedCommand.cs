using PawShelter.Core.Abstractions;

namespace PawShelter.Species.Application.Species.Commands.DeleteBreed;

public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;
