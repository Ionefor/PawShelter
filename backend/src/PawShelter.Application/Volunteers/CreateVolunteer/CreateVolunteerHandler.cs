using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CreateVolunteerHandler> _logger;
      
        public CreateVolunteerHandler(IVolunteerRepository volunteerRepository,
            IValidator<CreateVolunteerCommand> validator, 
            ILogger<CreateVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }
      
        public async Task<Result<Guid, ErrorList>> Handle(
            CreateVolunteerCommand command, 
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid) 
                return validationResult.ToErrorList();
            
            var volunteerId = VolunteerId.NewVolonteerId();
            
            var fullName = FullName.Create(
                command.FullNameDto.firstName,
                command.FullNameDto.middleName,
                command.FullNameDto.lastName).Value;
            
            var email = Email.Create(command.Email).Value;

            var description = Description.Create(command.Description).Value;

            var number = PhoneNumber.Create(command.PhoneNumber).Value;

            var experience = Experience.Create(command.Experience).Value;
            
            var requisites = new Requisites(command.Requisites.Select(r => 
                new Requisite(r.name, r.description)).ToList());
            
            var socialNetworks = new SocialNetworks(command.SocialNetworks.Select(s =>
                SocialNetwork.Create(s.name, s.link).Value).ToList());
            
            var volunteer = new Volunteer(
                volunteerId, fullName, email, description, 
                number, experience, requisites, socialNetworks);
            
            await _volunteerRepository.Add(volunteer, cancellationToken);
 
            _logger.LogInformation(
                "Volunteer {firstName} {middleName} created with id: {volunteerId}",
                fullName.FirstName, fullName.MiddleName, volunteerId.Value);
            
            return volunteer.Id.Value;
        }
    }
}
