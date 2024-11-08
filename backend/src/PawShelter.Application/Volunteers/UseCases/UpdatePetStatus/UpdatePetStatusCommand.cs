using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Volunteers.UseCases.UpdatePetStatus;

public record UpdatePetStatusCommand(Guid VolunteerId, Guid PetId, string Status) : ICommand;