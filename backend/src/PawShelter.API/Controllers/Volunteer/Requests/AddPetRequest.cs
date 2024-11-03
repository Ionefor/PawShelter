using PawShelter.Application.Dto;
using PawShelter.Application.Volunteers.UseCases.AddPet;
using PawShelter.Domain.PetsManagement;

namespace PawShelter.API.Controllers.Volunteer.Requests;

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
    PetStatus Status,
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