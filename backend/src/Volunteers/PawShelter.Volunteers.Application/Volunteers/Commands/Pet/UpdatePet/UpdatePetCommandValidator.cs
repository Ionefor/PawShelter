using FluentValidation;
using PawShelter.Core.Validation;
using PawShelter.Volunteers.Domain.ValueObjects.ForPet;
using PawShelter.Volunteers.Domain.ValueObjects.Shared;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePet;

public class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithMessage("VolunteerId cannot be empty");

        RuleFor(a => a.PetId).NotEmpty().WithMessage("PetId cannot be empty");
        
        RuleFor(a => a.Name).MustBeValueObject(Name.Create);

        RuleFor(a => a.Description).MustBeValueObject(Description.Create);

        RuleFor(a => a.Color).MustBeValueObject(Color.Create);

        RuleFor(a => a.HealthInfo).MustBeValueObject(HealthInfo.Create);

        RuleFor(a => a.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

        RuleFor(a => a.AddressDto).MustBeValueObject(ad =>
            Address.Create(
                ad.Country, ad.City, ad.Street, ad.HouseNumber));

        RuleFor(a => a.PetCharacteristicsDto).MustBeValueObject(p =>
            PetCharacteristics.Create(
                p.Height, p.Weight));

        RuleFor(a => a.Birthday).MustBeValueObject(Birthday.Create);

        RuleFor(a => a.Species).NotEmpty().NotNull().WithMessage("Species cannot be empty");

        RuleFor(a => a.Breed).NotEmpty().NotNull().WithMessage("Breed cannot be empty");

        RuleForEach(a => a.RequisitesDto.Requisites).MustBeValueObject(r =>
            Requisite.Create(r.Name, r.Description));
    }
}