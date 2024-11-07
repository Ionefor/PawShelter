using PawShelter.Application.Abstractions;

namespace PawShelter.Application.Volunteers.UseCases.DeletePetPhoto;

public record DeletePetPhotoCommand(
    Guid VolunteerId, Guid PetId, string FilePath) : ICommand;