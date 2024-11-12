using ICommand = PawShelter.Core.Abstractions.ICommand;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.SetMainPetPhoto;

public record SetMainPetPhotoCommand(
    Guid VolunteerId, Guid PetId, string FilePath) : ICommand;