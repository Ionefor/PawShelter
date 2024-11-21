using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Dto;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Volunteers.Contracts.Dto.Models;
using PawShelter.Volunteers.Domain.Aggregate;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteerById;

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
        
        if (volunteer is null)
        {
            return Errors.General.
                NotFound(new ErrorParameters.
                    General.NotFound(
                        nameof(Volunteer),
                        nameof(VolunteerId),
                        query.VolunteerId)).ToErrorList();
        }
        
        return volunteer;
    }
}