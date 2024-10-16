using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;

namespace PawShelter.Application.FileProvider;

public record FileMetaData(string BucketName, FilePath FilePath);