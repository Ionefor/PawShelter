using PawShelter.Volunteers.Domain.ValueObjects.ForPet;

namespace PawShelter.Volunteers.Application.PhotoProvider;

public record PhotoMetaData(string BucketName, FilePath FilePath);