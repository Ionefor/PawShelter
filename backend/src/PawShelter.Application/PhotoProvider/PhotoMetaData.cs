using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;

namespace PawShelter.Application.PhotoProvider;

public record PhotoMetaData(string BucketName, FilePath FilePath);