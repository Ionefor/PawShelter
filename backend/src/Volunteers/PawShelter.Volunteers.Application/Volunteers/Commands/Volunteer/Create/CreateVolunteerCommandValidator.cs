using FluentValidation;
using PawShelter.Core.Validation;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Domain.ValueObjects;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.Create;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.FullNameDto).MustBeValueObject(f =>
            FullName.Create(
                f.FirstName, f.MiddleName, f.LastName));

        RuleFor(c => c.Description).MustBeValueObject(Description.Create);

        RuleFor(c => c.Email).MustBeValueObject(Email.Create);

       RuleFor(c => c.Experience).MustBeValueObject(Experience.Create);

        RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleForEach(c => c.Requisites).MustBeValueObject(r =>
            Requisite.Create(r.Name, r.Description));

        RuleForEach(c => c.SocialNetworks).MustBeValueObject(s =>
            SocialNetwork.Create(s.Name, s.Link));
    }
}