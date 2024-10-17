using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Extensions;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.Entities;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;
using Serilog;

namespace PawShelter.Application.Species.AddBreed;

public class AddBreedHandler
{
    private IValidator<AddBreedCommand> _validator;
    private readonly ILogger<AddBreedHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;

    public AddBreedHandler(ILogger<AddBreedHandler> logger,
        IValidator<AddBreedCommand> validator,
        ISpeciesRepository speciesRepository)
    {
        _validator = validator;
        _logger = logger;
        _speciesRepository = speciesRepository;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        AddBreedCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var speciesResult = await _speciesRepository.GetById(
           SpeciesId.Create(command.SpeciesId), cancellationToken);
        
        if(speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();
        
        var breedExistResult = await _speciesRepository.ExistBreed(command.BreedName, cancellationToken);

        if (breedExistResult.IsSuccess)
        {
            return Error.Conflict(
                "breed.already.exists", "Breed already exists").ToErrorList();
        }
        
        var breedId = BreedId.NewBreedId();
        
        speciesResult.Value.AddBreed(
            Breed.Create(breedId, command.BreedName).Value);

        await _speciesRepository.Save(speciesResult.Value, cancellationToken);
        
        _logger.LogInformation("Breed {breed} has been added", command.BreedName);
        
        return breedId.Value;
    }
}