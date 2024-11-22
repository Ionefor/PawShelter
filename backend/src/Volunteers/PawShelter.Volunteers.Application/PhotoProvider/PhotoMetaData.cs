using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Volunteers.Application.PhotoProvider;

public record PhotoMetaData(string BucketName, FilePath FilePath);