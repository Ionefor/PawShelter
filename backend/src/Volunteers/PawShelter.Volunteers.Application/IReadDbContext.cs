using PawShelter.Core.Dto;
using PawShelter.Volunteers.Contracts.Dto.Models;

namespace PawShelter.Volunteers.Application;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    
    IQueryable<PetDto> Pets { get; }
}