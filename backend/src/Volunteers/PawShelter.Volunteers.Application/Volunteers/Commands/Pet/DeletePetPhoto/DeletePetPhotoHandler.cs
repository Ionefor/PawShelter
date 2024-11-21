using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawShelter.Core.Abstractions;
using PawShelter.Core.Extensions;
using PawShelter.Core.Messaging;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Definitions;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;
using PawShelter.SharedKernel.ValueObjects.Ids;
using PawShelter.Volunteers.Application.PhotoProvider;
using PawShelter.Volunteers.Domain.ValueObjects;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.DeletePetPhoto;

public class DeletePetPhotoHandler :
    ICommandHandler<string, DeletePetPhotoCommand>
{
    private readonly IValidator<DeletePetPhotoCommand> _validator;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotoProvider _photoProvider;
    private readonly ILogger<DeletePetPhotoHandler> _logger;

    public DeletePetPhotoHandler(
        IValidator<DeletePetPhotoCommand> validator,
        IVolunteerRepository volunteerRepository,
        [FromKeyedServices(ModulesName.Volunteers)]IUnitOfWork unitOfWork,
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

            var petResult = await _volunteerRepository.
                GetPetById(PetId.Create(command.PetId), cancellationToken);

            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();

            var filePath = FilePath.ToFilePath(command.FilePath);

            if (petResult.Value.Photos is null)
            {
                return Errors.General.ValueIsRequired(
                    new ErrorParameters.General.ValueIsRequired(nameof(PetPhoto))).ToErrorList();
            }

            var photoToDelete = petResult.Value.Photos.
                FirstOrDefault(p => p.Path.Value == filePath.Value);
            
            if (photoToDelete is null)
            {
                return Errors.General.NotFound(
                    new ErrorParameters.General.NotFound(
                        nameof(Pet), nameof(PetPhoto), filePath.Value)).ToErrorList();
            }
            
            var petPhotos = petResult.Value.Photos.ToList();
            petPhotos.Remove(photoToDelete);
            
            var volunteerResult = await _volunteerRepository.
                GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();
            
            volunteerResult.Value.UpdatePetPhotos(petResult.Value, petPhotos);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            var photoToDeleteMeta = new PhotoMetaData(Constants.Shared.BucketNamePhotos, photoToDelete.Path);
            
            var deleteResult = await _photoProvider.
                DeleteFile(photoToDeleteMeta, cancellationToken);
            
            if (deleteResult.IsFailure)
                return deleteResult.Error.ToErrorList();
            
            transaction.Commit();
            
            return photoToDelete.Path.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not delete photo - {id} in transaction", command.PetId);

            transaction.Rollback();

            return Errors.General.
                Failed(new ErrorParameters.General.Failed("Can not delete photo")).ToErrorList();
        }
    }
}