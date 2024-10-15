using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.FileProvider;

public interface IFileProvider
{
    Task<Result<string, Error>> UploadFile(
        FileData fileData, CancellationToken cancellationToken = default);
    
    Task<Result<string, Error>> DeleteFile(
        FileMetaData fileData, CancellationToken cancellationToken = default);
    
    Task<Result<string, Error>> GetFileByObjectName(
        FileMetaData fileData, CancellationToken cancellationToken = default);

    public Result<IReadOnlyCollection<string>> GetFiles();
}