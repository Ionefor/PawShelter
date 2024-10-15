using CSharpFunctionalExtensions;
using PawShelter.Application.FileProvider;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Files.Upload;

public class UploadFileHandler
{
    private readonly IFileProvider _fileProvider;
    
    public UploadFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    public async Task<Result<string, Error>> Handle(
        UploadFileCommand command,
        CancellationToken cancellationToken)
    {
        var fileDate = new FileData(command.Stream, command.BucketName, command.ObjectName);
        
        return await _fileProvider.UploadFile(fileDate, cancellationToken);
    }
}