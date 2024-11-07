using FluentValidation;

namespace PawShelter.Application.Volunteers.UseCases.DeletePetPhoto;

public class DeletePetPhotoCommandValidator 
    : AbstractValidator<DeletePetPhotoCommand>
{
    public DeletePetPhotoCommandValidator()
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