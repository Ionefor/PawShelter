using FluentValidation;
using PawShelter.Application.Validation;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;

namespace PawShelter.Application.Volunteers.AddPet;

public class AddPetCommandValidator: AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(a => a.Name).MustBeValueObject(Name.Create);
        
        RuleFor(a => a.Description).MustBeValueObject(Description.Create);

        RuleFor(a => a.Color).MustBeValueObject(Color.Create);
        
        RuleFor(a => a.HealthInfo).MustBeValueObject(HealthInfo.Create);
        
        RuleFor(a => a.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        
        RuleFor(a => a.AddressDto).
            MustBeValueObject(ad => 
                Address.Create(
                    ad.Country, ad.City, ad.Street, ad.HouseNumber));
        
        RuleFor(a => a.PetCharacteristicsDto).
            MustBeValueObject(p => 
                PetCharacteristics.Create(
                    p.Height, p.Width));
        
        RuleFor(a => a.Birthday).MustBeValueObject(Birthday.Create);
        
        RuleFor(a => a.Species).
            NotEmpty().
            NotNull().
            WithMessage("Species cannot be empty");
        
        RuleFor(a => a.Breed).
            NotEmpty().
            NotNull().
            WithMessage("Breed cannot be empty");
        
        RuleForEach(a => a.RequisitesDto.Requisites).
            MustBeValueObject(r => 
                Requisite.Create(r.Name, r.Description));
    }
}