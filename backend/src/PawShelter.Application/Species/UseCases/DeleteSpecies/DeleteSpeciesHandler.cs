using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Domain.Shared;
using PawShelter.Domain.SpeciesManagement.ValueObjects.Ids;

namespace PawShelter.Application.Species.UseCases.DeleteSpecies;

public class DeleteSpeciesHandler :
    ICommandHandler<Guid, DeleteSpeciesCommand>
{
    private readonly IReadDbContext _context;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpeciesHandler> _logger;

    public DeleteSpeciesHandler(
        IReadDbContext context,
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSpeciesHandler> logger)
    {
        _context = context;
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteSpeciesCommand command,
        CancellationToken cancellationToken = default)
    {
        var petWithSpecies = await _context.Pets.
            AnyAsync(p => p.SpeciesId == command.SpeciesId, cancellationToken);
        
        if (petWithSpecies)
            return Errors.Extra.InvalidDeleteOperation(command.SpeciesId, "species").ToErrorList();

        var speciesId = SpeciesId.Create(command.SpeciesId);
        
        var speciesResult = await _speciesRepository.GetById(speciesId, cancellationToken);
        if(speciesResult.IsFailure)
            return speciesResult.Error.ToErrorList();
        
        _speciesRepository.DeleteSpecies(speciesResult.Value);
        _unitOfWork.SaveChanges();
        
        _logger.LogInformation(
            $"Species {speciesResult.Value.Value} has been deleted");
        
        return speciesResult.Value.Id.Value;
    }
}