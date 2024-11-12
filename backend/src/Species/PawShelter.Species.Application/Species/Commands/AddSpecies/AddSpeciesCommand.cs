using PawShelter.Core.Abstractions;

namespace PawShelter.Species.Application.Species.Commands.AddSpecies;

public record AddSpeciesCommand(string Species): ICommand;