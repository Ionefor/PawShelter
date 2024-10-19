using System.Reactive.Linq;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PawShelter.Application.PhotoProvider;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;
using PawShelter.Domain.Shared;

namespace PawShelter.Infrastructure.Providers;

public class MinioProvider : IPhotoProvider
{
    private const int MAX_DEGREE_OF_PARALLELISM = 10;
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(
        IMinioClient minioClient,
        ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<FilePath, Error>> UploadFile(
        PhotoData fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs().WithBucket(fileData.BucketName);

            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(fileData.BucketName);

                await _minioClient.MakeBucketAsync(
                    makeBucketArgs, cancellationToken);
            }

            var putObjectArgs = new PutObjectArgs().
                WithBucket(fileData.BucketName).
                WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream.Length).
                WithObject(fileData.FilePath.Path);

            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Fail to upload to MinIO");
            return Error.Failure("file.upload", "Fail to upload to MinIO");
        }
    }

    public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadFiles(
        IEnumerable<PhotoData> filesData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesList = filesData.ToList();

        try
        {
            await IfBucketsNotExistCreateBucket(filesList, cancellationToken);
            
            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));
            
            var pathsResult = await Task.WhenAll(tasks);
            
            if (pathsResult.Any(p => p.IsFailure))
                return pathsResult.First().Error;
            
            var results = pathsResult.Select(p => p.Value).ToList();
            
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload files in minio, files amount: {amount}", filesList.Count);
            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }

    public async Task<UnitResult<Error>> DeleteFile(
        PhotoMetaData photoData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var statObjectArgs = new StatObjectArgs()
                .WithBucket(photoData.BucketName)
                .WithObject(photoData.FilePath.Path);

            var objectStat = await _minioClient.StatObjectAsync(statObjectArgs, cancellationToken);
            if (objectStat is null)
                return Result.Success<Error>();
            
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(photoData.BucketName)
                .WithObject(photoData.FilePath.Path);
            
            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Fail delete file from MinIO");
            return Error.Failure("file.delete", "Fail delete file from MinIO");
        }
        
        return Result.Success<Error>();
    }

    public async Task<Result<FilePath, Error>> GetFileByObjectName(
        PhotoMetaData photoData, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                .WithBucket(photoData.BucketName)
                .WithObject(photoData.FilePath.Path).
                WithExpiry(60 * 60 * 24);

            var path = await _minioClient.PresignedGetObjectAsync(args);

            return photoData.FilePath;
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Fail to get file from minio");
            return Error.Failure("file.get", "Fail to get file from minio");
        }
    }
    
    public Result<IReadOnlyCollection<FilePath>> GetFiles()
    {
        var listObjectsArgs = new ListObjectsArgs().
            WithBucket("photos").
            WithRecursive(false);
        
        var objects = _minioClient.ListObjectsAsync(listObjectsArgs);
        
        List<string> paths = [];

        foreach (var item in objects)
        {
            paths.Add(item.Key);
        }
        
        return paths.Select(p => FilePath.Create(p).Value).ToList();
    }
    
    private async Task<Result<FilePath, Error>> PutObject(
        PhotoData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);
        
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.FilePath.Path); 
        
        try
        {
            await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);
            
            return fileData.FilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.FilePath.Path,
                fileData.BucketName);
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<PhotoData> filesData,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..filesData.Select(file => file.BucketName)];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }
}