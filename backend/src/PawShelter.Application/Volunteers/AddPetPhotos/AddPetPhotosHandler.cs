using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawShelter.Application.FileProvider;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.PetsManagement.ValueObjects.Ids;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Volunteers.AddPetPhotos;

public class AddPetPhotosHandler
{
    private readonly IFileProvider _fileProvider;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddPetPhotosHandler> _logger;
    private const string BUCKET_NAME = "photos";
    
    public AddPetPhotosHandler(
        IVolunteerRepository volunteerRepository,
        IFileProvider fileProvider,
        IUnitOfWork unitOfWork,
        ILogger<AddPetPhotosHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
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
            
            if(volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();
            
            var petResult = await _volunteerRepository.GetPetById(
                PetId.Create(command.PetId), cancellationToken);
            
            if(petResult.IsFailure)
                return petResult.Error.ToErrorList();

            List<FileData> filesData = [];
            foreach (var file in command.Files)
            {
                var filePath = FilePath.Create();

                var fileData = new FileData(file.Content, filePath, BUCKET_NAME);
                filesData.Add(fileData);
            }

            var petPhotos = filesData.Select(
                f => PetPhoto.Create(f.FilePath, false).Value).ToList();
            
            petResult.Value.UpdatePetPhotos(petPhotos);
            
            await _unitOfWork.SaveChanges(cancellationToken);
            
            var uploadResult = await _fileProvider.UploadFiles(filesData, cancellationToken);
            
            if (uploadResult.IsFailure)
                return uploadResult.Error.ToErrorList();
            
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