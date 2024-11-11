using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Dto;
using PawShelter.Domain.PetsManagement.Entities;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.Queries.GetPetById;

public class GetPetByIdHandler : 
    IQueryHandler<Result<PetDto, ErrorList>, GetPetByIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetPetByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<Result<PetDto, ErrorList>> Handle(
        GetPetByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var pet = await _readDbContext.Pets.
            FirstOrDefaultAsync(
                p => p.Id == query.PetId,
                cancellationToken);

        if (pet is null)
        {
            return Errors.General.NotFound(
                query.PetId).ToErrorList();
        }
        
        return pet;
    }
}
