using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Species.UseCases.DeleteSpecies;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;