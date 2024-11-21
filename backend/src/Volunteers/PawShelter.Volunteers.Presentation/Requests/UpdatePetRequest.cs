using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePet;
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Volunteers.Presentation.Requests;

public record UpdatePetRequest(
    string Name,
    string Description,
    string Color,
    string HealthInfo,
    string PhoneNumber,
    AddressDto AddressDto,
    PetCharacteristicsDto PetCharacteristicsDto,
    bool IsCastrated,
    bool IsVaccinated,
    DateOnly Birthday,
    DateTime PublicationDate,
    string Species,
    string Breed,
    RequisitesDto RequisitesDto)
{
    public UpdatePetCommand ToCommand(Guid volunteerId, Guid petId)
    {
        return new UpdatePetCommand(volunteerId, petId, Name, Description, Color,
            HealthInfo, PhoneNumber, AddressDto,
            PetCharacteristicsDto, IsCastrated,
            IsVaccinated, Birthday, PublicationDate,
            Species, Breed, RequisitesDto);
    }
}