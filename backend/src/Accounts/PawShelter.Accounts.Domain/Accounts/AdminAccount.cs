namespace PawShelter.Accounts.Domain.Accounts;

public class AdminAccount
{
    private AdminAccount() {}

    public AdminAccount(User user)
    {
        User = user;
    }
    
    public Guid Id { get; set; }
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }
}