using FluentValidation;

namespace PawShelter.Application.Volunteers.UseCases.SetMainPetPhoto;

public class SetMainPetPhotoCommandValidator : AbstractValidator<SetMainPetPhotoCommand>
{
    public SetMainPetPhotoCommandValidator()
    {
        RuleFor(d => d.VolunteerId)
            .NotEmpty().
            WithMessage("VolunteerId cannot be empty.");
        
        RuleFor(d => d.PetId)
            .NotEmpty().
            WithMessage("PetId cannot be empty.");
        
        RuleFor(d => d.FilePath)
            .NotEmpty().
            NotNull().
            WithMessage("FilePath cannot be null or empty.");
    }
}