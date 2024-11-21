using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;

namespace PawShelter.Species.Application.Species.Commands.AddSpecies;

public class AddSpeciesHandler : ICommandHandler<Guid, AddSpeciesCommand>
{
    private readonly ILogger<AddSpeciesHandler> _logger;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IValidator<AddSpeciesCommand> _validator;
    public AddSpeciesHandler(
        ILogger<AddSpeciesHandler> logger,
        IValidator<AddSpeciesCommand> validator,
        ISpeciesRepository speciesRepository)
    {
        _logger = logger;
        _validator = validator;
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddSpeciesCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var speciesExistResult = await _speciesRepository.ExistSpecies(command.Species, cancellationToken);

        if (speciesExistResult.IsSuccess)
        {
            return Errors.Extra.AlreadyExists(
                new ErrorParameters.Extra.ValueAlreadyExists("Species already exists")).ToErrorList();
        }

        var speciesId = SpeciesId.NewGuid();

        await _speciesRepository.
            Add(Domain.Aggregate.Species.Create(speciesId, command.Species).Value, cancellationToken);

        _logger.LogInformation("Species {species} has been added", command.Species);

        return speciesId.Id;
    }
}