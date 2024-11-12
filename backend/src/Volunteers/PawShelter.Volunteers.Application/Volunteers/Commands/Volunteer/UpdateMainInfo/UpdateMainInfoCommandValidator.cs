using FluentValidation;
using PawShelter.Core.Validation;
using PawShelter.Volunteers.Domain.ValueObjects.ForVolunteer;
using PawShelter.Volunteers.Domain.ValueObjects.Shared;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateMainInfo;

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