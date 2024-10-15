using PawShelter.Application.FileProvider;

namespace PawShelter.Application.Files.Delete;

public record DeleteFileCommand(FileMetaData FileMetaData);