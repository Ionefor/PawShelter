using FluentValidation;
using PawShelter.Core.Validation;
using PawShelter.SharedKernel;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteerById;

public class GetVolunteerByIdQueryValidator : AbstractValidator<GetVolunteerByIdQuery>
{
    public GetVolunteerByIdQueryValidator()
    {
        RuleFor(g => g.VolunteerId).
            NotEmpty().
            WithError(Errors.General.ValueIsRequired("Volunteer id"));
    }
}