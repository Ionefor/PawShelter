using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Application.Species.UseCases.DeleteBreed;

public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
{
    private readonly IReadDbContext _context;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<DeleteBreedHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBreedHandler(
        IReadDbContext context,
        ISpeciesRepository speciesRepository,
        ILogger<DeleteBreedHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _context = context;
        _speciesRepository = speciesRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var petsWithBreed = await _context.Pets.
            AnyAsync(p => p.BreedId == command.BreedId, cancellationToken);
        
        if(petsWithBreed)
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
        
        _unitOfWork.SaveChanges();
        
        _logger.LogInformation(
            $"Breed {breed.Value} has been deleted");

        return breedId.Value;
    }
}