using FluentValidation;
using PawShelter.Application.Validation;
using PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;

namespace PawShelter.Application.Volunteers.UseCases.Create;

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