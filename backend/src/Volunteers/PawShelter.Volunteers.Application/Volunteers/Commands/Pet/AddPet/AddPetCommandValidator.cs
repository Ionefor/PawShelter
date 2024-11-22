using FluentValidation;
using PawShelter.Core.Validation;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Domain.ValueObjects;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.AddPet;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty().WithMessage("Id cannot be empty");

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

        RuleFor(a => a.Status).MustBeEnum<AddPetCommand, string, PetStatus>();
    }
}