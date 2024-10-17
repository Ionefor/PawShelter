using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawShelter.Application.FileProvider;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Files.Upload;

public class UploadFileHandler
{
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<UploadFileHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UploadFileHandler(
        IFileProvider fileProvider,
        ILogger<UploadFileHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _fileProvider = fileProvider;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<FilePath, Error>> Handle(
        UploadFileCommand command,
        CancellationToken cancellationToken)
    {
        var pathFile = FilePath.Create();
        
        var fileDate = new FileData(command.Stream, pathFile, command.BucketName);
        
        var path =  await _fileProvider.UploadFile(fileDate, cancellationToken);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("File with path {path} has been uploaded", path);

        return path;
    }
}