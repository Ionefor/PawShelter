using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawShelter.Application.FileProvider;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Files.Delete;

public class DeleteFileHandler
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<DeleteFileHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFileHandler(
        IFileProvider fileProvider,
        ILogger<DeleteFileHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _fileProvider = fileProvider;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<FilePath, Error>> Handle(
        DeleteFileCommand command,
        CancellationToken cancellationToken)
    {
        var fileData = new FileMetaData(
            command.FileMetaData.BucketName, command.FileMetaData.FilePath);
        
        var path = await _fileProvider.DeleteFile(fileData, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("File with path {FilePath} has been deleted", path);
        
        return path;
    }
}