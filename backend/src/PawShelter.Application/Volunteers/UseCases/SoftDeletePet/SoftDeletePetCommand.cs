using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Volunteers.UseCases.SoftDeletePet;

public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;