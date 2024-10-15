using System.Reactive.Linq;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PawShelter.Application.FileProvider;
using PawShelter.Application.Files.Delete;
using PawShelter.Domain.Shared;

namespace PawShelter.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(
        IMinioClient minioClient,
        ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<string, Error>> UploadFile(
        FileData fileData,
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
                WithObject(fileData.ObjectName);

            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return result.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Fail to upload to MinIO");
            return Error.Failure("file.upload", "Fail to upload to MinIO");
        }
    }

    public async Task<Result<string, Error>> DeleteFile(
        FileMetaData fileData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName);
            
            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
            
            return fileData.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Fail to upload to MinIO");
            return Error.Failure("file.upload", "Fail to upload to MinIO");
        }
    }

    public async Task<Result<string, Error>> GetFileByObjectName(
        FileMetaData fileData, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                .WithBucket(fileData.BucketName)
                .WithObject(fileData.ObjectName).
                WithExpiry(60 * 60 * 24);

            return await _minioClient.PresignedGetObjectAsync(args);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Fail to get file from minio");
            return Error.Failure("file.get", "Fail to get file from minio");
        }
    }
    
    public Result<IReadOnlyCollection<string>> GetFiles()
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
        
        return paths;
    }
}