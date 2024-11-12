using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePetPhotos;

public record UpdatePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<CreateFileDto> Files) : ICommand;