using FluentValidation;
using PawShelter.Core.Validation;
using PawShelter.SharedKernel;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePetStatus;

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