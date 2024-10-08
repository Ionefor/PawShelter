using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;
using PawShelter.Domain.VolunteerModel;


namespace PawShelter.Application.Volunteers;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId);
}
