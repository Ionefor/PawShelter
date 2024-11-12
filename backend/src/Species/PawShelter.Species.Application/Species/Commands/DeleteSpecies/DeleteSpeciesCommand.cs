using PawShelter.Core.Abstractions;

namespace PawShelter.Species.Application.Species.Commands.DeleteSpecies;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;