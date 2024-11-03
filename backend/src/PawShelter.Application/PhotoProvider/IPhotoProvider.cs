using CSharpFunctionalExtensions;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.Shared;

namespace PawShelter.Application.PhotoProvider;

public interface IPhotoProvider
{
    Task<Result<FilePath, Error>> UploadFile(
        PhotoData fileData, CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<PhotoData> filesData, CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> DeleteFile(
        PhotoMetaData photoData, CancellationToken cancellationToken = default);
}