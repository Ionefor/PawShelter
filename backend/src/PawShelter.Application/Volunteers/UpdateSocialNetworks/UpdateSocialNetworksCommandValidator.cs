using FluentValidation;
using PawShelter.Application.Validation;
using PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;

namespace PawShelter.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworksCommandValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialNetworksCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty();
        
        RuleForEach(u => u.SocialNetworksDto.SocialNetworks).
            MustBeValueObject(s => 
                SocialNetwork.Create(s.Name, s.Link));
    }
}