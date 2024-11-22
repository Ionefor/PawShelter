using PawShelter.Core.Dto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.AddPet;
using PawShelter.Volunteers.Contracts.Dto.Command;

namespace PawShelter.Volunteers.Presentation.Requests;

public record AddPetRequest(
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
    string Status,
    string Species,
    string Breed,
    RequisitesDto RequisitesDto)
{
    public AddPetCommand ToCommand(Guid volunteerId)
    {
        return new AddPetCommand(volunteerId, Name, Description, Color,
            HealthInfo, PhoneNumber, AddressDto,
            PetCharacteristicsDto, IsCastrated,
            IsVaccinated, Birthday, PublicationDate,
            Status, Species, Breed, RequisitesDto);
    }
}
