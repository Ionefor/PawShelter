using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;

namespace PawShelter.Application.FileProvider;

public record FileData(Stream Stream, FilePath FilePath, string BucketName);