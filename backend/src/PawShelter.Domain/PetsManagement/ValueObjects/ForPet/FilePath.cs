using CSharpFunctionalExtensions;
using PawShelter.Domain.Shared;

namespace PawShelter.Domain.PetsManagement.ValueObjects.ForPet;

public record FilePath
{
    private FilePath()
    {
        
    }

    private FilePath(string path)
    {
        Path = path;
    }
    public string Path { get; }
    
    public static FilePath Create()
    {
        var path = Guid.NewGuid();
        var extension = System.IO.Path.GetExtension(path.ToString());
        
        var fullPath = extension + path;
        
        return new FilePath(fullPath);
    }
    
    public static Result<FilePath, Error> Create(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return Errors.General.ValueIsInvalid("Path");
        
        var extension = System.IO.Path.GetExtension(path);
        
        var fullPath = extension + path;
        
        return new FilePath(fullPath);
    }
}