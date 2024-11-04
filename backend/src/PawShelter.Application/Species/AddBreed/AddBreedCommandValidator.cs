using FluentValidation;

namespace PawShelter.Application.Species.AddBreed;

public class AddBreedCommandValidator : AbstractValidator<AddBreedCommand>
{
    public AddBreedCommandValidator()
    {
        RuleFor(a => a.SpeciesId).NotEmpty().WithMessage("SpeciesId cannot be empty");

        RuleFor(a => a.BreedName).NotEmpty().NotNull().WithMessage("BreedName cannot be empty");
    }
}