using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Extensions;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.Aggregate;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Application.Species.AddSpecies;

public class AddSpeciesHandler
{
    private readonly ILogger<AddSpeciesHandler> _logger;
    private readonly IValidator<AddSpeciesCommand> _validator;
    private readonly ISpeciesRepository _speciesRepository;

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
        
        var speciesExistResult = await _speciesRepository.
            ExistSpecies(command.Species, cancellationToken);

        if (speciesExistResult.IsSuccess)
        {
            return Error.Conflict(
                "species.already.exists", "Species already exists").ToErrorList();
        }
        
        var speciesId = SpeciesId.NewSpeciesId();
        
        await _speciesRepository.Add(PawShelter.Domain.SpeciesManagement.Aggregate.
            Species.Create(speciesId, command.Species).Value, cancellationToken);

        _logger.LogInformation("Species {species} has been added", command.Species);
        
        return speciesId.Value;
    }
}