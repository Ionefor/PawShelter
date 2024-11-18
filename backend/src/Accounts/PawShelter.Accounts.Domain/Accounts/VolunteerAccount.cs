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
    
    public Guid Id { get; set; }
    public User User { get; set; }
    
    public int Experience { get; set; }
    
    public IEnumerable<Requisite> Requisites { get; set; }
    
    public IEnumerable<Description> Certificates { get; set; }
    
}
