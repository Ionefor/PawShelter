using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Database;
using PawShelter.Application.Messaging;
using PawShelter.Application.PhotoProvider;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.UseCases.AddPetPhotos;

public class AddPetPhotosHandler : ICommandHandler<Guid, AddPetPhotosCommand>
{
    private const string BUCKET_NAME = "photos";
    private readonly ILogger<AddPetPhotosHandler> _logger;
    private readonly IMessageQueue<IEnumerable<PhotoMetaData>> _messageQueue;
    private readonly IPhotoProvider _photoProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVolunteerRepository _volunteerRepository;

    public AddPetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IPhotoProvider photoProvider,
        IUnitOfWork unitOfWork,
        ILogger<AddPetPhotosHandler> logger,
        IMessageQueue<IEnumerable<PhotoMetaData>> messageQueue)
    {
        _volunteerRepository = volunteerRepository;
        _photoProvider = photoProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _messageQueue = messageQueue;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetPhotosCommand command,
        CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var volunteerResult = await _volunteerRepository.GetById(
                VolunteerId.Create(command.VolunteerId), cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petResult = await _volunteerRepository.GetPetById(
                PetId.Create(command.PetId), cancellationToken);

            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();

            List<PhotoData> photosData = [];
            foreach (var file in command.Files)
            {
                var filePath = FilePath.Create();

                var fileData = new PhotoData(file.Content, filePath, BUCKET_NAME);
                photosData.Add(fileData);
            }

            var petPhotos = photosData.Select(
                f => new PetPhoto(f.FilePath, false)).ToList();

            petResult.Value.UpdatePetPhotos(petPhotos);

            await _unitOfWork.SaveChanges(cancellationToken);

            var uploadResult = await _photoProvider.UploadFiles(photosData, cancellationToken);

            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(
                    photosData.Select(
                        p => new PhotoMetaData(p.BucketName, p.FilePath)), cancellationToken);

                return uploadResult.Error.ToErrorList();
            }


            transaction.Commit();

            return petResult.Value.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Can not add photos to pet - {id} in transaction", command.PetId);

            transaction.Rollback();

            return Error.Failure(
                "pet.photos.failure", "Can not add photos to pet").ToErrorList();
        }
    }
}