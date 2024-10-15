namespace PawShelter.Application.Files.Upload;

public record UploadFileCommand(Stream Stream, string BucketName, string ObjectName);