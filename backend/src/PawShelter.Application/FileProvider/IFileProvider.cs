using CSharpFunctionalExtensions;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.FileProvider;

public interface IFileProvider
{
    Task<Result<FilePath, Error>> UploadFile(
        FileData fileData, CancellationToken cancellationToken = default);
    
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData, CancellationToken cancellationToken = default);
    
    Task<Result<FilePath, Error>> DeleteFile(
        FileMetaData fileData, CancellationToken cancellationToken = default);
    
    Task<Result<FilePath, Error>> GetFileByObjectName(
        FileMetaData fileData, CancellationToken cancellationToken = default);

    public Result<IReadOnlyCollection<FilePath>> GetFiles();
}