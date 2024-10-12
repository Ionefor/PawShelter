﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Extensions;
using PawShelter.Domain.PetsManagement.ValueObjects.ForVolunteer;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworksHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdateSocialNetworksCommand> _validator;
    private readonly ILogger<UpdateSocialNetworksCommand> _logger;
      
    public UpdateSocialNetworksHandler(IVolunteerRepository volunteerRepository,
        IValidator<UpdateSocialNetworksCommand> validator, 
        ILogger<UpdateSocialNetworksCommand> logger)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialNetworksCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid) 
            return validationResult.ToErrorList();
            
        var volunteerResult = await _volunteerRepository.
            GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

        var socialNetworks= command.SocialNetworksDto.SocialNetworks.
            Select(s => SocialNetwork.Create(s.Name, s.Link).Value);
            
        volunteerResult.Value.UpdateSocialNetworks(new SocialNetworks(socialNetworks));
            
        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

        _logger.LogInformation(
            "SocialNetworks of the Volunteer {firstName} {middleName} has been updated",
            volunteerResult.Value.FullName.FirstName,
            volunteerResult.Value.FullName.MiddleName);
           
        return result;
    }
}