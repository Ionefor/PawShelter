using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.Volunteers.Contracts;

namespace PawShelter.Species.Application.Species.Commands.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteersContract _volunteersContract;

    public DeleteBreedHandler(
        ISpeciesRepository speciesRepository,
        ILogger<DeleteBreedHandler> logger,
        [FromKeyedServices("Species")]IUnitOfWork unitOfWork,
        IVolunteersContract volunteersContract)
    {
        _speciesRepository = speciesRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _volunteersContract = volunteersContract;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var petWithBreed = await _volunteersContract.
            PetWithBreed(command.BreedId, cancellationToken);
        
        if(petWithBreed is not null)
            return Errors.Extra.InvalidDeleteOperation(command.BreedId, "breed").ToErrorList();
        
        var speciesId = SpeciesId.Create(command.SpeciesId);
        var speciesResult = await _speciesRepository.GetById(speciesId, cancellationToken);
        
        if (speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();
        
        var breedId = BreedId.Create(command.BreedId);
        var breed = speciesResult.Value.Breeds.FirstOrDefault(b => b.Id == breedId);
        
        if (breed is null)
            return Errors.General.NotFound(command.BreedId).ToErrorList();
        
        speciesResult.Value.DeleteBreed(breed);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation(
            $"Breed {breed.Value} has been deleted");
        
        return breedId.Value;
    }
}