using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Domain.Accounts;

public class VolunteerAccount
{
    private VolunteerAccount() { }

    public VolunteerAccount(User user)
    {
        Id = Guid.NewGuid();
        User = user;
    }
    
    public Guid Id { get; init; }
    public User User { get; init; }
    
    public int Experience { get; init; }
    
    public IEnumerable<Requisite> Requisites { get; init; }
    
    public IEnumerable<Description> Certificates { get; init; }
    
}
