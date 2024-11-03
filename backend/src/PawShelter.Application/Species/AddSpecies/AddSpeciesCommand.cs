using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Species.AddSpecies;

public record AddSpeciesCommand(string Species): ICommand;