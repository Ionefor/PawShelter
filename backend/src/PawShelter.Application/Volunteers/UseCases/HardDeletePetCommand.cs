using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Volunteers.UseCases;

public record HardDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;