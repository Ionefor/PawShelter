using PawShelter.Core.Abstractions;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.HardDeletePet;

public record HardDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;