using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Extensions;
using PawShelter.Application.Messaging;
using PawShelter.Application.PhotoProvider;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.UseCases.DeletePetPhoto;

public class DeletePetPhotoHandler :
    ICommandHandler<string, DeletePetPhotoCommand>
{
    private const string BUCKET_NAME = "photos";
    private readonly IValidator<DeletePetPhotoCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotoProvider _photoProvider;
    private readonly ILogger<DeletePetPhotoHandler> _logger;

    public DeletePetPhotoHandler(
        IValidator<DeletePetPhotoCommand> validator,
        IVolunteerRepository volunteerRepository,
        IUnitOfWork unitOfWork,
        IPhotoProvider photoProvider,
        IMessageQueue<IEnumerable<PhotoMetaData>> messageQueue,
        ILogger<DeletePetPhotoHandler> logger)
    {
        _validator = validator;
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _photoProvider = photoProvider;
        _logger = logger;
    }
    public async Task<Result<string, ErrorList>> Handle(
        DeletePetPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        
        try
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                return validationResult.ToErrorList();

            var petResult = await _volunteerRepository.GetPetById(
                PetId.Create(command.PetId), cancellationToken);

            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();

            var filePath = FilePath.ToFilePath(command.FilePath);

            if (petResult.Value.Photos is null)
            {
                return Error.NotFound(
                    "photos.is.empty", "photos is empty").ToErrorList();
            }

            var photoToDelete = petResult.Value.Photos.
                FirstOrDefault(p => p.Path.Path == filePath.Path);
            
            if (photoToDelete is null)
            {
                return Error.NotFound(
                    "photo.not.found", "photo for delete not found").ToErrorList();
            }
            
            var petPhotos = petResult.Value.Photos.ToList();
            petPhotos.Remove(photoToDelete);
            
            petResult.Value.UpdatePetPhotos(petPhotos);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            var photoToDeleteMeta = new PhotoMetaData(BUCKET_NAME, photoToDelete.Path);
            
            var deleteResult = await _photoProvider.
                DeleteFile(photoToDeleteMeta, cancellationToken);
            
            if (deleteResult.IsFailure)
                return deleteResult.Error.ToErrorList();
            
            transaction.Commit();
            
            return photoToDelete.Path.Path;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not delete photo - {id} in transaction", command.PetId);

            transaction.Rollback();

            return Error.Failure(
                "pet.photos.failure", "Can not delete photo").ToErrorList();
        }
    }
}