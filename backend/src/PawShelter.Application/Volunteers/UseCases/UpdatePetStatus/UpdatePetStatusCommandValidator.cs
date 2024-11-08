using FluentValidation;
using PawShelter.Application.Validation;
using PawShelter.Domain.PetsManagement;

namespace PawShelter.Application.Volunteers.UseCases.UpdatePetStatus;

public class UpdatePetStatusCommandValidator : AbstractValidator<UpdatePetStatusCommand>
{
   public UpdatePetStatusCommandValidator()
   {
      RuleFor(u => u.VolunteerId).NotEmpty().WithMessage("VolunteerId cannot be empty");
      
      RuleFor(u => u.PetId).NotEmpty().WithMessage("PetId cannot be empty");

      RuleFor(u => u.Status).MustBeEnum
         <UpdatePetStatusCommand, string, PetStatus>();
   }
}