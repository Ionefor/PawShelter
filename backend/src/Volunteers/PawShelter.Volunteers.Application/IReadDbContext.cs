using PawShelter.Core.Dto;

namespace PawShelter.Volunteers.Application;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    
    IQueryable<PetDto> Pets { get; }
}