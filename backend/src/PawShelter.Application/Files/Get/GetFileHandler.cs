using CSharpFunctionalExtensions;
using PawShelter.Application.FileProvider;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.Files.Get;

public class GetFileHandler
{
    private readonly IFileProvider _fileProvider;
    
    public GetFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    
    public async Task<Result<string, Error>> Handle(
        GetFileCommand command,
        CancellationToken cancellationToken)
    {
        var fileData = new FileMetaData(
            command.FileData.BucketName, command.FileData.ObjectName);

       return  await _fileProvider.GetFileByObjectName(fileData, cancellationToken);
    }
}