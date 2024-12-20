﻿using CSharpFunctionalExtensions;
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
using PawShelter.Volunteers.Domain.ValueObjects;

namespace PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePetPhotos;

public class UpdatePetPhotosHandler : ICommandHandler<Guid, UpdatePetPhotosCommand>
{
    private readonly ILogger<UpdatePetPhotosHandler> _logger;
    private readonly IMessageQueue<IEnumerable<PhotoMetaData>> _messageQueue;
    private readonly IPhotoProvider _photoProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteerRepository _volunteerRepository;

    public UpdatePetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IPhotoProvider photoProvider,
        [FromKeyedServices(ModulesName.Volunteers)]IUnitOfWork unitOfWork,
        ILogger<UpdatePetPhotosHandler> logger,
        IMessageQueue<IEnumerable<PhotoMetaData>> messageQueue)
    {
        _volunteerRepository = volunteerRepository;
        _photoProvider = photoProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _messageQueue = messageQueue;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePetPhotosCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        
        try
        {
            var volunteerResult = await _volunteerRepository.
                GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petResult = await _volunteerRepository.
                GetPetById(PetId.Create(command.PetId), cancellationToken);

            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();

            List<PhotoData> photosData = [];
            foreach (var file in command.Files)
            {
                var filePathResult = FilePath.Create(file.FileName);

                if(filePathResult.IsFailure)
                    return filePathResult.Error.ToErrorList();

                var fileData = 
                    new PhotoData(file.Content, filePathResult.Value, Constants.Shared.BucketNamePhotos);
                
                photosData.Add(fileData);
            }

            List<PetPhoto> petPhotos = [];
            if (petResult.Value.Photos is not null)
            {
                petPhotos.AddRange(petResult.Value.Photos);
            }
            petPhotos.AddRange(
                photosData.Select(
                    f => new PetPhoto(f.FilePath, false)).ToList());
            
            volunteerResult.Value.UpdatePetPhotos(petResult.Value, petPhotos);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var uploadResult = await _photoProvider.UploadFiles(photosData, cancellationToken);

            if (uploadResult.IsFailure)
            {
                await _messageQueue.
                    WriteAsync(photosData.Select(
                        p => new PhotoMetaData(p.BucketName, p.FilePath)), cancellationToken);

                return uploadResult.Error.ToErrorList();
            }
            
            transaction.Commit();

            return petResult.Value.Id.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not add photos to pet - {id} in transaction", command.PetId);

            transaction.Rollback();

            return Errors.General.
                Failed(new ErrorParameters.General.Failed("Can not add photos to pet")).ToErrorList();
        }
    }
}