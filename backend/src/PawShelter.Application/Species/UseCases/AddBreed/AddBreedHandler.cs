using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Extensions;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.Entities;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Application.Species.UseCases.AddBreed;

public class AddBreedHandler : ICommandHandler<Guid, AddBreedCommand>
{
    private readonly ILogger<AddBreedHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddBreedCommand> _validator;

    public AddBreedHandler(ILogger<AddBreedHandler> logger,
        IValidator<AddBreedCommand> validator,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork)
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

        var speciesResult = await _speciesRepository.GetById(
            SpeciesId.Create(command.SpeciesId), cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();

        var breedExistResult = await _speciesRepository.ExistBreed(command.BreedName, cancellationToken);

        if (breedExistResult.IsSuccess)
            return Error.Conflict(
                "breed.already.exists", "Breed already exists").ToErrorList();

        var breedId = BreedId.NewBreedId();

        speciesResult.Value.AddBreed(
            Breed.Create(breedId, command.BreedName).Value);
        
       await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Breed {breed} has been added", command.BreedName);

        return breedId.Value;
    }
}