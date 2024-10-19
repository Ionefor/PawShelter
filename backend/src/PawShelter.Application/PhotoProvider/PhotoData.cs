using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;

namespace PawShelter.Application.PhotoProvider;

public record PhotoData(Stream Stream, FilePath FilePath, string BucketName);