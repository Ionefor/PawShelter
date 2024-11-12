using PawShelter.Core.Abstractions;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string Status) : ICommand;