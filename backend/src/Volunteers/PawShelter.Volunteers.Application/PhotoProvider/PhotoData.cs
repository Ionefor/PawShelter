using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Volunteers.Application.PhotoProvider;

public record PhotoData(Stream Stream, FilePath FilePath, string BucketName);