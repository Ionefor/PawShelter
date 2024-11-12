using PawShelter.Volunteers.Domain.ValueObjects.ForPet;

namespace PawShelter.Volunteers.Application.PhotoProvider;

public record PhotoData(Stream Stream, FilePath FilePath, string BucketName);