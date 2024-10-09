using CSharpFunctionalExtensions;
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
            MustBeValueObject(f => Name.Create(f.firstName)).
            MustBeValueObject(m => Name.Create(m.middleName)).
            MustBeValueObject(l => Name.Create(l.lastName));
        
        RuleFor(c => c.Description).
            MustBeValueObject(Description.Create);
        
        RuleFor(c => c.Email).
            MustBeValueObject(Email.Create);
        
        RuleFor(c => c.Experience).
            MustBeValueObject(Experience.Create);
        
        RuleFor(c => c.PhoneNumber).
            MustBeValueObject(PhoneNumber.Create);

        RuleForEach(c => c.Requisites).
            MustBeValueObject(r => Name.Create(r.name)).
            MustBeValueObject(r => Description.Create(r.description));
    }
}