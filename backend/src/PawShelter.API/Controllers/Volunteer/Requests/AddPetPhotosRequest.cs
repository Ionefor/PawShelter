using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.AddPetPhotos;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record AddPetPhotosRequest(IFormFileCollection Files)
{
    public AddPetPhotosCommand ToCommand(
        Guid volunteerId, Guid petId, IEnumerable<CreateFileDto> files) =>
            new(volunteerId, petId, files);
}