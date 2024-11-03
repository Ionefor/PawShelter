using FluentValidation;
using PawShelter.Application.Validation;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;

namespace PawShelter.Application.Volunteers.UseCases.UpdateRequisites;

public class UpdateRequisitesCommandValidator : AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisitesCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty();

        RuleForEach(u => u.RequisitesDto.Requisites).MustBeValueObject(r =>
            Requisite.Create(r.Name, r.Description));
    }
}