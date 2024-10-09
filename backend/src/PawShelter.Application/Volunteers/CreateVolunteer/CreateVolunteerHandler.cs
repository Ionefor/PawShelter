using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using FluentValidation;
using PawShelter.Application.Extensions;
using PawShelter.Domain.Shared;
using PawShelter.Domain.Shared.ValueObjects;
using PawShelter.Domain.VolunteerModel;

namespace PawShelter.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<CreateVolunteerCommand> _validator;
        public CreateVolunteerHandler(IVolunteerRepository volunteerRepository,
            IValidator<CreateVolunteerCommand> validator)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
        }
        public async Task<Result<Guid, ErrorList>> Handle(
            CreateVolunteerCommand command, 
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                return validationResult.ToErrorList();
            
            var volunteerId = VolunteerId.NewVolonteerId();
            
            var firstNameResult = Name.Create(
                command.FullNameDto.firstName);
            
            var middleNameResult = Name.Create(
                command.FullNameDto.middleName);
            
            var lastNameResult = Name.Create(
                command.FullNameDto.lastName);

            var fullName = new FullName(
                firstNameResult.Value, 
                middleNameResult.Value, 
                lastNameResult.Value);

            var email = Email.Create(command.Email).Value;

            var description = Description.Create(command.Description).Value;

            var number = PhoneNumber.Create(command.PhoneNumber).Value;

            var experience = Experience.Create(command.Experience).Value;
            
            var requisites = new Requisites(command.Requisites.Select(r => 
                new Requisite(Name.Create(r.name).Value,
                    Description.Create(r.description).Value)).ToList());

            var socialNetworks = new SocialNetworks(command.SocialNetworks.Select(s =>
                SocialNetwork.Create(Name.Create(s.name).Value, s.link).Value).ToList());
            
            var volunteer = new Volunteer(
                volunteerId, fullName, email, description, 
                number, experience, requisites, socialNetworks);

            await _volunteerRepository.Add(volunteer, cancellationToken);

            return volunteer.Id.Value;
        }
    }
}
