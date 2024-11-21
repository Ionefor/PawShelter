using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Contracts;

namespace PawShelter.Species.Application.Species.Commands.DeleteSpecies;

public class DeleteSpeciesHandler :
    ICommandHandler<Guid, DeleteSpeciesCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesHandler> _logger;
    private readonly IVolunteersContract _volunteersContract;

    public DeleteSpeciesHandler(
        ISpeciesRepository speciesRepository,
        [FromKeyedServices("Species")]IUnitOfWork unitOfWork,
        ILogger<DeleteSpeciesHandler> logger,
        IVolunteersContract volunteersContract)
    {
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _volunteersContract = volunteersContract;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteSpeciesCommand command,
        CancellationToken cancellationToken = default)
    {
        var petWithSpecies = await _volunteersContract.
            PetWithSpecies(command.SpeciesId, cancellationToken);
        
        if (petWithSpecies is not null)
            return Errors.Extra.InvalidDeleteOperation(command.SpeciesId, "species").ToErrorList();
        
        var speciesId = SpeciesId.Create(command.SpeciesId);
        
        var speciesResult = await _speciesRepository.GetById(speciesId, cancellationToken);
        if(speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();
        
        _speciesRepository.DeleteSpecies(speciesResult.Value);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            $"Species {speciesResult.Value.Value} has been deleted");
        
        return speciesResult.Value.Id;
    }
}