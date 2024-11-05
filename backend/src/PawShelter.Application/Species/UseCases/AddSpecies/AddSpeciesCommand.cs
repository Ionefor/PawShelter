using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Species.UseCases.AddSpecies;

public record AddSpeciesCommand(string Species): ICommand;