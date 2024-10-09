using FluentValidation;
using PawShelter.Application.Dto;
using PawShelter.Application.Validation;
using PawShelter.Domain.Shared.ValueObjects;
using PawShelter.Domain.VolunteerModel;

namespace PawShelter.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(c => c.FullNameDto).
            MustBeValueObject(f => 
                FullName.Create(f.firstName, f.middleName, f.lastName));
        
        RuleFor(c => c.Description).
            MustBeValueObject(Description.Create);
        
        RuleFor(c => c.Email).
            MustBeValueObject(Email.Create);
        
        RuleFor(c => c.Experience).
            MustBeValueObject(Experience.Create);
        
        RuleFor(c => c.PhoneNumber).
            MustBeValueObject(PhoneNumber.Create);

        RuleForEach(c => c.Requisites).
            MustBeValueObject(r => 
                Requisite.Create(r.name, r.description));

        RuleForEach(c => c.SocialNetworks).
            MustBeValueObject(s => 
                SocialNetwork.Create(s.name, s.link));
    }
}