using CSharpFunctionalExtensions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Models.Error;
using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Volunteers.Application.PhotoProvider;

public interface IPhotoProvider
{
    Task<Result<FilePath, Error>> UploadFile(
        PhotoData fileData, CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<PhotoData> filesData, CancellationToken cancellationToken = default);

    Task<UnitResult<Error>> DeleteFile(
        PhotoMetaData photoData, CancellationToken cancellationToken = default);
}