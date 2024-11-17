﻿using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;
using PawShelter.SharedKernel;

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
            return Errors.General.NotFound(
                query.PetId).ToErrorList();
        }
        
        return pet;
    }
}