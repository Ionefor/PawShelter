namespace PawShelter.Domain.PetsManagement;

public interface ISoftDeletable
{
    void Delete();
    
    void Restore();
}