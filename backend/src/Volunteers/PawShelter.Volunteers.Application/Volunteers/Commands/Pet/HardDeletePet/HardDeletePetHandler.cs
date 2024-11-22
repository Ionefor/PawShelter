using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Messaging;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Volunteers.Application.PhotoProvider;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.HardDeletePet;

public class HardDeletePetHandler : 
    ICommandHandler<Guid, HardDeletePetCommand>
{
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<HardDeletePetHandler> _logger;
    private readonly IPhotoProvider _photoProvider;
    private readonly IMessageQueue<IEnumerable<PhotoMetaData>> _messageQueue;

    public HardDeletePetHandler(
        IReadDbContext readDbContext,
        [FromKeyedServices(ModulesName.Volunteers)]IUnitOfWork unitOfWork,
        IVolunteerRepository volunteerRepository,
        ILogger<HardDeletePetHandler> logger,
        IPhotoProvider photoProvider,
        IMessageQueue<IEnumerable<PhotoMetaData>> messageQueue)
    {
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _photoProvider = photoProvider;
        _messageQueue = messageQueue;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        HardDeletePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerExist = _readDbContext.Volunteers.
            Any(v => v.Id == command.VolunteerId);
        
        if (!volunteerExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.General.NotFound(
                    nameof(Volunteer), nameof(VolunteerId), command.VolunteerId)).ToErrorList();
        }
            
        var petExist = _readDbContext.Pets.
            Any(
                p => p.Id == command.PetId &&
                     p.VolunteerId == command.VolunteerId);
        
        if (!petExist)
        {
            return Errors.General.NotFound(
                new ErrorParameters.General.NotFound(
                    nameof(Pet), nameof(PetId), command.PetId)).ToErrorList();
        }
        
        var volunteerResult = await _volunteerRepository.
            GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var petId = PetId.Create(command.PetId);
        
        var petForDelete = await _volunteerRepository.
            GetPetById(petId, cancellationToken);
        
        if(petForDelete.IsFailure)
            return petForDelete.Error.ToErrorList();

        var photosToDelete = petForDelete.Value.Photos;
        
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        
        try
        {
            if (photosToDelete is not null)
            {
                foreach (var photo in photosToDelete)
                {
                    var photoToDeleteMeta = new PhotoMetaData(Constants.Shared.BucketNamePhotos, photo.Path);
                
                    var deleteResult = await _photoProvider.
                        DeleteFile(photoToDeleteMeta, cancellationToken);

                    if(deleteResult.IsFailure)
                    {
                        await _messageQueue.WriteAsync(
                            photosToDelete.Select(
                                p => new PhotoMetaData(Constants.Shared.BucketNamePhotos, p.Path)),
                            cancellationToken);

                        return deleteResult.Error.ToErrorList();
                    }
                }
            }
            
            volunteerResult.Value.HardDeletePet(petForDelete.Value);
        
           await _unitOfWork.SaveChangesAsync(cancellationToken);
           
           transaction.Commit();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not delete photo - {id} in transaction", command.PetId);

            transaction.Rollback();

            return Errors.General.
                Failed(new ErrorParameters.General.Failed("Can not delete pet")).ToErrorList();
        }
        
        return petForDelete.Value.Id.Id;
    }
}