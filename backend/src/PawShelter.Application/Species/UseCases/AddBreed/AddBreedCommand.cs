using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Species.UseCases.AddBreed;

public record AddBreedCommand(Guid SpeciesId, string BreedName) : ICommand;