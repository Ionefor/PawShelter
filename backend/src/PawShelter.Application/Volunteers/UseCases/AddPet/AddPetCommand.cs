using PawShelter.Application.Abstractions;
using PawShelter.Application.Dto;
using PawShelter.Domain.PetsManagement;

namespace PawShelter.Application.Volunteers.UseCases.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
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
    RequisitesDto RequisitesDto) : ICommand;