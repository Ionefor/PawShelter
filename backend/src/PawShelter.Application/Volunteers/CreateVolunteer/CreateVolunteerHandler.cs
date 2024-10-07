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
            CreateVolunteerRequest request, CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.NewVolonteerId();

            var firstNameResult = Name.Create(request.fullNameDto.firstName);
            if(firstNameResult.IsFailure)
                return firstNameResult.Error;

            var middleNameResult = Name.Create(request.fullNameDto.middleName);
            if (middleNameResult.IsFailure)
                return middleNameResult.Error;

            var lastNameResult = Name.Create(request.fullNameDto.lastName);
            if (lastNameResult.IsFailure)
                return lastNameResult.Error;

            var fullName = new FullName(
                firstNameResult.Value, middleNameResult.Value, lastNameResult.Value);

            var emailResult = Email.Create(request.email);
            if (emailResult.IsFailure)
                return emailResult.Error;

            var descriptionResult = Description.Create(request.description);
            if (descriptionResult.IsFailure)
                return descriptionResult.Error;

            var numberResult = PhoneNumber.Create(request.phoneNumber);
            if (numberResult.IsFailure)
                return numberResult.Error;

            var experienceResult = Experience.Create(request.experience);
            if (experienceResult.IsFailure)
                return experienceResult.Error;

            /*
               Нужно ли проверять в списках что все хорошо? И если нужно, то делать так?
           
            var requisitesListTemp = new List<Requisite>();

            foreach (var requisite in request.requisites)
            {
                var nameResult = Name.Create(requisite.name);
                if(nameResult.IsFailure)
                    return nameResult.Error;

                var descResult = Description.Create(requisite.description);
                if (descResult.IsFailure)
                    return descResult.Error;

                var requisiteVo = new Requisite(nameResult.Value, descResult.Value);

                requisitesListTemp.Add(requisiteVo);
            }

            var requisites = new Requisites(requisitesListTemp);

             */
            var requisitesList = request.requisites.Select(r => 
                new Requisite(
                    Name.Create(r.name).Value, Description.Create(r.description).Value)).ToList();

            var requisites = new Requisites(requisitesList);

            var socialNetworksList = request.socialNetworks.Select(s =>
                SocialNetwork.Create(Name.Create(s.name).Value, s.link).Value).ToList();

            var socialNetworks = new SocialNetworks(socialNetworksList);

            var volunteer = new Volunteer(
                volunteerId, fullName, emailResult.Value, descriptionResult.Value, 
                numberResult.Value, experienceResult.Value, requisites, socialNetworks);

            await _volunteerRepository.Add(volunteer, cancellationToken);

            return volunteer.Id.Value;
        }
    }
}
