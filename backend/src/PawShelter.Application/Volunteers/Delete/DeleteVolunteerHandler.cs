﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.Delete;

public class DeleteVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerCommand> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<DeleteVolunteerCommand> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
      
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteVolunteerCommand command, 
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteerRepository.
            GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);
            
        if(volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation(
            "Volunteer {firstName} {middleName} has been deleted", 
                volunteerResult.Value.FullName.FirstName,
                volunteerResult.Value.FullName.MiddleName);
           
        return volunteerResult.Value.Id.Value;
    }
}
    
    
    
