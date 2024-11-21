using FluentValidation;
using PawShelter.Core.Validation;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;

namespace PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteerById;

public class GetVolunteerByIdQueryValidator : AbstractValidator<GetVolunteerByIdQuery>
{
    public GetVolunteerByIdQueryValidator()
    {
        RuleFor(g => g.VolunteerId).
            NotEmpty().
            WithError(Errors.General.
                ValueIsRequired(new ErrorParameters.General.ValueIsRequired(nameof(VolunteerId))));
    }
}