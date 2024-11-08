using PawShelter.Application.Abstractions;
using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.UseCases.AddPetPhotos;

public record UpdatePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<CreateFileDto> Files) : ICommand;