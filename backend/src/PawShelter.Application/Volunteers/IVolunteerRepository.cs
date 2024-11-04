using CSharpFunctionalExtensions;
using PawShelter.Domain.PetsManagement.Aggregate;
using PawShelter.Domain.PetsManagement.Entities;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Guid Save(Volunteer volunteer, CancellationToken cancellationToken = default);

    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);

    Task<Result<Pet, Error>> GetPetById(PetId petId, CancellationToken cancellationToken = default);
}