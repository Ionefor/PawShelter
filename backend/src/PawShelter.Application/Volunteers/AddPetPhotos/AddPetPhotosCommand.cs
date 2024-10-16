using PawShelter.Application.Dto;
using PawShelter.Application.FileProvider;

namespace PawShelter.Application.Volunteers.AddPetPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId, Guid PetId,IEnumerable<CreateFileDto> Files);