using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Messaging;
using PawShelter.Application.PhotoProvider;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.UseCases.UpdateSocialNetworks;

public class HardDeletePetHandler : 
    ICommandHandler<Guid, HardDeletePetCommand>
{
    private const string BUCKET_NAME = "photos";
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<HardDeletePetHandler> _logger;
    private readonly IPhotoProvider _photoProvider;
    private readonly IMessageQueue<IEnumerable<PhotoMetaData>> _messageQueue;

    public HardDeletePetHandler(
        IReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
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
            return Error.NotFound(
                "volunteer.not.found", "Volunteer not found").ToErrorList();
        }
            
        var petExist = _readDbContext.Pets.
            Any(
                p => p.Id == command.PetId &&
                     p.VolunteerId == command.VolunteerId);
        
        if (!petExist)
        {
            return Error.NotFound(
                "pet.not.found", "pet not found").ToErrorList();
        }
        
        var volunteerResult =
            await _volunteerRepository.GetById(
                VolunteerId.Create(command.VolunteerId), cancellationToken);

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
                    var photoToDeleteMeta = new PhotoMetaData(BUCKET_NAME, photo.Path);
                
                    var deleteResult = await _photoProvider.
                        DeleteFile(photoToDeleteMeta, cancellationToken);

                    if(deleteResult.IsFailure)
                    {
                        await _messageQueue.WriteAsync(
                            photosToDelete.Select(
                                p => new PhotoMetaData(BUCKET_NAME, p.Path)),
                            cancellationToken);

                        return deleteResult.Error.ToErrorList();
                    }
                }
            }
            
            volunteerResult.Value.DeletePet(petForDelete.Value);
        
           _unitOfWork.SaveChanges();
           
           transaction.Commit();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not delete photo - {id} in transaction", command.PetId);

            transaction.Rollback();

            return Error.Failure(
                "pet.photos.failure", "Can not delete photo").ToErrorList();
        }
        
        return petForDelete.Value.Id.Value;
    }
}