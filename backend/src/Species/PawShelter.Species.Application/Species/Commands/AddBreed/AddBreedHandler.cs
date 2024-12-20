﻿using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Species.Domain.Entities;

namespace PawShelter.Species.Application.Species.Commands.AddBreed;

public class AddBreedHandler : ICommandHandler<Guid, AddBreedCommand>
{
    private readonly ILogger<AddBreedHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddBreedCommand> _validator;

    public AddBreedHandler(ILogger<AddBreedHandler> logger,
        IValidator<AddBreedCommand> validator,
        ISpeciesRepository speciesRepository,
        [FromKeyedServices(ModulesName.Species)]IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _logger = logger;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddBreedCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var speciesResult = await _speciesRepository.
            GetById(SpeciesId.Create(command.SpeciesId), cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedExistResult = await _speciesRepository.ExistBreed(command.BreedName, cancellationToken);

        if (breedExistResult.IsSuccess)
        {
            return Errors.Extra.AlreadyExists(
                new ErrorParameters.Extra.ValueAlreadyExists("Breed already exists")).ToErrorList();
        }

        var breedId = BreedId.NewGuid();

        speciesResult.Value.
            AddBreed(Breed.Create(breedId, command.BreedName).Value);
        
       await _unitOfWork.SaveChangesAsync(cancellationToken);
       
       _logger.LogInformation("Breed {breed} has been added", command.BreedName);

       return breedId.Id;
    }
}