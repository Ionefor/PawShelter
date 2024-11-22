using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Volunteers.Contracts.Dto.Models;
using PawShelter.Volunteers.Domain.Entities;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetPetById;

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
            return Errors.General.
                NotFound(new ErrorParameters.
                    General.NotFound(nameof(Pet),
                        nameof(PetId), query.PetId)).ToErrorList();
        }
        
        return pet;
    }
}
