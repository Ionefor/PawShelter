using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.UseCases.AddPetPhotos;

namespace PawShelter.API.Controllers.Volunteer.Requests;

public record UpdatePetPhotosRequest(IFormFileCollection Files)
{
    public UpdatePetPhotosCommand ToCommand(
        Guid volunteerId, Guid petId, IEnumerable<CreateFileDto> files)
    {
        return new UpdatePetPhotosCommand(volunteerId, petId, files);
    }
}