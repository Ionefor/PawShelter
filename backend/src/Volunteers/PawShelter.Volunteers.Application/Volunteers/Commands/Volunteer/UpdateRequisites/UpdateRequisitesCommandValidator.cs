using FluentValidation;
using PawShelter.Core.Validation;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Domain.ValueObjects.Shared;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateRequisites;

public class UpdateRequisitesCommandValidator : AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisitesCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty();

        RuleForEach(u => u.RequisitesDto.Requisites).MustBeValueObject(r =>
            Requisite.Create(r.Name, r.Description));
    }
}