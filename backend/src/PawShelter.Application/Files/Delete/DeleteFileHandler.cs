using CSharpFunctionalExtensions;
using PawShelter.Application.FileProvider;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Files.Delete;

public class DeleteFileHandler
{
    private readonly IFileProvider _fileProvider;
    
    public DeleteFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    
    public async Task<Result<string, Error>> Handle(
        DeleteFileCommand command,
        CancellationToken cancellationToken)
    {
        var fileData = new FileMetaData(
            command.FileMetaData.BucketName, command.FileMetaData.ObjectName);
        
        return await _fileProvider.
            DeleteFile(fileData, cancellationToken);
    }
}