using FluentValidation;
using PawShelter.Application.Validation;
using PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;

namespace PawShelter.Application.Volunteers.UseCases.UpdateMainInfo;

public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoCommandValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty();

        RuleFor(u => u.MainInfoDto.FullName).MustBeValueObject(f =>
            FullName.Create(f.FirstName, f.MiddleName, f.LastName));

        RuleFor(u => u.MainInfoDto.Description).MustBeValueObject(Description.Create);

        RuleFor(u => u.MainInfoDto.Email).MustBeValueObject(Email.Create);

        RuleFor(u => u.MainInfoDto.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleFor(u => u.MainInfoDto.Experience).MustBeValueObject(Experience.Create);
    }
}