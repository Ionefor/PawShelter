using PawShelter.Core.Abstractions;

namespace PawShelter.Species.Application.Species.Commands.AddBreed;

public record AddBreedCommand(Guid SpeciesId, string BreedName) : ICommand;