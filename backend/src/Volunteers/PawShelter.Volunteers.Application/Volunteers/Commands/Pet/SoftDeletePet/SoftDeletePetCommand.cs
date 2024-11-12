using PawShelter.Core.Abstractions;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.SoftDeletePet;

public record SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;