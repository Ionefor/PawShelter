using PawShelter.Application.Dto;

namespace PawShelter.Application.Volunteers.AddPetPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId, Guid PetId, IEnumerable<CreateFileDto> Files);