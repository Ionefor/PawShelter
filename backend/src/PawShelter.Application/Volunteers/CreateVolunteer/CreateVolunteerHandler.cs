using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;
using PawShelter.Domain.Shared.ValueObjects;
using PawShelter.Domain.VolunteerModel;

namespace PawShelter.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }
        public async Task<Result<Guid, Error>> Handle(
            CreateVolunteerCommand command, CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.NewVolonteerId();

            var firstNameResult = Name.Create(command.fullNameDto.firstName);
            if(firstNameResult.IsFailure)
                return firstNameResult.Error;

            var middleNameResult = Name.Create(command.fullNameDto.middleName);
            if (middleNameResult.IsFailure)
                return middleNameResult.Error;

            var lastNameResult = Name.Create(command.fullNameDto.lastName);
            if (lastNameResult.IsFailure)
                return lastNameResult.Error;

            var fullName = new FullName(
                firstNameResult.Value, middleNameResult.Value, lastNameResult.Value);

            var emailResult = Email.Create(command.email);
            if (emailResult.IsFailure)
                return emailResult.Error;

            var descriptionResult = Description.Create(command.description);
            if (descriptionResult.IsFailure)
                return descriptionResult.Error;

            var numberResult = PhoneNumber.Create(command.phoneNumber);
            if (numberResult.IsFailure)
                return numberResult.Error;

            var experienceResult = Experience.Create(command.experience);
            if (experienceResult.IsFailure)
                return experienceResult.Error;
           
            var requisiteList = new List<Requisite>();

            foreach (var requisite in command.requisites)
            {
                var nameResult = Name.Create(requisite.name);
                if(nameResult.IsFailure)
                    return nameResult.Error;

                var descResult = Description.Create(requisite.description);
                if (descResult.IsFailure)
                    return descResult.Error;

                var requisiteVo = new Requisite(nameResult.Value, descResult.Value);

                requisiteList.Add(requisiteVo);
            }

            var requisites = new Requisites(requisiteList);

            var socialNetworkList = new List<SocialNetwork>();

            foreach (var social in command.socialNetworks)
            {
                var nameResult = Name.Create(social.name);
                if (nameResult.IsFailure)
                    return nameResult.Error;

                var socialNetworksVo = SocialNetwork.Create(nameResult.Value, social.link);

                socialNetworkList.Add(socialNetworksVo.Value);
            }

            var socialNetworks = new SocialNetworks(socialNetworkList);

            var volunteer = new Volunteer(
                volunteerId, fullName, emailResult.Value, descriptionResult.Value, 
                numberResult.Value, experienceResult.Value, requisites, socialNetworks);

            await _volunteerRepository.Add(volunteer, cancellationToken);

            return volunteer.Id.Value;
        }
    }
}
