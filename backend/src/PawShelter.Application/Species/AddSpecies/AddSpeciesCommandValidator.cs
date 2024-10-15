using FluentValidation;

namespace PawShelter.Application.Species.AddSpecies;

public class AddSpeciesCommandValidator : AbstractValidator<AddSpeciesCommand>
{
    public AddSpeciesCommandValidator()
    {
        RuleFor(a => a.Species).
            NotNull().
            NotEmpty().
            WithMessage("Species cannot be empty");
    }
}