using PawShelter.Core.Abstractions;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.DeletePetPhoto;

public record DeletePetPhotoCommand(
    Guid VolunteerId, Guid PetId, string FilePath) : ICommand;