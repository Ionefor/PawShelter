using PawShelter.SharedKernel.ValueObjects;

namespace PawShelter.Accounts.Domain.Accounts;

public class VolunteerAccount
{
    private VolunteerAccount() {}
    public VolunteerAccount(User user, Experience experience, IReadOnlyList<Requisite> requisites)
    {
        Id = Guid.NewGuid();
        User = user;
        Experience = experience;
        Requisites = requisites;
    }
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
    public Experience Experience { get; init; }
    public IReadOnlyList<Requisite> Requisites { get; init; }
    
    public IReadOnlyList<string> Certificates { get; init; }
}
