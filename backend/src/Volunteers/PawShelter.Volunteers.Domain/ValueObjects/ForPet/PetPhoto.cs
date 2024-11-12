namespace PawShelter.Volunteers.Domain.ValueObjects.ForPet;

public record PetPhoto
{
    private PetPhoto()
    {
    }

    public PetPhoto(FilePath path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public FilePath Path { get; } = null!;
    public bool IsMain { get; }
}