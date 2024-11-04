using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Dto;
using PawShelter.Application.Extensions;
using PawShelter.Application.Models;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.Queries.GetVolunteerById;

public class GetVolunteerByIdHandler : 
    IQueryHandler<Result<VolunteerDto, ErrorList>, GetVolunteerByIdQuery>
{
    private readonly IReadDbContext _context;
    private readonly IValidator<GetVolunteerByIdQuery> _validator;

    public GetVolunteerByIdHandler(
        IReadDbContext context,
        IValidator<GetVolunteerByIdQuery> queryValidator)
    {
        _context = context;
        _validator = queryValidator;
    }
    
    public async Task<Result<VolunteerDto, ErrorList>> Handle(
        GetVolunteerByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var validatorResult = await _validator.ValidateAsync(query, cancellationToken);
        
        if (!validatorResult.IsValid)
            return validatorResult.ToErrorList();
        
        var volunteer = await _context.Volunteers.
            FirstOrDefaultAsync(
                v => v.Id == query.VolunteerId, cancellationToken);
        
        if(volunteer is null)
            return Errors.General.NotFound(query.VolunteerId).ToErrorList();
        
        return volunteer;
    }
}