﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Extensions;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.PetsManagement.ValueObjects.Shared;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.UpdateRequisites;

public class UpdateRequisitesHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<UpdateRequisitesCommand> _validator;
        private readonly ILogger<UpdateRequisitesCommand> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRequisitesHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<UpdateRequisitesCommand> validator, 
            ILogger<UpdateRequisitesCommand> logger,
            IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
      
        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateRequisitesCommand command, 
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid) 
                return validationResult.ToErrorList();
            
            var volunteerResult = await _volunteerRepository.
                GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

            var requisites = command.RequisitesDto.Requisites.
                Select(r => Requisite.Create(r.Name, r.Description).Value);
            
            volunteerResult.Value.UpdateRequisites(new Requisites(requisites));
            
            await _unitOfWork.SaveChanges(cancellationToken);
            
            _logger.LogInformation(
                "Requisites of the Volunteer {firstName} {middleName} has been updated",
                    volunteerResult.Value.FullName.FirstName,
                    volunteerResult.Value.FullName.MiddleName);
           
           return volunteerResult.Value.Id.Value;
        }
    }