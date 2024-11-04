using Microsoft.EntityFrameworkCore;
using PawShelter.Application.Dto;

namespace PawShelter.Application.Database;

public interface IReadDbContext
{
    DbSet<VolunteerDto> Volunteers { get; }
}