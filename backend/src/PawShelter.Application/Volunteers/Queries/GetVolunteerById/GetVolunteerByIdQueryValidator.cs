using FluentValidation;
using PawShelter.Application.Validation;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.Queries.GetVolunteerById;

public class GetVolunteerByIdQueryValidator : AbstractValidator<GetVolunteerByIdQuery>
{
    public GetVolunteerByIdQueryValidator()
    {
        RuleFor(g => g.VolunteerId).
            NotEmpty().
            WithError(Errors.General.ValueIsRequired("Volunteer id"));
    }
}