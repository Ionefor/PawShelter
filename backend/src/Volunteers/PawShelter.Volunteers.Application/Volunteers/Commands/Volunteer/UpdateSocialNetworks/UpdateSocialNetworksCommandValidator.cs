using FluentValidation;
using PawShelter.Core.Validation;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateSocialNetworks;

public class UpdateSocialNetworksCommandValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialNetworksCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty();

        RuleForEach(u => u.SocialNetworksDto.SocialNetworks).MustBeValueObject(s =>
            SocialNetwork.Create(s.Name, s.Link));
    }
}