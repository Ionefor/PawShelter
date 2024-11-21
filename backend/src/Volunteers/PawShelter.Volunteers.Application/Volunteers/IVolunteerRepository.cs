using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Volunteers.Domain.Aggregate;
using PawShelter.Volunteers.Domain.Entities;

namespace PawShelter.Volunteers.Application.Volunteers;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    
    Guid Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
    
    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);

    Task<Result<Pet, Error>> GetPetById(PetId petId, CancellationToken cancellationToken = default);
}