using Microsoft.AspNetCore.Http;
using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePetPhotos;

namespace PawShelter.Volunteers.Presentation.Requests;

public record UpdatePetPhotosRequest(IFormFileCollection Files)
{
    public UpdatePetPhotosCommand ToCommand(
        Guid volunteerId, Guid petId, IEnumerable<CreateFileDto> files)
    {
        return new UpdatePetPhotosCommand(volunteerId, petId, files);
    }
}