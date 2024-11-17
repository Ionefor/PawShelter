namespace PawShelter.Accounts.Domain.Accounts;

public class ParticipantAccount
{
    private ParticipantAccount() {}

    public ParticipantAccount(User user)
    {
        User = user;
    }
    
    public Guid Id { get; set; }
    
    public User User { get; set; }
}